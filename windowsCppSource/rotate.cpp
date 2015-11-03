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
using namespace caffe;  // NOLINT(build/namespaces)
#include <iostream>
using std::string;
using std::cout;
using std::endl;

void rotate(cv::Mat& src, double angle, cv::Mat& dst);
void cropCircle(cv::Mat& src, int sqSize, cv::Mat& dst);

extern "C" __declspec(dllexport) int Augment(const char *image_file, const char *output_file,int angle) {
	cv::Mat src = cv::imread(image_file);
	cv::Mat dst;
	rotate(src, angle, dst);
	cropCircle(dst, 60, dst);
	cv::imwrite(output_file, dst);
	return 1;
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
		mask = cv::imread("F:/CircleMask406.png");
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





