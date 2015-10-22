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

int findContour(Mat);

//*******  Contour finding sample *****
Mat src;
int CrMin = 170;
int CrMax = 255;
int erodeSquare = 5;
int dilateSquare = 30;
int coinRadius = 203;
int YCrop = 180;
int XOffset = 5;
int YOffset = 127;

int max_thresh = 255;

void thresh_callback(int, void*);
Point2f findCoinCenter(Mat input);

extern "C" __declspec(dllexport) int findCoinCenter(int imageID) {
	Mat input;
	input = imread("F:/OpenCV/" + std::to_string(imageID) + ".jpg");
	findCoinCenter(input);
	return 0;
}


Point2f findCoinCenter(Mat input)
{
	src = input;
	Point2f coinCenter;

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

	cv::Mat YCrCb, threshold, croppedFrame;
	
	cv::Rect myROI(0, 0, 639, YCrop);
	croppedFrame = src(myROI);
	cvtColor(croppedFrame, YCrCb, COLOR_RGB2YCrCb);
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

	namedWindow("threshold", CV_WINDOW_AUTOSIZE);
	imshow("threshold", threshold);


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

	/// Draw polygonal contour + bonding rects + circles
	Mat drawing = src.clone();
	//Mat drawing = Mat::zeros(threshold.size(), CV_8UC3);
	for (int i = 0; i < contours.size(); i++)
	{
		if (radius[i]>100) {
			Scalar color = Scalar(0,255,0);
			drawContours(drawing, contours_poly, i, color, 1, 8, vector<Vec4i>(), 0, Point());
			rectangle(drawing, boundRect[i].tl(), boundRect[i].br(), color, 2, 8, 0);

			//circle(drawing, center[i], (int)radius[i], color, 2, 8, 0);
			//How should coinRadius be used?
			//coinRadius = (int)radius[i];

			/// Get the moments
			Moments mu;
			mu = moments(contours[i], false);
			int coinX = (int)(mu.m10 / mu.m00) - XOffset;
			int coinY = (int)(mu.m01 / mu.m00) + YOffset;
			cv::Point blobCenter(coinX, coinY);
			//circle(drawing, blobCenter, (int)radius[i], color, 1, 8, 0);
			circle(drawing, blobCenter,coinRadius, color, 2, 8, 0);
		}
	}

	namedWindow("Contours", CV_WINDOW_AUTOSIZE);
	imshow("Contours", drawing);

	//cout << "17" << endl;
}





/* show a cirle acound the coin:
int thickness = 1;
int lineType = 8;
int frameWidth = frame.cols;
int frameHeight = frame.rows;

circle(frame,
Point(frameWidth / 2, frameHeight/2),
coinRadius,
Scalar(0, 0, 255),
thickness,
lineType);*/



/* Crop to center of the coin:
int coinRadius = 208;
//Why -1? I don't know, it's just is better centered:
int centerX = (int)coinCenter.x - coinRadius + 1;
int centerY = (int)coinCenter.y - coinRadius;
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
*/