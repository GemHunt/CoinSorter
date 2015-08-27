# CoinSorter

Sorts coins by solenoid on a conveyor by classifying images with [Caffe](https://github.com/NVIDIA/caffe) &amp;  [DIGITS](https://github.com/NVIDIA/DIGITS)

To understand where this system is heading in the short term see the [prototype CoinSorter's issues](
https://github.com/GemHunt/CoinSorter/milestones/Prototype%20CoinSorter).

This group of programs and scripts is currently just a quick proof of concept to show physical coin sorting. It's currently sorting about 2 pennies a second, continuously. One solenoid and 2 physical bins are currently set up. Using Caffe it’s easy to distinguish between designs of coins. For example you can train a convolutional neural network (CNN, what Caffe uses) to determine if a coin image is heads vs tails or say recognize the state on a random US state quarter image. Using the "copper" image set out on [GemHunt.com](http://www.gemHunt.com) Caffe can tell heads vs tails between US copper pennies 99.9% of the time. This can be done using using [DIGITS](https://github.com/NVIDIA/DIGITS) with default setting of AlexNet with no programming involved!  

[Click to watch a video of it in action:](http://www.youtube.com/watch?feature=player_embedded&v=_fJcIxWgQbs)
<a href="http://www.youtube.com/watch?feature=player_embedded&v=_fJcIxWgQbs" target="_blank"><img src="http://img.youtube.com/vi/_fJcIxWgQbs/0.jpg" alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>

[View parts list and documentation](/hardware/conveyors/conveyors.md) for the conveyors and the [CAD files](/hardware/conveyors/). 

For a long term project direction [check out the issues for future milestones.](https://github.com/GemHunt/CoinSorter/milestones/Future%20Milestones) 

#CoinSorter Project:
* This program watches a directory for images to appear
* The image is sent to DIGITS (The Nivida Caffe front end) for classification
* Finally a solenoid turns on for coins that meet a certain criteria

#CanonSDKTutorial-noexe:
* Saves image captures to file and calls a MATLAB script to crop the coins to 256x256 images
* Originally published by Johannes Bildstein on [CODE PROJECT](http://www.codeproject.com/Articles/688276/Canon-EDSDK-Tutorial-in-Csharp)

This project is project is brought to you by:

[GemHunt.com](http://www.GemHunt.com): helping people learn robotics, machine vision, and deep learning while providing DIY small part handling systems. 

Thanks!  
Paul Krush
