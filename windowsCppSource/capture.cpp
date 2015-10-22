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

extern "C" __declspec(dllexport) int captureFromWebCam(int imageID, int showImages);
void deskew(cv::Mat& src, cv::Mat& dst);
Point CoinCenter(Mat input, int showImages);

int captureFromWebCam(int imageID, int showImages)
{
	static bool setup = false;
	double dWidth, dHeight;
	static VideoCapture cap;
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

		if (showImages == 1){
			namedWindow("MyVideo", CV_WINDOW_AUTOSIZE); //create a window called "MyVideo"
		}
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

	cv::Mat deskewedFrame = Mat::zeros(frame.rows, frame.cols, frame.type());
	deskew(frame, deskewedFrame);
	
	//Point coinCenter = CoinCenter(deskewedFrame, showImages);
	//cv::Mat crop = Mat::zeros(frame.rows, frame.cols, frame.type());


	imwrite("F:/OpenCV/" + std::to_string(imageID) + ".jpg", deskewedFrame);
	
	if (showImages == 1){
		imshow("MyVideo", deskewedFrame);

		if (waitKey(1) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			destroyWindow("MyVideo");
			return 0;
		}
	}

	return 0;
}

