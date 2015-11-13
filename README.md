# CoinSorter

High Speed Sorting of Pennies by Type and Date

Sorts coins by solenoid on a conveyor by classifying images with [Caffe](https://github.com/NVIDIA/caffe) &amp;  [DIGITS](https://github.com/NVIDIA/DIGITS)

To understand where this system is heading in the short term see the [2nd Proof of Concept issues](https://github.com/GemHunt/CoinSorter/milestones/2nd%20Proof%20of%20Concept) and the [Prototype Coinsorter Issues](https://github.com/GemHunt/CoinSorter/milestones/Portable%20Prototype%20Coin%20Sorter)

Check out the [starting point issues](https://github.com/GemHunt/CoinSorter/labels/starting%20point) to see what is currently being worked on.

The first version of this system used a C# project to capture images from a Canon Rebel camera and called MATLAB to preprocess them. A VB project was used to call DIGITS to classify the images and call a HP Power supply to drive a solenoid. These two projects have now been replaced. 

The second verion is almost running. It uses C#, OpenCV, a webcam, Arduino solenoid control, and local classification with Caffe on Windows 10. [Here is a poster that describes the 2nd version.](https://github.com/GemHunt/CoinSorter/blob/master/docs/GTC%20Poster.pdf)

This group of programs and scripts is just a quick proof of concept to show physical coin sorting. It "was" sorting about 2 pennies a second, continuously. One solenoid and 2 physical bins are currently set up. Using Caffe it’s easy to distinguish between designs of coins. For example you can train a convolutional neural network (CNN, what Caffe uses) to determine if a coin image is heads vs tails or say recognize the state on a random US state quarter image. Using the "copper" image set out on [GemHunt.com](http://www.gemHunt.com) Caffe can tell heads vs tails between US copper pennies 99.9% of the time. This can be done using using [DIGITS](https://github.com/NVIDIA/DIGITS) with default setting of AlexNet with no programming involved!  

[Click to watch a video of it in action:](http://www.youtube.com/watch?feature=player_embedded&v=_fJcIxWgQbs)
<a href="http://www.youtube.com/watch?feature=player_embedded&v=_fJcIxWgQbs" target="_blank"><img src="http://img.youtube.com/vi/_fJcIxWgQbs/0.jpg" alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>

[View parts list and documentation](/hardware/conveyors/conveyors.md) for the conveyors and the [CAD files](/hardware/conveyors/). 

For a long term project direction [check out the issues for future milestones.](https://github.com/GemHunt/CoinSorter/milestones/Future%20Milestones) 

Feel free to contact me if you have questions about this project. 

Thanks!  

Paul Krush

pkrush@Gemhunt.com

1-630-830-6640
