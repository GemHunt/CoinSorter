I have used this many times at this point:

1.) I(Paul Krush) started with this, by Raffael Vogler:
GPU Powered DeepLearning with NVIDIA DIGITS on EC2:
http://www.joyofdata.de/blog/gpu-powered-deeplearning-with-nvidia-digits/

2.) I rebuilt for Digits 2.

3.) I then rebuilt for cudnn3 and Nidvia Caffe v0.13.0 
	This one took me about 2 hours
	
4.) Then I built a local server. (9/22/2015)
	MSI H81M-E33 motherboard, Intel G3258 Processor, 8GB of Ram, 250GB SSD, GTX 970
	It really was the same as AWS except for installing ubuntu 14.04
	Changes AWS or Local:
		cuDnn is now "prod" (not rc) 
		cuda 7.5 is now installing by default
		udo was needed before Caffe requirements
5.) Rebuilt the same hardware (6/9/2016)  	
	ubuntu 14.04(Still recommended by Nvidia), CUDA 8RC, CuDnn 5.0 
	Digits is claiming to be 4.0.0-rc.1.
	So this updated to new versions, got the box ready for pascal cards (4 gtx 1080's or 1070's for me) 
	Also I uncomment "WITH_PYTHON_LAYER:= 1 so I can use this option in DIGITS	

I think Nivida is saying they have an web-installer that does better. I don't see how?

I use ModaXterm(Free) on Windows instead of Putty because there was some sort of issue with 
	DIGITS needing an X-Server. 
