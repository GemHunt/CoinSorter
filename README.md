# CoinSorter
Sorts coins by solenoid on a conveyor by classifying images with Caffe &amp; DIGETS

This group of programs and scripts is just a quick proof of concept to show physical coins sorting. It's currently sorting about 2 pennies a second, continuously. With only one solenoid only 2 physical bins are are currently set up. 

**The end goal is a low cost system / kit that can:**
* handle, inspect, count, sort, and position small parts.
* with local or remote control.

**For example a coin collector could use this to:**
* semi-automatically sort coins remotely,
* in his basement at home, 
* from his smart phone,
* while he is waiting in line at the bank,
* to dump the last batch of coins he searched. 

There is clear demand for a coin sorting tool and it’s not just people interested in coins. Since everyone one understands what coins are, it’s a great part to demonstrate what a low cost system like this can do. 

#CoinSorter Project:
This program watches a directory for images to appear. 
The image is sent to DIGITS (The Nivida Caffe front end) for classification.
Finally a solenoid turns on for coins that meet a certain criteria.
Written in VB.Net v4.5 Framework. 

#CanonSDKTutorial-noexe:
Originally created by Johannes Bildstein at:
http://www.codeproject.com/Articles/688276/Canon-EDSDK-Tutorial-in-Csharp
Changes:
Save LiveView captaures 
Call a MATLAB script to crop the coins to 256x256 images
I did not include the Canon SDK DLLs. 



Milestones coming soon...

