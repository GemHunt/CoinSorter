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
using namespace cv;
using std::string;
using std::cout;
using std::endl;

int findContour(Mat);

//*******  Contour finding sample *****
Mat src;
int CrMin = 158;
int CrMax = 255;
int erodeSquare = 5;
int dilateSquare = 30;
int coinRadius = 203;
int YCrop = 180;
int XOffset = 2;
int YOffset = 132;

int max_thresh = 255;

void startCoinCenterGUI(Mat input);
void thresh_callback(int, void*);
Point CoinCenter(Mat input, bool showImages);


extern "C" __declspec(dllexport) int FindCoinCenter(int imageID, bool showImages) {
	Mat input;
	input = imread("F:/OpenCV/" + std::to_string(imageID) + "raw.jpg");
	if (showImages){
		startCoinCenterGUI(input);
	}

	Point coinCenter = CoinCenter(input, showImages);
	return 0;
}

void startCoinCenterGUI(Mat input)
{
	src = input;

	/// Create Window
	char* source_window = "Source";
	namedWindow(source_window, CV_WINDOW_AUTOSIZE);
	imshow(source_window, src);

	createTrackbar(" CrMin:", "Source", &CrMin, max_thresh, thresh_callback);
	createTrackbar(" CrMax:", "Source", &CrMax, max_thresh, thresh_callback);
	createTrackbar(" erode:", "Source", &erodeSquare, 35, thresh_callback);
	createTrackbar(" dilate:", "Source", &dilateSquare, 35, thresh_callback);
	createTrackbar(" radius:", "Source", &coinRadius, 300, thresh_callback);
	createTrackbar(" YCrop:", "Source", &YCrop, 400, thresh_callback);
	createTrackbar(" XOffset:", "Source", &XOffset, 20, thresh_callback);
	createTrackbar(" YOffset:", "Source", &YOffset, 200, thresh_callback);
	thresh_callback(0, 0);
	waitKey(1);
}

/** @function thresh_callback */
void thresh_callback(int, void*){
	CoinCenter(src, 1);
}


Point CoinCenter(Mat input, bool showImages)
{
	//cout << "101" << endl;
	Point coinCenter;
	vector<vector<Point> > contours;
	vector<Vec4i> hierarchy;
	//cout << "102" << endl;
	cv::Mat YCrCb, threshold, croppedFrame;
	//cout << "103" << endl;
	cv::Rect myROI(0, 0, 639, YCrop);
	//cout << "103" << endl;
	croppedFrame = input(myROI);
	//cout << "103" << endl;
	cvtColor(croppedFrame, YCrCb, COLOR_RGB2YCrCb);
	//cout << "103" << endl;
	inRange(YCrCb, cv::Scalar(0, 0, CrMin), cv::Scalar(255, 255, CrMax), threshold);
	if (erodeSquare == 0) {
		erodeSquare = 1;
	}
	if (dilateSquare == 0) {
		dilateSquare = 1;
	}

	Mat erodeElement = getStructuringElement(MORPH_RECT, cv::Size(erodeSquare, erodeSquare));
	Mat dilateElement = getStructuringElement(MORPH_RECT, cv::Size(dilateSquare, dilateSquare));
	erode(threshold, threshold, erodeElement);
	dilate(threshold, threshold, dilateElement);
	//cout << "104" << endl;
	if (showImages){
		namedWindow("threshold", CV_WINDOW_AUTOSIZE);
		imshow("threshold", threshold);
	}

	//cv::resize(threshold, threshold, cv::Size(360, 286));
	//cout << "10" << endl;

	/// Find contours
	findContours(threshold, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	//cout << "11" << endl;

	/// Approximate contours to polygons + get bounding rects and circles
	vector<vector<Point> > contours_poly(contours.size());
	vector<Rect> boundRect(contours.size());
	//cout << "12" << endl;
	vector<Point2f>center(contours.size());
	//cout << "13" << endl;
	vector<float>radius(contours.size());
	//cout << "14" << endl;
	for (int i = 0; i < contours.size(); i++)
	{
		approxPolyDP(Mat(contours[i]), contours_poly[i], 3, true);
		boundRect[i] = boundingRect(Mat(contours_poly[i]));
		minEnclosingCircle((Mat)contours_poly[i], center[i], radius[i]);
	}
	//cout << "15" << endl;


	int contourIndex = -1;
	int maxRadius = 0;
	//cout << "16" << endl;
	for (int i = 0; i < contours.size(); i++)
	{
		if (radius[i]>maxRadius) {
			maxRadius = (int)radius[i];
			contourIndex = i;
		}
	}

	//return nothing if nothing found:
	if (contourIndex == -1){
		cout << "No contour Found" << endl;
		return Point(0,0);
		//return Point(320,240);
		//srand(static_cast<unsigned int>(time(0)));
		//return Point(260 + (rand() % 60), 180 + (rand() % 60));
	}

	Scalar color = Scalar(0, 255, 0);
	Moments mu;
	mu = moments(contours[contourIndex], false);
	int coinX = (int)(mu.m10 / mu.m00) - XOffset;
	int coinY = (int)(mu.m01 / mu.m00) + YOffset;
	coinCenter = Point(coinX, coinY);
	//cout << "17" << endl;
	if (showImages == 1){
		/// Draw polygonal contour + bonding rects + circles
		Mat drawing = input.clone();
		//Mat drawing = Mat::zeros(threshold.size(), CV_8UC3);
		drawContours(drawing, contours_poly, contourIndex, color, 1, 8, vector<Vec4i>(), 0, Point());
		rectangle(drawing, boundRect[contourIndex].tl(), boundRect[contourIndex].br(), color, 2, 8, 0);
		circle(drawing, coinCenter, coinRadius, color, 2, 8, 0);
		namedWindow("Contours", CV_WINDOW_AUTOSIZE);
		imshow("Contours", drawing);
	}
	//cout << "18" << endl;
	return coinCenter;
}

Mat CropToCenter(Mat input, Point coinCenter)
{
	int centerX = (int)coinCenter.x - coinRadius;
	int centerY = (int)coinCenter.y - coinRadius;
	if (centerX < 0){
		centerX = 0;
	}

	if (centerX + coinRadius * 2 > input.cols - 1){
		centerX = 0;
	}

	if (centerY < 0){
		centerY = 0;
	}

	if (centerY + coinRadius * 2 > input.rows - 1){
		centerY = 0;
	}

	cv::Rect myROI(centerX, centerY, coinRadius * 2, coinRadius * 2);
	//cout << "20" << endl;
	return input(myROI);
}