*************************************************************************
```
#This is written as a script, but I don't see how you could run it as one...
#There is just too many things that change and can go wrong. 
#I just copy and paste one or more lines at a time. 
#But I am just a beginner unix user...

#To further speed up deep learning relevant calculations 
#it is a good idea to set up the cuDNN library.
#For that purpose you will have to get an NVIDIA developer account
#and join the CUDA registered developer program. 
#The last step requires NVIDIA to unlock your account 
#and that might take hours or 1-2 days.
#But you can get started also without cuDNN library. 
#As soon as you have the okay from them – download cuDNN and upload it to your instance.

#Paul's Comment:  If you going through this much trouble, 
#I would just register as a developer and download CuDnn first. 


#FTP Install Notes
#FTP is not required, but I think it's handy. 
#There are all sorts of examples of using vsftpd on AWS, but they all a bit different. 
#This is what works for me, just to get things started. 
#This is just for temporary instance that only I would be using. 

#AWS Security group changes: 
#Make sure 20-21 and 1024-1048 is open to your IP
cd ~
sudo apt-get update
sudo apt-get dist-upgrade
sudo apt-get install vsftpd

sudo vi /etc/vsftpd.conf
#Add the following lines to the bottom of the vsftpd.conf file:
#Change pasv_address to the Public IP of the AWS Instance:
pasv_address=54.157.147.8
pasv_enable=YES
pasv_min_port=1024
pasv_max_port=1048
port_enable=YES
pasv_addr_resolve=YES
write_enable=YES

#For local servers just these lines needed to be enabled:
write_enable=YES
local_umask=022

sudo service vsftpd restart
cd ~

#I put chrome on local machines:
sudo apt-get install libxss1 libappindicator1 libindicator7
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
sudo dpkg -i google-chrome*.deb

#Installing CUDA 8RC
#Installation of required tools
#A menu pops up, and I just tell it to keep the local version:
sudo apt-get install -y gcc g++ gfortran build-essential \
  git wget linux-image-generic libopenblas-dev python-dev \
  python-pip python-nose python-numpy python-scipy
 
#download CUDA 8:
Download cuda-repo-ubuntu1404-8-0-local-ga2_8.0.61-1_amd64.deb from the NVidia site (You have to register)
# install CUDA 8:
sudo dpkg -i cuda-repo-ubuntu1404-8-0-local-ga2_8.0.61-1_amd64.deb
sudo apt-get update
sudo apt-get install cuda

# setting the environment variables so CUDA will be found
echo -e "\nexport PATH=/usr/local/cuda/bin:$PATH" >> .bashrc
echo -e "\nexport LD_LIBRARY_PATH=/usr/local/cuda/lib64" >> .bashrc
 
sudo reboot
 
# installing the samples and checking the GPU
cuda-install-samples-8.0.sh ~/
cd NVIDIA\_CUDA-8.0\_Samples/1\_Utilities/deviceQuery  
make 
./deviceQuery

#At this point deviceQuery should have given you a bunch of information about the GPU(s?) and passed tests. 

# Install CuDNN v6
#You can't just wget the file
#You have to register as a developer and manually download. 
#This is why FTP was the step before this. 
#So first FTP the cudnn-8.0-linux-x64-v6.0.tgz file to home

gzip -d cudnn-8.0-linux-x64-v6.0.tgz
tar xf cudnn-8.0-linux-x64-v6.0.tgz

# copy the library files into CUDA's include and lib folders
sudo cp cuda/include/cudnn.h /usr/local/cuda-8.0/include
sudo cp cuda/lib64/libcudnn* /usr/local/cuda-8.0/lib64


#Installing caffe
#Main source for this and the following step is the readme of the DIGITS project.
sudo apt-get install udo

sudo apt-get install libprotobuf-dev libleveldb-dev \
  libsnappy-dev libopencv-dev libboost-all-dev libhdf5-serial-dev \
  libgflags-dev libgoogle-glog-dev liblmdb-dev protobuf-compiler \
  libatlas-base-dev
 
sudo apt-get install python-dev python-pip python-numpy gfortran

#Install Git:
sudo apt-get install git

# consult https://github.com/NVIDIA/DIGITS/blob/master/README.md
git clone https://github.com/NVIDIA/caffe.git   
 
#This takes a long time,(10-30+ minutes)
cd ~/caffe/python
for req in $(cat requirements.txt); do sudo pip install $req; done
 
cd ~/caffe
cp Makefile.config.example Makefile.config
 
# In Makefile.config uncomment USE_CUDNN :1 
# Also I uncomment "WITH_PYTHON_LAYER:= 1 so I can use this option in digits
vi Makefile.config

make all
make py
make pycaffe   (It says nothing to be done?)
make test
#This runs a large number of Caffe tests:
make runtest

echo -e "\nexport CAFFE_HOME=~/caffe" >> ~/.bashrc
#Load the new environmental variables
bash

Install Torch:
https://github.com/NVIDIA/DIGITS/blob/master/docs/BuildTorch.md

Installing DIGITS
cd ~
DIGITS_HOME=~/digits
#Using currrent build:
git clone https://github.com/NVIDIA/DIGITS.git $DIGITS_HOME
#I backed up to 4.0 release because an issue with the current build:
#git clone --branch digits-4.0  https://github.com/NVIDIA/DIGITS.git $DIGITS_HOME

#If you don't want the master this is an example of cloning a branch:
#git clone -b dev/lmdb-inference --single-branch https://github.com/gheinrich/DIGITS.git digits

cd digits
sudo apt-get install graphviz gunicorn

for req in $(cat requirements.txt); do sudo pip install $req; done

####I added this line because the pycaffe module was not being found in Digits:
####OK, I took this out and added:  make pycaffe above
####echo 'export PYTHONPATH=/home/ubuntu/caffe/python' >> ~/.bashrc 
####bash 

cd ~

# You might want to consider locating your job-directory ( jobs_dir) on an EBS 
# See https://github.com/NVIDIA/DIGITS/blob/master/docs/GettingStarted.md for details. 
# I set the the jobs_dir at /data/digits/jobs by using the --config option:
#./digits/digits-devserver --config

# start the server
./digits/digits-devserver

#To use opencv in python scripts:
sudo apt-get install python-opencv

#AWS Security group changes: 
#Make sure port 5000 is open to your IP

#To view DIGITS go to port 5000 the public IP of your instance:
#Example:  I pasted 50.16.43.125:5000 in my browser. 


#Mounting a new EBS Volume
#WARNING! Only format a new volume:
#sudo mkfs -t ext4 xvdf
#Create a mount point:
#sudo mkdir /data
#Mount the new volume
#sudo mount /dev/xvdf /data
#Add permissions:
#sudo chmod 777 /data
```
