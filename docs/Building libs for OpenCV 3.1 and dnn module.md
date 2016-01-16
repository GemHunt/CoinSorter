This is not done yet! See "Next Steps" below. 

Building libs for OpenCV 3.1 and dnn module
Instructions for build the libraries for the OpenCV v3.1 on CUDA on Visual Studio 2013 Community edition. 
This is a massive build, required 12GB and 2 hours of build time. 
Most of what is built is not needed. 
When I finish this the libraries in opencv_root/build_opencv/lib will be available for download on GemHunt.com 
Anyone should be able to build the same libraries with these instructions. 


Starting with these directions:
http://docs.opencv.org/master/de/d25/tutorial_dnn_build.html#gsc.tab=0

Step 1: 
I downloaded the stable 3.1 release source code Zip files instead of cloning the current master as this tutorial says to:
git clone https://github.com/Itseez/opencv
git clone https://github.com/Itseez/opencv_contrib

Step 4 
“Visual Studio 12 2013 WIN 64” is correct for 2013 community edition as is what I have been using. 
Clicking finish in the preferred project generator form takes a minute or two. 
Step 5:
Search “ext”, uncheck grouped, check Advanced
Step 6:
 Opencv_dnn_BUILD_TORCH_IMPORTER also checked

Step 8: 
	Visual Studio too 2 hours to build this. HDF5 currently failing for me. 

I got this working:
tests accuracy-> opencv_test_dnn to test the sample files at https://github.com/Itseez/opencv_contrib

Next steps:
Use the libraries created from this in a seperate solution to classify a Caffe model
Replace the current CoinSorter Windows Caffe build functionality in this new solution using these new libraries. 


Latter: 
HDF5 & CuDNN should be in this, but I am skipping them until I can get the dnn module working to replace the Windows Caffe build. 
HDF5 is failing in my build. 
I don’t know how to include CuDNN. I put CuDNN files for now go into: C:\Program Files (x86)\NVIDIA Corporation\cuda\include
