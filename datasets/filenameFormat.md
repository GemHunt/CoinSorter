Capture File Name Layout:
Length | Example | Description
--- | --- | ---
3|106|Run Number
6|100001|Image Number

Example:  106100001.jpg

Cropped File Name Layout:
Len Example Desc
3 106 Run Number
6 100001 Image number
1 1 Belt number (This is if there are more than one belt in the image)
2 01 Crop Number (Say 10,11,12,13 when there are 4 pennies in the image) 
4 1025 Coin center in pixels from edge.

Example:  1061000011011025.jpg

It seems like you should code some of this into file folder naming, but you need to mix image files from different runs.

Database for image metadata:
You could have a key and look this up in a database, but this is simpler to start. 

Image Database:
The file system is slow. It would be better to store images in a database. LMDB looks like it’s the way to do, but it has to wait to a Python port.
I have a feeling that people will still want to work with image files though. There could be a converstion script. 

I am guessing that in production a system is not going to use a perfectly cropped undistorted images for training and classification. But they are nice for people to look at and will be able to cross over to systems that have different cameras, lenses, lighting, belt speeds, backgrounds, Etc.