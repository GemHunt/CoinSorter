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


///for web cam
#include <iostream>
using namespace cv;
using std::cout;
using std::endl;
///for web cam

//for Theshold
int findContour(Mat);
int coinRadius = 255;
int coinX = 200;
int coinY = 200;
//for Theshold

void rotate(cv::Mat& src, double angle, cv::Mat& dst);
void deskew(cv::Mat& src, float angle, cv::Mat& dst);
int Skew = 7;


#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

using namespace caffe;  // NOLINT(build/namespaces)
using std::string;


/* Pair (label, confidence) representing a prediction. */
typedef std::pair<string, float> Prediction;

extern "C" __declspec(dllexport) double* ClassifyImage(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file, const char *image_file);
extern "C" __declspec(dllexport) int captureFromWebCam(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file);

double* ClassifyImage(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file, cv::Mat img);


extern "C" __declspec(dllexport) int ReleaseMemory(double* pArray)
{
	delete[] pArray;
	return 0;
}


class Classifier {
public:
	Classifier();
	std::vector<Prediction> Classify(const cv::Mat& img, int N = 5);

	void Setup(const string& model_file,
		const string& trained_file,
		const string& mean_file,
		const string& label_file);

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


void Classifier::Setup(const string& model_file,
	const string& trained_file,
	const string& mean_file,
	const string& label_file) {

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


double* ClassifyImage(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file, const char *image_file) {
	cv::Mat img = cv::imread(image_file, -1);
	CHECK(!img.empty()) << "Unable to decode image " << image_file;
	return ClassifyImage(model_file, trained_file, mean_file, label_file, img);
}


double* ClassifyImage(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file, cv::Mat img) {
	//::google::InitGoogleLogging("classification-d.dll");

	static int counter;
	counter += 1;
	static Classifier classifier;

	if (counter == 1) {
		classifier.Setup(model_file, trained_file, mean_file, label_file);
	}


	std::vector<Prediction> predictions = classifier.Classify(img);
	/* Print the top N predictions. */
	double * result = new double[3];
	//cout << "11" << endl;


	double totalConfidence = 0;
	double totalPredicted = 0;
	double totalPredictedConfidence = 0;
	//cout << "12" << endl;

	std::string::size_type sz;     // alias of size_t
	//cout << "13" << endl;


	for (size_t i = 0; i < predictions.size(); ++i) {
		Prediction p = predictions[i];
		//cout << "14" << endl;

		double confidence = p.second;
		double degreePredicted = std::stod(p.first, &sz);
		totalPredicted += degreePredicted;
		totalPredictedConfidence += (confidence * degreePredicted);
		totalConfidence += confidence;
		std::cout << std::fixed << std::setprecision(4) << p.second << " - \""
			<< p.first << "\"" << std::endl;
	}


	//cout << "15" << endl;


	double totalPredictedMean = totalPredicted / 5;
	double totalPredictedSquareMeanDeviations = 0;

	for (size_t i = 0; i < predictions.size(); ++i) {
		Prediction p = predictions[i];
		double degreePredicted = std::stod(p.first, &sz);
		totalPredictedSquareMeanDeviations += (totalPredictedMean - degreePredicted) * (totalPredictedMean - degreePredicted);
	}
	//this could be better because it could take the confidence into account:
	double predictedStandardDeviation = sqrt(totalPredictedSquareMeanDeviations / 5);

	//This is not correct around 359-000-001
	//this does not acount for outliers(112)  222-112-223-221-220
	result[0] = totalPredictedConfidence / totalConfidence;
	result[1] = totalConfidence;
	result[3] = predictedStandardDeviation;

	return result;
}



//int OldClassifyNetwork(int argc, char** argv) {
//	if (argc != 6) {
//		std::cerr << "Usage: " << argv[0]
//			<< " deploy.prototxt network.caffemodel"
//			<< " mean.binaryproto labels.txt img.jpg" << std::endl;
//		return 1;
//	}
//
//	::google::InitGoogleLogging(argv[0]);
//
//	string model_file = argv[1];
//	string trained_file = argv[2];
//	string mean_file = argv[3];
//	string label_file = argv[4];
//	Classifier classifier(model_file, trained_file, mean_file, label_file);
//
//	string file = argv[5];
//
//	std::cout << "---------- Prediction for "
//		<< file << " ----------" << std::endl;
//
//	cv::Mat img = cv::imread(file, -1);
//	CHECK(!img.empty()) << "Unable to decode image " << file;
//
//	std::vector<Prediction> predictions;
//
//	//for (int x = 0; x < 10; ++x) {
//		predictions = classifier.Classify(img);
//	//}
//
//	/* Print the top N predictions. */
//	for (size_t i = 0; i < predictions.size(); ++i) {
//		Prediction p = predictions[i];
//		std::cout << std::fixed << std::setprecision(4) << p.second << " - \""
//			<< p.first << "\"" << std::endl;
//	}
//	return 1;
//}



int captureFromWebCam(const char *model_file, const char *trained_file, const char *mean_file, const char *label_file)
{
	static bool setup = false;
	double dWidth, dHeight;
	VideoCapture cap;
	if (setup == false) {

		cap.open(0); // open the video camera no. 0

		if (!cap.isOpened())  // if not success, exit program
		{
			cout << "Cannot open the video cam" << endl;
			return -1;
		}

		dWidth = cap.get(CV_CAP_PROP_FRAME_WIDTH); //get the width of frames of the video
		dHeight = cap.get(CV_CAP_PROP_FRAME_HEIGHT); //get the height of frames of the video

		cout << "Frame size : " << dWidth << " x " << dHeight << endl;

		namedWindow("MyVideo", CV_WINDOW_AUTOSIZE); //create a window called "MyVideo"
		setup = true;
	}


	Mat frame;

	bool bSuccess = cap.read(frame); // read a new frame from video

	if (!bSuccess) //if not success, break loop
	{
		cout << "Cannot read a frame from video stream" << endl;
		return 0;
	}

	int thickness = 1;
	int lineType = 8;


	//int coinRadius = 208;
	int frameWidth = frame.cols;
	int frameHeight = frame.rows;

	/*
	circle(frame,
	Point(frameWidth / 2, frameHeight/2),
	coinRadius,
	Scalar(0, 0, 255),
	thickness,
	lineType);*/



	cv::Mat deskewedFrame = Mat::zeros(frame.rows, frame.cols, frame.type());
	deskew(frame, Skew, deskewedFrame);

	findContour(deskewedFrame);

	//cv::Rect myROI(frameWidth / 2 - coinRadius, frameHeight / 2 - coinRadius, coinRadius * 2, coinRadius * 2);
	//cv::Rect myROI(coinCenter.x - coinRadius, coinCenter.y - coinRadius, coinRadius * 2, coinRadius * 2);
	//cout << "18" << endl;
	coinRadius = 208;
	//cout << "19" << endl;

	//Why -1? I don't know, it's just is better centered:
	int centerX = coinX - coinRadius + 1;
	int centerY = coinY - coinRadius;
	if (centerX < 0){
		centerX = 0;
	}

	if (centerX + coinRadius * 2 > deskewedFrame.cols - 1){
		centerX = 0;
	}

	if (centerY < 0){
		centerY = 0;
	}

	if (centerY + coinRadius * 2 > deskewedFrame.rows - 1){
		centerY = 0;
	}


	cv::Rect myROI(centerX, centerY, coinRadius * 2, coinRadius * 2);
	//cout << "20" << endl;
	cv::Mat croppedFrame = deskewedFrame(myROI);
	cout << "" << endl;

	double* result = ClassifyImage(model_file, trained_file, mean_file, label_file, croppedFrame);

	cv::Mat rotatedFrame;
	rotate(croppedFrame, 360 - result[0], rotatedFrame);

	if (result[1] > .9) {
		imshow("MyVideo", rotatedFrame); //show the frame in "MyVideo" window
	}



	if (waitKey(1) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
	{
		cout << "esc key is pressed by user" << endl;
		destroyWindow("MyVideo");
		return 0;
	}
	return 0;
}




//*******  Contour finding sample *****
Mat src;
int HMin = 24;
int HMax = 140;
int VMin = 0;
int VMax = 255;
int SMin = 0;
int SMax = 255;
int max_thresh = 255;
int max_skew = 60;
RNG rng(12345);

/// Function header
void thresh_callback(int, void*);

/** @function main */
int findContour(Mat input)
{
	src = input;
	/// Convert image to gray and blur it
	//cvtColor(src, src_gray, CV_BGR2GRAY);
	//blur(src_gray, src_gray, Size(7, 7));

	/// Create Window
	char* source_window = "Source";
	namedWindow(source_window, CV_WINDOW_AUTOSIZE);
	imshow(source_window, src);

	createTrackbar(" HMin:", "Source", &HMin, max_thresh, thresh_callback);
	createTrackbar(" HMax:", "Source", &HMax, max_thresh, thresh_callback);
	createTrackbar(" VMin:", "Source", &VMin, max_thresh, thresh_callback);
	createTrackbar(" VMax:", "Source", &VMax, max_thresh, thresh_callback);
	createTrackbar(" SMin:", "Source", &SMin, max_thresh, thresh_callback);
	createTrackbar(" SMax:", "Source", &SMax, max_thresh, thresh_callback);

	createTrackbar(" Skew:", "Source", &Skew, max_skew, thresh_callback);
	thresh_callback(0, 0);

	//waitKey(0);
	return(0);
}

/** @function thresh_callback */
void thresh_callback(int, void*)
{
	//cout << "9" << endl;
	//Mat threshold_output;
	vector<vector<Point> > contours;
	vector<Vec4i> hierarchy;

	/// Detect edges using Threshold
	//threshold(src_gray, threshold_output, thresh, 255, THRESH_BINARY);

	cv::Mat HSV, threshold;
	cvtColor(src, HSV, COLOR_BGR2HSV);
	//inRange(HSV, cv::Scalar(HMin, SMin, VMin), cv::Scalar(HMax, SMax, VMax), threshold);
	inRange(HSV, cv::Scalar(HMin, SMin, VMin), cv::Scalar(HMax, SMax, VMax), threshold);
	Mat erodeElement = getStructuringElement(MORPH_RECT, cv::Size(5, 5));
	Mat dilateElement = getStructuringElement(MORPH_RECT, cv::Size(12, 12));
	erode(threshold, threshold, erodeElement);
	dilate(threshold, threshold, dilateElement);
	//cv::resize(threshold, threshold, cv::Size(360, 286));
	//cout << "10" << endl;

	/// Find contours
	findContours(threshold, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	//cout << "11" << endl;

	/// Approximate contours to polygons + get bounding rects and circles
	vector<vector<Point> > contours_poly(contours.size());
	//vector<Rect> boundRect(contours.size());
	//cout << "12" << endl;
	vector<Point2f>center(contours.size());
	//cout << "13" << endl;
	vector<float>radius(contours.size());
	//cout << "14" << endl;
	for (int i = 0; i < contours.size(); i++)
	{
		approxPolyDP(Mat(contours[i]), contours_poly[i], 3, true);
		//boundRect[i] = boundingRect(Mat(contours_poly[i]));
		minEnclosingCircle((Mat)contours_poly[i], center[i], radius[i]);
	}
	//cout << "15" << endl;

	/// Draw polygonal contour + bonding rects + circles
	Mat drawing = Mat::zeros(threshold.size(), CV_8UC3);
	for (int i = 0; i < contours.size(); i++)
	{
		if ((radius[i] > 125) && (radius[i] < 230))  {
			Scalar color = Scalar(rng.uniform(0, 255), rng.uniform(0, 255), rng.uniform(0, 255));
			drawContours(drawing, contours_poly, i, color, 1, 8, vector<Vec4i>(), 0, Point());
			//rectangle(drawing, boundRect[i].tl(), boundRect[i].br(), color, 2, 8, 0);
			circle(drawing, center[i], (int)radius[i], color, 2, 8, 0);
			coinRadius = (int)radius[i];

			/// Get the moments
			Moments mu;
			mu = moments(contours[i], false);
			coinX = mu.m10 / mu.m00;
			coinY = mu.m01 / mu.m00;
		}
	}

	//cout << "16" << endl;
	/// Show in a window
	//namedWindow("Contours", CV_WINDOW_AUTOSIZE);
	//imshow("Contours", drawing);

	//namedWindow("threshold", CV_WINDOW_AUTOSIZE);
	//imshow("threshold", threshold);
	//cout << "17" << endl;
}

void rotate(cv::Mat& src, double angle, cv::Mat& dst)
{
	int len = std::max(src.cols, src.rows);
	cv::Point2f pt(len / 2., len / 2.);
	cv::Mat r = cv::getRotationMatrix2D(pt, angle, 1.0);
	cv::warpAffine(src, dst, r, cv::Size(len, len), cv::INTER_CUBIC);
}

void deskew(cv::Mat& src, float shiftpixels, cv::Mat& dst)
{
	Point2f srcTri[3];
	Point2f dstTri[3];

	Mat rot_mat(2, 3, CV_32FC1);
	Mat warp_mat(2, 3, CV_32FC1);

	/// Set your 3 points to calculate the  Affine Transform
	srcTri[0] = Point2f(0, 0);
	srcTri[1] = Point2f(src.cols - 1, 0);
	srcTri[2] = Point2f(0, src.rows - 1);

	//dstTri is the same except the bottom is moved over shiftpixels:
	dstTri[0] = srcTri[0];
	dstTri[1] = srcTri[1];
	dstTri[2] = Point2f(shiftpixels, src.rows);

	/// Get the Affine Transform
	warp_mat = getAffineTransform(srcTri, dstTri);

	// Apply the Affine Transform just found to the src image
	warpAffine(src, dst, warp_mat, dst.size(), cv::INTER_CUBIC);
}












