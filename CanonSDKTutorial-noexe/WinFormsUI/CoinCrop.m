%imageName needs to be set when this script is called:
%imageName = 'C:\Temp\TempCoinImages\100559.jpg';

%close all;
%tic
%I did not find calibrating the camera and undistortImage the image was worth it, 
%    at least for the large 18MB images. 
%load 'C:\Users\pkrush\Documents\Computer Vision\coins\CameraParmesEOS3.mat'
%toc; disp('1');

%totalImages = 154;
dirName =  imageName(1:length(imageName)-10);
imageID = str2num(imageName(length(imageName)-9:length(imageName)-4));

%for iter = 1:totalImages
%imageID = 100001 + iter;

fileName = strcat(dirName,num2str(imageID));
cropIter = 1;
BlobIndex = zeros(40,1);
RegionFeatures = RegionFeature.empty(40,0);

if ~ exist(strcat(fileName,'.jpg'), 'file')
    return;
end

%toc; disp('2');
CoinsImage = imread(strcat(fileName,'.jpg'));
%toc; disp('3');
%CoinsImage = undistortImage(CoinsImage, cameraParams);
%toc; disp('4');


row = 1; %Code was taken out that does more then one row.
beltTop = 50;
CoinsImage = CoinsImage(beltTop:beltTop+450,:,:);


%This is deshearing the image from shear/skew caused
%   from it moving on the conveyor
%   and the camera not instantaneous sampling all the pixels at once:
%   The only non 1 or is the "angle".
%   This changes on belt speed changes. 
ShearAngle = [1 0 0; -.02 1 0; 0 0 1];
tform = maketform('affine',ShearAngle);
CoinsImage = imtransform(CoinsImage,tform);


%toc; disp('5');
hsv = rgb2hsv(CoinsImage);

%toc; disp('6');
hue = hsv(:,:,1);
%toc; disp('7');
mask = (hue < .4) | (hue > .65);
%idisp(hsv);
%toc; disp('8');
se = kcircle(6);
%toc; disp('9');
clean = iclose(mask,se);
%toc; disp('10');
clean = iopen(clean,se);

%toc; disp('11');
b = iblobs(clean, 'class',1);
%toc; disp('12');
squareSize = 140;

%figure;
%idisp(clean);
%figure;
%idisp(CoinsImage);

for blobID = 1:size(b,2)
    if (b(blobID).area < 50000 || b(blobID).area > 65000);
        disp(num2str(blobID));
        disp(' has the wrong area to be a penny');
        continue
    end
    if b(blobID).circularity < .8
        disp(num2str(blobID));
        disp(' is not a circle');
        continue
    end
    vmin = int32(b(blobID).vc-squareSize);
    vmax = int32(b(blobID).vc+squareSize);
    umin = int32(b(blobID).uc-squareSize);
    umax = int32(b(blobID).uc+squareSize);
    if vmin <= 0
        disp(num2str(blobID));
        disp(' vmin to low');
        continue
    end
    
    if vmax > size(CoinsImage,1)
        disp(num2str(blobID));
        disp(' vmax to high');
        continue
    end
    
    if umin <= 0
        disp(num2str(blobID));
        disp(' umin to low');
        continue
    end
    if umax > size(CoinsImage,2)
        disp(num2str(blobID));
        disp(' umax to high');
        continue
    end
    
    %pennyMask = clean(b(blobID).vmin:b(blobID).vmax,b(blobID).umin:b(blobID).umax,:);
    %pennyMask = clean(b(blobID).bbox,:);
    %idisp(pennyMask);
    
    
    penny = CoinsImage(vmin:vmax,umin:umax,:);
    
    outputImageSize = 256;
    
    %rotated the image because the pennys look like ovals and this oval needs to be horizontal:
    %There should be real homography calculations here...)
    pennyRotated = imrotate(penny,b(blobID).theta_ * -1);
    
    
    %Squeeze the image so the penny is round again:
    pennyRoundedHeight = size(pennyRotated,1);
    pennyRoundedWidth = int16(pennyRoundedHeight * b(blobID).shape/2) *2 ;
    %At the same time reduce the size of the image over all.
    %reductionAmount = .835 - ((abs(b(blobID).theta_) + .7) /200) ;
    reductionAmount = 1 - ((abs(b(blobID).theta_) + .7) /200) ;
    pennyRoundedHeight = int16(pennyRoundedHeight * reductionAmount);
    pennyRoundedWidth = int16(pennyRoundedWidth * reductionAmount) ;
    pennyRounded = imresize(pennyRotated, [pennyRoundedHeight,pennyRoundedWidth]);
    
    %crop to 256 pixels square:
    leftCrop = int16(pennyRoundedWidth-outputImageSize)/2 -1;
    rightCrop = pennyRoundedWidth - (outputImageSize + leftCrop);
    topCrop = int16(pennyRoundedHeight-outputImageSize)/2;
    bottomCrop = pennyRoundedHeight - (outputImageSize + topCrop);
    pennyCropped = pennyRounded(topCrop+1:pennyRoundedHeight-bottomCrop,leftCrop+1:pennyRoundedWidth-rightCrop,:);
    %imshow(pennyCropped);
    %fig.Name = num2str(b(blobID).theta_);
    %drawnow();
    %pause(.5);
    %
    
    cropID = blobID + 100*row;
    
    %resize from 256x256 to 128x128
    %pennyCropped = impyramid(pennyCropped,'reduce');
    %resize from 128x128 to 64x64
    %pennyCropped = impyramid(pennyCropped,'reduce');
           
    imwrite(pennyCropped,strcat(dirName,num2str(imageID), '_', num2str(cropID) , '_',  num2str(int16(b(blobID).uc) + 10000)   , '.jpg'));
    RegionFeatures(cropIter) = b(blobID);
    BlobIndex(cropIter) = cropID;
    cropIter = cropIter + 1;
    
end
%toc; disp('13');
%save (strcat(dirName, 'mat\',num2str(imageID), 'Blobs.mat'), 'RegionFeatures');
%toc; disp('14');
%save(strcat(dirName, 'mat\',num2str(imageID),'BlobIndex.mat'), 'BlobIndex');
%toc; disp('15');
%end