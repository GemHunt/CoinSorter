#include <caffe/caffe.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <iosfwd>
#include <iomanip>
#include <memory>
#include <string>
#include <utility>
#include <vector>
#include <windows.h>
using namespace caffe;  // NOLINT(build/namespaces)
#include <iostream>
using std::string;
using std::cout;
using std::endl;

void rotate(cv::Mat& src, double angle, cv::Mat& dst);
void cropCircle(cv::Mat& src, int sqSize, cv::Mat& dst);
void CropForDate(cv::Mat src, std::string outputFileName, float angle);
void CropForDate(cv::Mat src, cv::Mat dst, float angle);


//extern "C" __declspec(dllexport) int Augment(const char *image_file, const char *output_file,int angle) {
//	cv::Mat src = cv::imread(image_file);
//	cv::Mat dst;
//	rotate(src, angle, dst);
//	cropCircle(dst, 60, dst);
//	cv::imwrite(output_file, dst);
//	return 1;
//}


extern "C" __declspec(dllexport) int Augment(const char *fileDir,  const char *outputRootDir, int imageID, float angle) {
	cv::Mat src = cv::imread(fileDir + std::to_string(imageID) + ".jpg");
	cv::Mat dst;
	cropCircle(src, 100, src);
	cv::Size size(60, 60);

	for (int a=0; a < 360; a++){
		rotate(src, a + (360-angle), dst);
		cv::resize(dst, dst, size, 0, 0, 1);
		std::stringstream dir;
		dir << std::setfill('0') << std::setw(3) << a << "/";
		std::string outputfile = outputRootDir + dir.str() + std::to_string(imageID) + ".jpg";
		cv::imwrite(outputfile, dst);
	}
	return 1;
}


extern "C" __declspec(dllexport) int CropForDate(const char *fileDir, const char *outputRootDir, int imageID, float angle,bool augment) {
	cv::Mat src = cv::imread(fileDir + std::to_string(imageID) + ".jpg");
	if (augment) {
		int a = 1, x = 1, y = 1;

		for (int a = 0; a < 10; a++)
		{
			int augAngle;
			augAngle = (360 - angle) + (a - 5);
			std::string outputfile = outputRootDir + std::to_string(imageID) + std::to_string(a) + ".jpg";
			CropForDate(src, outputfile, augAngle);
		}
	}
	else{
		std::string outputfile = outputRootDir + std::to_string(imageID) + ".jpg";
		CropForDate(src, outputfile, 360 - angle);
	}
	return 1;
}


void CropForDate(cv::Mat src, std::string outputFileName, float angle) {
	cv::Mat cropped;
	CropForDate(src, cropped, angle);
	cv::imwrite(outputFileName, cropped);
}

void CropForDate(cv::Mat src, cv::Mat dst, float angle) {
	rotate(src, angle, dst);
	cv::Rect dateROI(307, 250, 64, 64);
	//cv::Rect dateROI(257, 226, 124, 124);
	cv::Mat cropped;
	dst = dst(dateROI);
	cv::Size size(32, 32);
	cv::resize(dst, dst, size, 0, 0, 1);
}


void rotate(cv::Mat& src, double angle, cv::Mat& dst)
{
	int len = std::max(src.cols, src.rows);
	cv::Point2f pt(len / 2., len / 2.);
	cv::Mat r = cv::getRotationMatrix2D(pt, angle, 1.0);
	cv::warpAffine(src, dst, r, cv::Size(len, len), cv::INTER_CUBIC);
}


void cropCircle(cv::Mat& src, int sqSize, cv::Mat& dst)
{
	static cv::Mat mask;
	static int counter;
	counter += 1;

	if (counter == 1) {
		mask = cv::imread("C:/Users/pkrush/Documents/GemHunt/CoinSorter/models/centered/circleMask406.png");
	}
	//imwrite("F:/src.png", src);
	dst = src & mask;
	//imwrite("F:/mask.png", mask);
	//imwrite("F:/masked.png", dst);
	
	//dst = dst(cv::Rect(285, 151, 100,100));
	cv::Size size(sqSize, sqSize);
	cv::resize(dst, dst, size, 0, 0, 1);
	cv::cvtColor(dst, dst, CV_BGR2GRAY);
	//imwrite("F:/output.png", dst);
}





