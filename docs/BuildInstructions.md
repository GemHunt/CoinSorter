I have been posting code for over 3 months here and I don’t think this project has ever been complied anywhere other than my workstation. It’s getting to the point that this system is doing something useful. 

**Tasks:**
* Remove hardcoded directories, and add a configuration file
* Bring the model configurations into the repository
* Add a fake camera and solenoid so the software can be run without any special hardware
* Put images and models on GemHunt.Com to correspond to the 0.2 release point
* Try to build on a different machine and document this
* Build again on AWS and make a public AMI for this
* Finish BuildInstructions.md
* Make a 0.2 release point
* Add a note to the README; something like this:
*Please fork me: This system is at the point that you could build the hardware and do this yourself. Most likely you will have to build new models from your camera’s images. Also, there is a test mode that you can use the system without a camera or solenoid controller. You should be to clone or fork release 0.2 and get this system to work with the release 0.2 images and models out on GemHunt.com. It’s rather difficult to compile this, as there is a huge C++ DLL project and a C# project.  Click here for build instructions. Or you cheat: I built this already on AWS and made a Public Windows AMI. Also click here for the Linux Training Server AMI.*