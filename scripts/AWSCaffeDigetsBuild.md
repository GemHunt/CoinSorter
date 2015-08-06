1.) I started with this, by Raffael Vogler:
GPU Powered DeepLearning with NVIDIA DIGITS on EC2:
http://www.joyofdata.de/blog/gpu-powered-deeplearning-with-nvidia-digits/

2.) I rebuilt for Digits 2.

3.) I am just about to rebuild for Cuda 7.5/cudnn3/Nidvia Caffe v0.13.0 
Nivida is saying they have an installer that does better. I have to check this out. 


Current, but useless notes:  ***************************************************************************
# don't forget to get your system up to date
sudo apt-get update
sudo apt-get dist-upgrade
Installing CUDA 7
Main source for this step is Markus Beissinger’s blog post on setting up Theano.
 

# installation of required tools
sudo apt-get install -y gcc g++ gfortran build-essential \
  git wget linux-image-generic libopenblas-dev python-dev \
  python-pip python-nose python-numpy python-scipy
 
# downloading the (currently) most recent version of CUDA 7
sudo wget http://developer.download.nvidia.com/compute/cuda/repos/ubuntu1404/x86_64/cuda-repo-ubuntu1404_7.0-28_amd64.deb
 
# installing CUDA
sudo dpkg -i cuda-repo-ubuntu1404_7.0-28_amd64.deb
 
sudo apt-get update
sudo apt-get install cuda
 
# setting the environment variables so CUDA will be found
echo -e "\nexport PATH=/usr/local/cuda/bin:$PATH" >> .bashrc
echo -e "\nexport LD_LIBRARY_PATH=/usr/local/cuda/lib64" >> .bashrc
 
sudo reboot
 
# installing the samples and checking the GPU
cuda-install-samples-7.0.sh ~/
cd NVIDIA\_CUDA-7.0\_Samples/1\_Utilities/deviceQuery  
make  ./deviceQuery
Installing cuDNN
To further speed up deep learning relevant calculations it is a good idea to set up the cuDNN library. For that purpose you will have to get an NVIDIA developer account and join the CUDA registered developer program. The last step requires NVIDIA to unlock your account  and that might take one or two days. But you can get started also without cuDNN library. As soon as you have the okay from them – download cuDNN and upload it to your instance.
 

# unpack the library

*A new 3.0 multi-GPU version is coming out, it might be better to use this next. 

sudo wget www.inlay.com/cudnn-6.5-linux-x64-v2.tgz   *I changed this
gzip -d cudnn-6.5-linux-x64-v2.tgz  *I changed this
tar xf cudnn-6.5-linux-x64-v2.tar
 
# copy the library files into CUDA's include and lib folders
sudo cp cudnn-6.5-linux-x64-v2/cudnn.h /usr/local/cuda-7.0/include
sudo cp cudnn-6.5-linux-x64-v2/libcudnn* /usr/local/cuda-7.0/lib64
Installing caffe
Main source for this and the following step is the readme of the DIGITS project.
 

sudo apt-get install libprotobuf-dev libleveldb-dev \
  libsnappy-dev libopencv-dev libboost-all-dev libhdf5-serial-dev \
  libgflags-dev libgoogle-glog-dev liblmdb-dev protobuf-compiler \
  libatlas-base-dev
 
# the version number of the required branch might change
# consult https://github.com/NVIDIA/DIGITS/blob/master/README.md
git clone --branch v0.12.0 https://github.com/NVIDIA/caffe.git   *I used 12 for multi -GPU
 
cd ~/caffe/python
for req in $(cat requirements.txt); do sudo pip install $req; done
 
cd ~/caffe
cp Makefile.config.example Makefile.config
 
# check that USE_CUDNN is set to 1 in case you would
# like to use it and to 0 if not
 
make all
make py
make test
make runtest
 
echo -e "\nexport CAFFE_HOME=/home/ubuntu/caffe" >> ~/.bashrc
 
# load the new environmental variables
Bash

*** What is that bash by itself do?

Installing DIGITS
 

	cd ~
git clone https://github.com/NVIDIA/DIGITS.git digits   *the master branch is now  2.0
cd digits
sudo apt-get install graphviz gunicorn
for req in $(cat requirements.txt); do sudo pip install $req; done
•	I added this line because the pycaffe module was not being found in Digits:
echo 'export PYTHONPATH=/caffe/python' >> ~/.bashrc 



Starting and Configuring DIGITS
The first time you start DIGITS it will ask you number of questions for the purpose of its configuration. But those settings are pretty much self-explanatory and you can change them afterwards in ~/.digits/digits.cfg . You might want to consider locating your job-directory ( jobs_dir) on an EBS – the data set of about 140’000 PNGs in the example I feature here consumes about 10 GB of space and the trained models (with all its model snapshots) accounts for about 1 GB.
 

# change into your digits directory
cd digits
 
# start the server
./digits-devserver
