Following WindowsCaffeInstall.md

I know there has to be a better way then this! I have seen advice saying writing the network yourself is not hard, but for now I need the features of Caffe. 


* Replace D:\GitHub\build\DownloadCache\caffe\examples\cpp_classification\classification.cpp
with classification.cpp from this directory. 
* Open build\Caffe-prefix\src\Caffe-build in Visual Studio 2013
* Open the properties of examples-> classification project
* In the general configuration properties change the Target extension to .dll and the configuration type to Dynamic Library(.dll) 
* Build



