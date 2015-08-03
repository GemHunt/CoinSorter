# CoinSorter
Sorts coins by solenoid on a conveyor by classifying images with Caffe &amp; DIGETS

This program sorts coins on a conveyor belt. 
The program watches a directory for images to appear. 
The image is sent to DIGITS(The Nivida Caffe front end) for classification.
Finally a solenoid turns on for coins that meet a certain criteria.

It's just a quick dirty prototype to show physical sorting,
    after the coin is classified by Caffe.

It's written in VB.Net v4.5 Framework. 
