clear;
close all;

imageSize = 406;
imageSizeX = imageSize;
imageSizeY = imageSize;
[columnsInImage, rowsInImage] = meshgrid(1:imageSizeX, 1:imageSizeY);
centerX = imageSize/2 + .5;
centerY = imageSize/2 + .5;
radius = imageSize/2;
CircleMask(:,:,1) = (rowsInImage - centerY).^2 + (columnsInImage - centerX).^2 <= radius.^2;
CircleMask(:,:,2) = CircleMask(:,:,1);
CircleMask(:,:,3) = CircleMask(:,:,1);
CircleMask = uint8(CircleMask);
%To save CircleMask:
%CircleMask = rgb2gray(uint8(CircleMask * 255));
%imwrite(CircleMask,'F:\CircleMask406.png');
dirName = 'F:\new4\maple\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
    penny = imread(strcat(dirName , F(ii).name));
    
    %No Rotation:
    %pennyMasked = penny .* CircleMask;
    %penny40 =  imresize(pennyMasked, [40,40]);
    %penny40 = rgb2gray(penny40);
    %imwrite(penny40,strcat('F:\new4NotRot\maple\',F(ii).name));
    
    for angle = 5:26:359;
        pennyRotated = imrotate(penny,angle,'bilinear');
        rotatedSize = size(pennyRotated);
        rotatedSize = rotatedSize(1);
        remainder = mod( rotatedSize-imageSize,2);
        cropAmount = int16((rotatedSize - imageSize )/2);
        cropStart = cropAmount + remainder +1;
        cropEnd = cropStart + imageSize -1;
        pennyCropped = pennyRotated(cropStart:cropEnd,cropStart:cropEnd,:);
        pennyMasked = pennyCropped .* centerMask;
        %penny9th =  pennyMasked(164:243,321:400,:);
        penny40 =  imresize(pennyMasked, [40,40]);
        penny40 = rgb2gray(penny40);
        imwrite(penny40,strcat('F:\new4Rot\canada_other\R',sprintf('%03d', angle),F(ii).name));
    end
end





