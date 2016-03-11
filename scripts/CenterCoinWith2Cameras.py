import numpy as np
import cv2
import math

topWidth = int(1920 * .67)
topHeight = int(1080 * .67)
bottomWidth = int(1280 * 1.086)
bottomHeight = int(720 * 1.086)
coinRadius = 355
coinSquareRadius = int(math.sqrt(.5) * coinRadius)
bottomXOffset = -70
bottomYOffset = 46
edgeCropRadius = 14
#topWidth = 548 * 2
#topHeight = 411 * 2
#bottomWidth = 592 * 2
#bottomHeight = 443 * 2
#coinRadius = 203 * 2
#bottomXOffset = -32
#bottomYOffset = 25


topCamera = cv2.VideoCapture(0)
topCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
topCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
bottomCamera = cv2.VideoCapture(1)
bottomCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
bottomCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

drawing=False # true if mouse is pressed
mode=True # if True, draw rectangle. Press 'm' to toggle to curve
top = np.zeros((topWidth,topHeight,1), np.uint8)
bottom = np.zeros((bottomWidth,bottomHeight,1), np.uint8)

# mouse callback function
def interactive_drawing(event,x,y,flags,param):
    global ix,iy,drawing, mode

    if event==cv2.EVENT_MOUSEMOVE:
        topDrawing = top.copy()
        cv2.circle(topDrawing,(x,y), coinRadius,(0,255,0))
        cv2.rectangle(topDrawing,(x+coinSquareRadius-edgeCropRadius,y+coinSquareRadius-edgeCropRadius),(x+coinSquareRadius+edgeCropRadius,y+coinSquareRadius+edgeCropRadius),(0,255,0))
        cv2.imshow('top',topDrawing)
        bottomDrawing = bottom.copy()
        cv2.circle(bottomDrawing,(bottomWidth+bottomXOffset-x,y+bottomYOffset), coinRadius,(0,255,0))
        cv2.imshow('bottom',bottomDrawing)
    return x,y


cv2.namedWindow('top')
cv2.setMouseCallback('top',interactive_drawing)
cv2.namedWindow('bottom')
cv2.setMouseCallback('bottom',interactive_drawing)

while(True):
    ret, frame = topCamera.read()
    top = cv2.resize(frame, (topWidth,topHeight), interpolation = cv2.INTER_AREA)
    #top = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    cv2.imshow('top',top)
    
    ret, frame = bottomCamera.read()
    bottom = cv2.resize(frame, (bottomWidth,bottomHeight), interpolation = cv2.INTER_AREA)
    #bottom = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    cv2.imshow('bottom',bottom)
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

while(True):
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

topCamera.release()
bottomCamera.release()
cv2.destroyAllWindows()
