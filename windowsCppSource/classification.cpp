#include <caffe/caffe.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <iosfwd>
#include <memory>
#include <string>
#include <utility>
#include <vector>
#include <windows.h>
#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
using namespace caffe;  // NOLINT(build/namespaces)
#include <iostream>
using std::string;
using std::cout;
using std::endl;

/* Pair (label, confidence) representing a prediction. */
typedef std::pair<string, float> Prediction;

extern "C" __declspec(dllexport) double* ClassifyImage(const char *modelDir, const char *image_file);

double* ClassifyImage(char *modelDir, cv::Mat img);
void cropCircle(cv::Mat& src, int sqSize, cv::Mat& dst);
int GetLabelID(std::string label);
void CropForDate(cv::Mat src, cv::Mat dst, float angle);

extern "C" __declspec(dllexport) int ReleaseMemory(double* pArray)
{
	delete[] pArray;
	return 0;
}

class Classifier {
public:
	Classifier();
	std::vector<Prediction> Classify(const cv::Mat& img, int N = 2);
	void Setup(const string& modelDir);

private:
	void SetMean(const string& mean_file);

	std::vector<float> Predict(const cv::Mat& img);

	void WrapInputLayer(std::vector<cv::Mat>* input_channels);

	void Preprocess(const cv::Mat& img,
		std::vector<cv::Mat>* input_channels);

private:
	shared_ptr<Net<float> > net_;
	cv::Size input_geometry_;
	int num_channels_;
	cv::Mat mean_;
	std::vector<string> labels_;
};


Classifier::Classifier() {
}

void Classifier::Setup(const string& modelDir) {

	const string& model_file = modelDir + "/deploy.prototxt";
	const string& trained_file = modelDir + "/snapshot.caffemodel";
	const string& mean_file = modelDir + "/mean.binaryproto";
	const string& label_file = modelDir + "/labels.txt";

	Caffe::set_mode(Caffe::CPU);
	/* Load the network. */
	net_.reset(new Net<float>(model_file, TEST));
	net_->CopyTrainedLayersFrom(trained_file);

	CHECK_EQ(net_->num_inputs(), 1) << "Network should have exactly one input!";
	CHECK_EQ(net_->num_outputs(), 1) << "Network should have exactly one output.";

	Blob<float>* input_layer = net_->input_blobs()[0];
	num_channels_ = input_layer->channels();
	CHECK(num_channels_ == 3 || num_channels_ == 1)
		<< "Input layer should have 1 or 3 channels.";
	input_geometry_ = cv::Size(input_layer->width(), input_layer->height());

	/* Load the binaryproto mean file. */
	SetMean(mean_file);

	/* Load labels. */
	std::ifstream labels(label_file.c_str());
	CHECK(labels) << "Unable to open labels file " << label_file;
	string line;
	while (std::getline(labels, line))
		labels_.push_back(string(line));

	Blob<float>* output_layer = net_->output_blobs()[0];
	CHECK_EQ(labels_.size(), output_layer->channels())
		<< "Number of labels is different from the output layer dimension.";
}

static bool PairCompare(const std::pair<float, int>& lhs,
	const std::pair<float, int>& rhs) {
	return lhs.first > rhs.first;
}

/* Return the indices of the top N values of vector v. */
static std::vector<int> Argmax(const std::vector<float>& v, int N) {
	std::vector<std::pair<float, int> > pairs;
	for (size_t i = 0; i < v.size(); ++i)
		pairs.push_back(std::make_pair(v[i], i));
	std::partial_sort(pairs.begin(), pairs.begin() + N, pairs.end(), PairCompare);

	std::vector<int> result;
	for (int i = 0; i < N; ++i)
		result.push_back(pairs[i].second);
	return result;
}

/* Return the top N predictions. */
std::vector<Prediction> Classifier::Classify(const cv::Mat& img, int N) {
	std::vector<float> output = Predict(img);

	std::vector<int> maxN = Argmax(output, N);
	std::vector<Prediction> predictions;
	for (int i = 0; i < N; ++i) {
		int idx = maxN[i];
		predictions.push_back(std::make_pair(labels_[idx], output[idx]));
	}

	return predictions;
}

