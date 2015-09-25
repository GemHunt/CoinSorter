clear;
close all;
%Make the directories to start with:
%for angle = 0:1:359;
 %   mkdir(strcat('F:\Rotated\HeadsWithRotation3600\',sprintf('%03d', angle),'\'));
%end

imageSizeX = 227;
imageSizeY = 227;
[columnsInImage, rowsInImage] = meshgrid(1:imageSizeX, 1:imageSizeY);
centerX = 114;
centerY = 114;
radius = 113.5;
centerMask(:,:,1) = (rowsInImage - centerY).^2 + (columnsInImage - centerX).^2 <= radius.^2;
centerMask(:,:,2) = centerMask(:,:,1);
centerMask(:,:,3) = centerMask(:,:,1);
centerMask = uint8(centerMask);

%dirName = 'D:\CoinImages\Rotated\HeadsWithRotation\';
dirName = 'D:\CoinImages\Rotated\heads\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
    startDegree = 180-(double( str2num(F(ii).name(17:20)))/10);
    penny = imread(strcat(dirName , F(ii).name));
    for angle = 0:1:359;
        pennyRotated = imrotate(penny,angle + startDegree,'bilinear');
        rotatedSize = size(pennyRotated);
        rotatedSize = rotatedSize(1);
        remainder = mod( rotatedSize-226,2);
        cropAmount = (rotatedSize - 226 - remainder)/2;
        cropStart = cropAmount + remainder;
        cropEnd = rotatedSize -cropAmount;
        pennyRotated = pennyRotated(cropStart:cropEnd,cropStart:cropEnd,:);
        pennyMasked = pennyRotated .* centerMask;
        penny40 =  imresize(pennyMasked, [40,40]);
        penny40 = rgb2gray(penny40);
        imwrite(penny40,strcat('F:\Rotated\HeadsWithRotation3600\',sprintf('%03d', angle),'\',F(ii).name(1:16),'.png'));
    end
end





