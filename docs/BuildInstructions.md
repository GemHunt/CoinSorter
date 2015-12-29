As far as I know no one has built this on any computer other than my workstation. 

At this point just call me if you want to try to build it! 1-630-830-6640 I will try to work you through it. 


Tasks to make a build easier:

* Remove hardcoded directories, and add a configuration file
* Add a fake camera, solenoid, and raw image database so the software can be run without any special hardware
* Try to build on a different machine and document this
* Build again on AWS and make a public AMI for this
* Finish BuildInstructions.md

* Add a note to the README; something like this:

*Please fork me: This system is at the point that you could build the hardware and do this yourself. Most likely you will have to build new models from your camera’s images. Also, there is a test mode that you can use the system without a camera or solenoid controller. You should be to clone or fork release 0.2 and get this system to work with the release 0.2 images and models out on GemHunt.com. It’s rather difficult to compile this, as there is a huge C++ DLL project and a C# project.  Click here for build instructions. Or you cheat: I built this already on AWS and made a Public Windows AMI. Also click here for the Linux Training Server AMI.*