/* Load the mean file in binaryproto format. */
void Classifier::SetMean(const string& mean_file) {
	BlobProto blob_proto;
	ReadProtoFromBinaryFileOrDie(mean_file.c_str(), &blob_proto);

	/* Convert from BlobProto to Blob<float> */
	Blob<float> mean_blob;
	mean_blob.FromProto(blob_proto);
	CHECK_EQ(mean_blob.channels(), num_channels_)
		<< "Number of channels of mean file doesn't match input layer.";

	/* The format of the mean file is planar 32-bit float BGR or grayscale. */
	std::vector<cv::Mat> channels;
	float* data = mean_blob.mutable_cpu_data();
	for (int i = 0; i < num_channels_; ++i) {
		/* Extract an individual channel. */
		cv::Mat channel(mean_blob.height(), mean_blob.width(), CV_32FC1, data);
		channels.push_back(channel);
		data += mean_blob.height() * mean_blob.width();
	}

	/* Merge the separate channels into a single image. */
	cv::Mat mean;
	cv::merge(channels, mean);

	/* Compute the global mean pixel value and create a mean image
	 * filled with this value. */
	cv::Scalar channel_mean = cv::mean(mean);
	mean_ = cv::Mat(input_geometry_, mean.type(), channel_mean);
}

std::vector<float> Classifier::Predict(const cv::Mat& img) {
	Blob<float>* input_layer = net_->input_blobs()[0];
	input_layer->Reshape(1, num_channels_,
		input_geometry_.height, input_geometry_.width);
	/* Forward dimension change to all layers. */
	net_->Reshape();

	std::vector<cv::Mat> input_channels;
	WrapInputLayer(&input_channels);

	Preprocess(img, &input_channels);

	net_->ForwardPrefilled();

	/* Copy the output layer to a std::vector */
	Blob<float>* output_layer = net_->output_blobs()[0];
	const float* begin = output_layer->cpu_data();
	const float* end = begin + output_layer->channels();
	return std::vector<float>(begin, end);
}

/* Wrap the input layer of the network in separate cv::Mat objects
 * (one per channel). This way we save one memcpy operation and we
 * don't need to rely on cudaMemcpy2D. The last preprocessing
 * operation will write the separate channels directly to the input
 * layer. */
void Classifier::WrapInputLayer(std::vector<cv::Mat>* input_channels) {
	Blob<float>* input_layer = net_->input_blobs()[0];

	int width = input_layer->width();
	int height = input_layer->height();
	float* input_data = input_layer->mutable_cpu_data();
	for (int i = 0; i < input_layer->channels(); ++i) {
		cv::Mat channel(height, width, CV_32FC1, input_data);
		input_channels->push_back(channel);
		input_data += width * height;
	}
}

void Classifier::Preprocess(const cv::Mat& img,
	std::vector<cv::Mat>* input_channels) {
	/* Convert the input image to the input image format of the network. */
	cv::Mat sample;
	if (img.channels() == 3 && num_channels_ == 1)
		cv::cvtColor(img, sample, CV_BGR2GRAY);
	else if (img.channels() == 4 && num_channels_ == 1)
		cv::cvtColor(img, sample, CV_BGRA2GRAY);
	else if (img.channels() == 4 && num_channels_ == 3)
		cv::cvtColor(img, sample, CV_BGRA2BGR);
	else if (img.channels() == 1 && num_channels_ == 3)
		cv::cvtColor(img, sample, CV_GRAY2BGR);
	else
		sample = img;

	cv::Mat sample_resized;
	if (sample.size() != input_geometry_)
		cv::resize(sample, sample_resized, input_geometry_);
	else
		sample_resized = sample;

	cv::Mat sample_float;
	if (num_channels_ == 3)
		sample_resized.convertTo(sample_float, CV_32FC3);
	else
		sample_resized.convertTo(sample_float, CV_32FC1);

	cv::Mat sample_normalized;
	cv::subtract(sample_float, mean_, sample_normalized);

	/* This operation will write the separate BGR planes directly to the
	 * input layer of the network because it is wrapped by the cv::Mat
	 * objects in input_channels. */
	cv::split(sample_normalized, *input_channels);

	CHECK(reinterpret_cast<float*>(input_channels->at(0).data)
		== net_->input_blobs()[0]->cpu_data())
		<< "Input channels are not wrapping the input layer of the network.";
}


