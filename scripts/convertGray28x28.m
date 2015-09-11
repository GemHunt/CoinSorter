clear;
close all;

dirName = 'F:\LiveView Pennies\1982\1982heads\1982D\';
F = dir(strcat(dirName , '*.jpg'));
for ii = 1:length(F)
     penny = imread(strcat(dirName , F(ii).name));
     penny40 =  imresize(penny, [40,40]);
     penny40 = rgb2gray(penny40);
    imwrite(penny40,strcat('F:\LiveView Pennies\1982\1982heads-40\1982D\',F(ii).name(1:16),'.png'));
end





