clear;
close all;

imageSize = 406;
imageSizeX = imageSize;
imageSizeY = imageSize;
[columnsInImage, rowsInImage] = meshgrid(1:imageSizeX, 1:imageSizeY);
centerX = imageSize/2 - .5;
centerY = imageSize/2 - .5;
radius = imageSize/2;
centerMask(:,:,1) = (rowsInImage - centerY).^2 + (columnsInImage - centerX).^2 <= radius.^2;
centerMask(:,:,2) = centerMask(:,:,1);
centerMask(:,:,3) = centerMask(:,:,1);
centerMask = uint8(centerMask);

dirName = 'F:\new2\tails\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
    penny = imread(strcat(dirName , F(ii).name));
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
        imwrite(pennyMasked,strcat('F:\new3\tails\R',sprintf('%03d', angle),F(ii).name));
    end
end





