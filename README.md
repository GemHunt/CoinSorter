# CoinSorter
Sorts coins by solenoid on a conveyor by classifying images with Caffe &amp; DIGETS

A seperate program controls the hopper, conveyor, and camera. It creates images on disk. 
This program watches a directory for images to appear. 
The image is sent to DIGITS (The Nivida Caffe front end) for classification.
Finally a solenoid turns on for coins that meet a certain criteria.

It's just a quick dirty prototype to show physical sorting.

It's written in VB.Net v4.5 Framework. 
