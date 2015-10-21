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
using namespace cv;

//pixelShift depends on the camera and the speed of the belt
int pixelShift = 35;


void deskew(cv::Mat& src, cv::Mat& dst)
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
	dstTri[2] = Point2f(pixelShift, src.rows);

	/// Get the Affine Transform
	warp_mat = getAffineTransform(srcTri, dstTri);

	// Apply the Affine Transform just found to the src image
	warpAffine(src, dst, warp_mat, dst.size(), cv::INTER_CUBIC);
}












