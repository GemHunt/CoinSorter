#You have to rebuild caffe to run it in CPU only mode. 
cd ~/caffe 
vi MakeFile.config
#Change CPU_ONLY to 0

make clean
make all -j8
sudo make py
