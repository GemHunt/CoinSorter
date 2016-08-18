#HoughCircles works pretty good at first glance...
import cv2
import numpy as np

img = cv2.imread('F:/2Camera/raw/30062.png',0)
img = cv2.medianBlur(img,5)
cimg = cv2.cvtColor(img,cv2.COLOR_GRAY2BGR)

#works for 300 x 169
#circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,100,param1=50,param2=30,minRadius=78,maxRadius=85)
#works for 711 x 400
#circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,100,param1=50,param2=30,minRadius=191,maxRadius=200)

#for num in range(1,1000): 
#works on even:
circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,30,param1=50,param2=30,minRadius=350,maxRadius=357)
#works on odd:
#circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,200,param1=50,param2=30,minRadius=320,maxRadius=330)


circles = np.uint16(np.around(circles))
for i in circles[0,:]:
    # draw the outer circle
    cv2.circle(cimg,(i[0],i[1]),i[2],(0,255,0),1)
    # draw the center of the circle
    cv2.circle(cimg,(i[0],i[1]),2,(0,0,255),1)

print circles
cv2.imshow('detected circles',cimg)
cv2.waitKey(0)
cv2.destroyAllWindows()
