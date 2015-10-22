How to build github.com/willyd/caffe-builder CPU ONLY

* Review caffe-builder/Readme.md  (Known issues!!!)
* Be sure to have at least 8.5GB free, it's huge. 
* Clone https://github.com/willyd/caffe-builder: 
* Open the CMAKE GUI
* Enter your source(the clone directory) and build directorys. 
* Click "Configure"
* You might have to Configure more then once because of download issues. 
* Make sure your current generator is "Visual Studio 12 1013 Win64"
* Make CPU_ONLY change (build\DownloadCache\caffe\CMakeLists.txt):
	caffe_option(CPU_ONLY  "Build Caffe without CUDA support" OFF) # TODO: rename to USE_CUDA
	to	
	caffe_option(CPU_ONLY  "Build Caffe without CUDA support" ON) # TODO: rename to USE_CUDA
* Then Generate
* Open build\caffe-builder.sln in VS 2013 and build


**To build DLL:**
* Replace build\DownloadCache\caffe\examples\cpp_classification\classification.cpp with classification.cpp from this directory. 
* Copy the rest of the *.cpp files from this directory as well. 
* Open build\Caffe-prefix\src\Caffe-build\Caffe.sln in Visual Studio 2013  (this is a different solution from above)
* Open the properties of examples-> classification project
* In the general configuration properties change the Target extension to .dll and the configuration type to Dynamic Library(.dll) 
* Build the examples-> classification project


**Notes**
Moving these *.cpp files into a directory created by the caffe-builder is just weird to me, but it's clean for now. There is a batch file to do this. I really don't know the correct way of doing this. It's a fork, but caffe-builder is so massive, and it's a fork of many projects. 