double* ClassifyImage(char *modelDir, const char *image_file) {
	cv::Mat img = cv::imread(image_file);
	CHECK(!img.empty()) << "Unable to decode image " << image_file;
	return ClassifyImage(modelDir, img);
}


double* ClassifyImage(char *modelDir, cv::Mat img) {
	//this function should be returning a structure! 
	double* result = new double[8];
	static cv::Mat center32;
	static cv::Mat design60;
	static cv::Mat date32;
	static int counter;
	counter += 1;
	static Classifier centerClassifier;
	static Classifier designClassifier;
	static Classifier angleClassifier;
	static Classifier dateClassifier;

	if (counter == 1) {
		centerClassifier.Setup(strcat(modelDir, "/centered"));
		designClassifier.Setup(strcat(modelDir, "/designs"));
		angleClassifier.Setup(strcat(modelDir, "/angles"));
		dateClassifier.Setup(strcat(modelDir, "/dates"));
	}

	std::vector<Prediction> predictions;
	Prediction p;
	cv::Size size;
	
	size = cv::Size(32, 32);
	cv::resize(img, center32, size, 0, 0, 1);
	predictions = designClassifier.Classify(center32);
	p = predictions[0];
	result[0] = GetLabelID(p.first);
	result[1] = p.second;
	
	if (img.rows == 406){
		cropCircle(img, 60, design60);
	}
	predictions = designClassifier.Classify(design60);
	p = predictions[0];
	result[2] = GetLabelID(p.first);
	result[3] = p.second;
	
	predictions = angleClassifier.Classify(design60);
	p = predictions[0];
	result[4] = GetLabelID(p.first);
	result[5] = p.second;
	
	CropForDate(img, center32, 360 - result[4]);
	predictions = designClassifier.Classify(center32);
	p = predictions[0];
	result[6] = GetLabelID(p.first);
	result[7] = p.second;

	//cout << "Number of Predictions" << predictions.size() << endl;
	//for (size_t i = 0; i < predictions.size(); ++i) {
	//	Prediction p = predictions[i];
	//	cout << p.first << endl;
	//	result[i] = GetLabelID(p.first);
	//	result[i + predictions.size()] = p.second;
	//}

	
	///* Print the top N predictions. */
	//double * result = new double[3];
	////cout << "11" << endl;


	//double totalConfidence = 0;
	//double totalPredicted = 0;
	//double totalPredictedConfidence = 0;
	////cout << "12" << endl;

	//std::string::size_type sz;     // alias of size_t
	////cout << "13" << endl;


	//for (size_t i = 0; i < predictions.size(); ++i) {
	//	Prediction p = predictions[i];
	//	//cout << "14" << endl;

	//	double confidence = p.second;
	//	double degreePredicted = std::stod(p.first, &sz);
	//	totalPredicted += degreePredicted;
	//	totalPredictedConfidence += (confidence * degreePredicted);
	//	totalConfidence += confidence;
	//	std::cout << std::fixed << std::setprecision(4) << p.second << " - \""
	//		<< p.first << "\"" << std::endl;
	//}


	////cout << "15" << endl;


	//double totalPredictedMean = totalPredicted / 5;
	//double totalPredictedSquareMeanDeviations = 0;

	//for (size_t i = 0; i < predictions.size(); ++i) {
	//	Prediction p = predictions[i];
	//	double degreePredicted = std::stod(p.first, &sz);
	//	totalPredictedSquareMeanDeviations += (totalPredictedMean - degreePredicted) * (totalPredictedMean - degreePredicted);
	//}
	////this could be better because it could take the confidence into account:
	//double predictedStandardDeviation = sqrt(totalPredictedSquareMeanDeviations / 5);

	////This is not correct around 359-000-001
	////this does not acount for outliers(112)  222-112-223-221-220
	//result[0] = totalPredictedConfidence / totalConfidence;
	//result[1] = totalConfidence;
	//result[3] = predictedStandardDeviation;

	return result;
}

int GetLabelID(std::string label){
	if (isdigit(label[0])){
		return std::stoi(label);
	}
if (label == "canadaOther"){
		return 0;
	}
	if (label == "heads"){
		return 1;
	}
	if (label == "maple"){
		return 2;
	}
	if (label == "tails"){
		return 3;
	}
	if (label == "wheat"){
		return 4;
	}
	return -1;
}



