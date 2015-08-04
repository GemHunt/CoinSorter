# CoinSorter
Sorts coins by solenoid on a conveyor by classifying images with Caffe &amp; DIGETS

This group of programs and scripts is just a quick dirty prototype to show physical sorting.

CoinSorter Project:
This program watches a directory for images to appear. 
The image is sent to DIGITS (The Nivida Caffe front end) for classification.
Finally a solenoid turns on for coins that meet a certain criteria.
Written in VB.Net v4.5 Framework. 

CanonSDKTutorial-noexe:
Originally created by Johannes Bildstein at:
http://www.codeproject.com/Articles/688276/Canon-EDSDK-Tutorial-in-Csharp
Changes:
Save LiveView captaures 
Call a MATLAB script to crop the coins to 256x256 images
I did not include the Canon SDK DLLs. 



