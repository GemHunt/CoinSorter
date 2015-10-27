clear;
close all;
meanEmptyCapture = zeros(480,640,3);
%Get a mean of 156 empty belt images:
for i = 2:157
 emptyCapture = imread(strcat('F:\OpenCV\empty\',sprintf('%d', i + 10000),'raw.jpg'));
 %emptyCapture = rgb2ycbcr(emptyCaptureRGB);
 %With a blue background the cr channel stands out:
 %emptyCapture = emptyCapture(:,:,3);
 emptyCapture = double(emptyCapture);
 meanEmptyCapture = meanEmptyCapture + emptyCapture;
end
%lastCapture = emptyCapture;
lastCapture = imread(strcat('F:\OpenCV\10406raw.jpg'));
idisp(uint8(lastCapture));
lastCapture = double(lastCapture);


meanEmptyCapture = meanEmptyCapture / 156;
meanCorrectedLastCapture = lastCapture - meanEmptyCapture;
meanMeanMean = mean(mean(meanEmptyCapture));
meanCorrectedLastCapture = bsxfun(@plus,meanCorrectedLastCapture,meanMeanMean);
figure;
idisp(uint8(meanCorrectedLastCapture));