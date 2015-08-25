# CoinSorter

Sorts coins by solenoid on a conveyor by classifying images with [Caffe](https://github.com/NVIDIA/caffe) &amp;  [DIGITS](https://github.com/NVIDIA/DIGITS)

This group of programs and scripts is just a quick proof of concept to show physical coin sorting. It's currently sorting about 2 pennies a second, continuously. With only one solenoid only 2 physical bins are are currently set up. Using Caffe it’s easy to distinguish between designs of coins. For example you can train a convolutional neural network (CNN, what Caffe uses) to determine if a coin image is heads vs tails or say recognize the state on a random US state quarter image. Using the "copper" image set out on [GemHunt.com](http://www.gemHunt.com) Caffe can tell heads vs tails between US copper pennies 99.9% of the time. This can be done using using [DIGITS](https://github.com/NVIDIA/DIGITS) with default setting of AlexNet with no programming involved!  

![Image of Conveyor 5](/hardware/conveyors/5small.jpg) 

To understand where this system is heading in the short term see the [prototype CoinSorter's issues](
https://github.com/GemHunt/CoinSorter/milestones/Prototype%20CoinSorter).

[View parts list and documentation](/hardware/conveyors/conveyors.md) for the conveyors and the [CAD files](/hardware/conveyors/). 

#CoinSorter Project:
* This program watches a directory for images to appear
* The image is sent to DIGITS (The Nivida Caffe front end) for classification
* Finally a solenoid turns on for coins that meet a certain criteria

#CanonSDKTutorial-noexe:
* Saves image captures to file and calls a MATLAB script to crop the coins to 256x256 images
* Originally published by Johannes Bildstein on [CODE PROJECT](http://www.codeproject.com/Articles/688276/Canon-EDSDK-Tutorial-in-Csharp)

This project is project is brought to you by:

GemHunt.com: a for profit company helping people learn robotics, machine vision, and deep learning while providing DIY small part handling systems. 

Thanks!  
Paul Krush
