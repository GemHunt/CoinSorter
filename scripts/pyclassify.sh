python classify.py /data/1982heads/1982D 

python ~/caffe/python/classify.py --model_def /data/models/HeadsWithRotation360/deploy.prototxt --pretrained_model /data/models/HeadsWithRotation360/snapshot_iter_94688.caffemodel /data/1982heads/1982D/1060015001030763.png foo



 python ~/digits/examples/classification/example.py /data/models/HeadsWithRotation360/snapshot_iter_94688.caffemodel /data/models/HeadsWithRotation360/deploy.prototxt /data/1982heads/1982D/1060015001030763.png