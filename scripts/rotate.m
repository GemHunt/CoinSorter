clear;
close all;
% I want crop out a donut shape out of a 256 x 256 image and unroll it.
% In other words I want a rect image of the lettering around the edge of a coin.
% Create a 360x10 rect from a radius of 94 to 103 pixels.

%working sample:
% years = 1950:10:1990;
% service = 10:10:30;
% wage = [150.697 199.592 187.625
%           179.323 195.072 250.287
%           203.212 179.092 322.767
%           226.505 153.706 426.730
%           249.633 120.281 598.243];
% w = interp2(service,years,wage,[15,13;16,16.6],[1971,1975.3;1973.3,1977]);

%Also works:
%  X = 1:256;
%  Y = 1:256;
% penny = imread('F:\Pennies\Run001\crop\100003_106.jpg');
% penny = double(penny(:,:,1));
% idisp(penny);
% donutMapX = [15,13;16,16.6]
% donutMapY = [171,175.3;173.3,197]
% donut = interp2(X,Y,penny,donutMapX,donutMapY)

dirName = 'F:\Rotated\HeadsWithRotation\';
F = dir(strcat(dirName , '*.jpg'));

for ii = 1:length(F)
    X = 1:256;
    Y = 1:256;
    %imageName = '1000001161030686251';
    
    %penny = imread(strcat('F:\Rotated\HeadsWithRotation\', imageName, '.jpg'));
    penny = imread(strcat(dirName , F(ii).name));
    
    penny = double(penny(:,:,3));
    
    
    donutMapX = double(zeros(360,10));
    donutMapY = double(zeros(360,10));
    
    %donutInsideRadius = 94;
    %donutThickness = 10;
    
    donutInsideRadius = 83;
    donutThickness = 40;
    
    
    startDegree = 180-double( str2num(F(ii).name(17:19)));
    for x = 360:-1:1;
        for r = 1:donutThickness;
            donutMapX(x,r) = cos((x+startDegree)/180*pi-pi)  * (donutInsideRadius + donutThickness - r) + 128;
            donutMapY(x,r) = sin((x+startDegree)/180*pi-pi)  * (donutInsideRadius + donutThickness - r) + 128;
        end
    end
    
    donut = interp2(X,Y,penny,donutMapX,donutMapY);
    %donutLandscape = mat2gray(donut');
    %imwrite(donutLandscape,'F:\rim.png');
    
    for x = 1:40:360;
        donutCrop  = mat2gray(donut(x:x+39,:));
        imwrite(donutCrop',strcat('F:\Rotated\headsRim\',sprintf('%03d', x -1),'\',F(ii).name(1:16),'.png'));
    end
    
    %idisp(donut')
    %figure
    %idisp(penny)
    
end



