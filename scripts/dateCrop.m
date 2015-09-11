clear;
close all;

dirName = 'F:\LiveView Pennies\1982\1982HeadsAngle\not1982\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
     penny = imread(strcat(dirName , F(ii).name));
    startDegree = 180-double( str2num(F(ii).name(17:19)));
         pennyRotated = imrotate(penny, startDegree,'bilinear');
        rotatedSize = size(pennyRotated);
        rotatedSize = rotatedSize(1);
        remainder = mod( rotatedSize-255,2);
        cropAmount = (rotatedSize - 255 - remainder)/2;
        cropStart = cropAmount + remainder;
        cropEnd = rotatedSize -cropAmount;
        pennyRotated = pennyRotated(cropStart:cropEnd,cropStart:cropEnd,:);
        dateCropped = pennyRotated(160:210,190:240,:);

    imwrite(dateCropped,strcat('F:\LiveView Pennies\1982\1982Dates\not1982\',F(ii).name(1:16),'.png'));
end





