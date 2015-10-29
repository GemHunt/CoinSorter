clear;
close all;

dirName = 'F:\new4\tails\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
    lastCapture = imread(strcat(dirName,F(ii).name));
    lastCapture = double(lastCapture);

    meanOfTheRows = mean(lastCapture,2);
    meanOfTheRow = mean(meanOfTheRows);
    correction = bsxfun(@minus,meanOfTheRows , meanOfTheRow);
    deFlickeredLastCapture = uint8(bsxfun(@minus,lastCapture,correction));

    imwrite(deFlickeredLastCapture,strcat(dirName,F(ii).name));
end 

