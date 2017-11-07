```
#This is handy, you can rebuild caffe to run it in CPU only mode.
#This works great for classifying or training small networks such as LeNet
cd ~/caffe 
vi Makefile.config
change to:
#USE_CUDNN := 1
CPU_ONLY := 1

make clean
make all -j8
sudo make py
```