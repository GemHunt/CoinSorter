# CoinSorter

CoinSorter is a low cost, open source, coin inspection system. It sorts coins by solenoid, on conveyors, by classifying images with deep learning based models using [Caffe](https://github.com/BVLC/caffe), OpenCV, Python, LMDB, and Arduino. The system uses an algorithm in which coin designs and features can be found with 1-5 labeled examples. This is done by augmenting the training image set with many different camera and lighting angles. 

[![Check out the video](https://github.com/GemHunt/lighting-augmentation/blob/master/youtube.jpg)](https://www.youtube.com/watch?v=cwcn3tuXrv4)

**CoinSorter is the main repo. The project has many sub-repos:**
* Conveyors: [conveyors repo](https://github.com/GemHunt/conveyors)
* Lighting: [lighting-augmentation repo](https://github.com/GemHunt/lighting-augmentation)
* Hopper Metering: [hopper-metering-tool repo](https://github.com/GemHunt/hopper-metering-tool)
* Inital Scanning & Operation: [real-time-coin-id repo](https://github.com/GemHunt/real-time-coin-id)
* SAIL: Unsupervised Labeling: [sail repo](https://github.com/GemHunt/sail)
* Caffe fork needed for SAIL: [caffe repo](https://github.com/GemHunt/caffe)

**The Goal of CoinSorter is to be a:**

* Image collection system for a stand-alone automated coin grading & counterfeit detection app
* High speed coin scanning, inspection, and sorting system
* Tool for finding rare coins
* Study tool for the classification of images using deep learning.
* System costing less than $200 as a kit excluding the cost of the computer and optional GPU card and less than $50 in a quantity of 20 systems provided you have free access to a laser cutter.
* Low cost, real world example of an open high speed machine vision system that can be used as a starting point for handling, inspection, and manufacture of other small parts.

|Clear Belt & Cameras Removed| Scanning with a Center Cutout| Top and Bottom Camera Closeup|
| --- | --- | --- |
|![Sorting](https://github.com/GemHunt/CoinSorter/blob/master/hardware/old/conveyors/scanning/SortingSmall.jpg "Sorting")|![Sorting](https://github.com/GemHunt/CoinSorter/blob/master/hardware/old/conveyors/scanning/2CameraScanningSmall.jpg "Sorting")|![Sorting](https://github.com/GemHunt/CoinSorter/blob/master/hardware/old/conveyors/scanning/2CameraCloseUpSmall.jpg "Sorting")|

**Milestones & Short term goals & tasks:**
* GitHub milestones and issues are the best place to understand the project's past, present, and future. 
* In general the short term goal is to [sort 2 pennies a second by type, date and mintmark.](https://github.com/GemHunt/CoinSorter/issues/57)  
* Check out the [starting point issues](https://github.com/GemHunt/CoinSorter/labels/starting%20point) to see what is currently being worked on.
* For long term project direction [check out the issues for future milestones.](https://github.com/GemHunt/CoinSorter/milestones/Future%20Milestones) 

**Usage:**
* A new Python version is being planned.
* All C# code is being dropped from the project and will no long be maintained.   
* The new conveyors need to be documented and linked here, but in general everything is in [one Rhino 3D file.](https://github.com/GemHunt/CoinSorter/blob/master/hardware/conveyors/endMount/endMount.3dm)
* [View the old parts list and documentation](https://github.com/GemHunt/conveyors)

**How to Contribute & Participate**
* Buy and build the kit yourself. (I need to post the kit on eBay...)
* Work on, comment, and post new [issues.](https://github.com/GemHunt/CoinSorter/issues)
* If you have a laser cutter feel free to to build and sell systems yourself.
* If your into Caffe and don't want to post, just call me at 630-830-6640.

**Past Progress:**
* 2 working proof of concepts have been built proving Caffe & DIGITS is an excellent choice for coin image classification using small fully connected LeNet style networks.
* Over 20 prototype conveyors have been build each improving on the last design with around 150,000 coins imaged so far. 
* The full hardware setup is mature enough that others can start building and using it. 

**Detailed History:**
* The first proof of concept of this system used a C# project to capture images from a Canon Rebel camera and called MATLAB to preprocess them. A VB project was used to call DIGITS to classify the images and call a HP Power supply to drive a solenoid. These two projects have now been replaced. 

* The second proof of concept used C#, OpenCV, a webcam, Arduino solenoid control, and local classification with Caffe on Windows 10. Here is a [poster](https://github.com/GemHunt/CoinSorter/blob/master/docs/GTC%20Poster.pdf) and a [Power Point](https://github.com/GemHunt/CoinSorter/blob/master/docs/Deep%20Learning%20with%20Caffe%20%26%20DIGITS%20for%20Robotic.pptx) that describes the 2nd version. [You can download the second proof of concept release here.](https://github.com/GemHunt/CoinSorter/releases/tag/v0.2) 

* This first two groups of programs and scripts were just a quick proof of concept to show physical coin sorting. They sorted about 2 pennies a second, continuously. One solenoid and 2 physical bins are currently set up. Using Caffe it’s easy to distinguish between designs, orintation, and dates of coins. For example you can train a convolutional neural network (CNN, what Caffe uses) to determine if a coin image is heads vs tails or say recognize the state on a random US state quarter image. On one of the first models that was built Caffe could tell heads vs tails between US copper pennies 99.9% of the time. This can be done using using [DIGITS](https://github.com/NVIDIA/DIGITS) with default setting of AlexNet with no programming involved! In practice it's more efficient to use smaller image sizes and optimized networks. 

**In Review:**
* On the surface this system may look toy like and have a very narrow focus, but this is not true at all. You can take the basics of this system and use it for all sorts of very practical industrial uses. It’s not just sorting a handful of coins. It scales very quickly to tons of coins (or any parts for that matter). I have no doubt the system will get to the thousands of users range and be used for uses I could never envision. 

* Nothing remotely like it exists that is very low cost or open source. There are undocumented one off builds for all kinds of part handling. Probably the closest thing would be the open source pick and place machines. I have yet to see any personal or open source part handling systems that use the current crop of deep learning tools. MakerBot did have a conveyor on one of their machines, but this was a blind setup. Please let me know if you know about other, complete or not, documented open hardware machine vision systems

**Feel free to contact me if you have questions about this project.**

Thanks!  

Paul Krush

pkrush@Gemhunt.com

1-630-830-6640
