Dataset Definition:
A set of images documented in a standard way that someone can repeat it from scratch.
When I (Paul) create datasets I will post GitHub issues on them first. 

Each dataset will have:
Captured and cropped images
A markdown file detailing how it was created
Its own directory
A file of the image names and ground truth 
URL(s) where the images can be found. 

Dataset description file:
The hash of the code used. For example
https://github.com/GemHunt/CoinSorter/commit/cc747544cb067c74f2c6b2df1c97b827d6a86a1e
System and hardware details such as 
Camera & Lens settings 
Hopper Type
Conveyors

Directory Naming 
Some physically different coins can be separated:
/data/dataSetName/train
/data/dataSetName/Val

A dataset is not a model. Models can use images from many datasets. 