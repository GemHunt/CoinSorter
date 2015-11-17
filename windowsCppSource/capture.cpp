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
using std::string;

///for web cam
#include <iostream>
using namespace cv;
using std::cout;
using std::endl;
///for web cam

extern "C" __declspec(dllexport) double* ClassifyFromWebCam(int imageID, bool showImages, bool classify, bool deskewImage, bool autorotate, const char *modelDir);
void deskew(cv::Mat& src, cv::Mat& dst);
Point CoinCenter(Mat input, bool showImages);
Mat CropToCenter(Mat input, Point coinCenter);
double* ClassifyImage(char *modelDir, cv::Mat img);
void rotate(cv::Mat& src, double angle, cv::Mat& dst);


double* ClassifyFromWebCam(int imageID, bool showImages, bool classify, bool deskewImage, bool autorotate, const char *modelDir)
{

	//cout << "01" << endl;
	double* result = new double[8];
	static bool setup = false;
	double dWidth, dHeight;
	static VideoCapture cap;
	cout << "02" << endl;
	if (setup == false) {

		cap.open(0); // open the video camera no. 0

		if (!cap.isOpened())  // if not success, exit program
		{
			cout << "Cannot open the video cam" << endl;
			return result;
		}

		dWidth = cap.get(CV_CAP_PROP_FRAME_WIDTH); //get the width of frames of the video
		dHeight = cap.get(CV_CAP_PROP_FRAME_HEIGHT); //get the height of frames of the video

		cout << "Frame size : " << dWidth << " x " << dHeight << endl;

		if (showImages){
			namedWindow("MyVideo", CV_WINDOW_AUTOSIZE); //create a window called "MyVideo"
		}
		cap.set(CV_CAP_PROP_BRIGHTNESS, 0);
		cap.set(CV_CAP_PROP_CONTRAST, 47);
		cap.set(CV_CAP_PROP_SATURATION, 32);
		cap.set(CV_CAP_PROP_GAIN, 24);

		setup = true;
	}
	
	Mat frame;
	bool bSuccess;
	bSuccess = cap.read(frame); // read a new frame from video
	waitKey(1);
	//for whatever reasons it's keeping a frame in a buffer, or something, and you have to call read twice to get a new frame:
	bSuccess = cap.read(frame); // read a new frame from video

	if (!bSuccess) //if not success, break loop
	{
		cout << "Cannot read a frame from video stream" << endl;
		return 0;
	}
	//cout << "03" << endl;
	if (deskewImage) {
		deskew(frame, frame);
	}

	//cout << "05" << endl;

	imwrite("F:/OpenCV/Raw/" + std::to_string(imageID) + "raw.jpg", frame);
	Point coinCenter = CoinCenter(frame, showImages);
	//cout << "06" << endl;
	
	if (coinCenter.x == 0) {
		cout << "Coin Not found" << endl;
		return 0;
	}
	
	cv::Mat crop = CropToCenter(frame, coinCenter);
	imwrite("F:/OpenCV/" + std::to_string(imageID) + ".jpg", crop);
	
	if (classify) {
		char* dir = (char*)modelDir;
		result = ClassifyImage(dir, crop);
	}

	if (autorotate && (int)result[2] == 1){
		rotate(crop, 360-result[4], crop);
	}

	if (showImages){
		imshow("MyVideo", crop);

		if (waitKey(1) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			destroyWindow("MyVideo");
			return result;
		}
	}



	return result;
}

