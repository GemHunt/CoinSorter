# CoinSorter
Sorts coins by solenoid on a conveyor by classifying images with [Caffe](https://github.com/NVIDIA/caffe) &amp; [DIGITS](https://github.com/NVIDIA/DIGITS)

This group of programs and scripts is just a quick proof of concept to show physical coin sorting. It's currently sorting about 2 pennies a second, continuously. With only one solenoid only 2 physical bins are are currently set up. Using Caffe it’s easy to distinguish between designs of coins. For example you can train a convolutional neural network (CNN, what Caffe uses) to determine if a coin image is heads vs tails or say recognize the state on a random US state quarter image. Using the "copper" image set out on [GemHunt.com](http://www.gemHunt.com) Caffe can tell heads vs tails between US copper pennies 99.9% of the time. This can be done using using [DIGITS](https://github.com/NVIDIA/DIGITS) with default setting of AlexNet with no programming involved! 



**The goal is a open low cost system / kit that can:**
* handle, image, inspect, count, sort, and position small parts.
* with local or remote control.

**For example a coin collector could one day configure the system to:**
* semi-automatically inspect coins remotely,
* in his basement at home, 
* from his smart phone,
* while he is waiting in line at the bank,
* to dump the last batch of coins he searched. 

There is clear demand for a coin sorting tool, but it’s not just people interested in coins that would use this.  Since everyone one understands what coins are, coins a great "small part" to demonstrate what a low cost system like this can do. 

#CoinSorter Project:
This program watches a directory for images to appear. 
The image is sent to DIGITS (The Nivida Caffe front end) for classification.
Finally a solenoid turns on for coins that meet a certain criteria.
Written in VB.Net v4.5 Framework. 

#CanonSDKTutorial-noexe:
The image capture program
Originally created by Johannes Bildstein at:
http://www.codeproject.com/Articles/688276/Canon-EDSDK-Tutorial-in-Csharp

Changes:
* Saves LiveView captures 
* Calls a MATLAB script to crop the coins to 256x256 images
* I did not include the Canon SDK DLLs. 

To better understand where this system is heading read the Prototype CoinSorter Milestone description:
https://github.com/GemHunt/CoinSorter/milestones
and it's issues:
https://github.com/GemHunt/CoinSorter/milestones/Prototype%20CoinSorter

This project is project is brought to you by:

GemHunt.com: a for profit company helping people learn robotics, machine vision, and deep learning while providing DIY small part handling systems. 

Thanks!  
Paul Krush




