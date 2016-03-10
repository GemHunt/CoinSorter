import numpy as np
import cv2

topWidth = 548
topHeight = 411
bottomWidth = 592
bottomHeight = 443
coinRadius = 203
bottomXOffset = -32
bottomYOffset = 25

topCamera = cv2.VideoCapture(0)
#topCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
#topCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
bottomCamera = cv2.VideoCapture(1)
#bottomCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
#bottomCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

drawing=False # true if mouse is pressed
mode=True # if True, draw rectangle. Press 'm' to toggle to curve
grayTop = np.zeros((topWidth,topHeight,1), np.uint8)
grayBottom = np.zeros((bottomWidth,bottomHeight,1), np.uint8)

# mouse callback function
def interactive_drawing(event,x,y,flags,param):
    global ix,iy,drawing, mode

    if event==cv2.EVENT_MOUSEMOVE:
        grayTopDrawing = grayTop.copy()
        cv2.circle(grayTopDrawing,(x,y), coinRadius,128)
        #cv2.rectangle(grayTopDrawing,(x,y),(x+10,y+10),0)
        cv2.imshow('top',grayTopDrawing)

        grayBottomDrawing = grayBottom.copy()
        cv2.circle(grayBottomDrawing,(bottomWidth+bottomXOffset-x,y+bottomYOffset), coinRadius,128)
        cv2.imshow('bottom',grayBottomDrawing)
    return x,y


cv2.namedWindow('top')
cv2.setMouseCallback('top',interactive_drawing)
cv2.namedWindow('bottom')
cv2.setMouseCallback('bottom',interactive_drawing)

while(True):
    ret, frame = topCamera.read()
    frame = cv2.resize(frame, (topWidth,topHeight), interpolation = cv2.INTER_AREA)
    grayTop = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    cv2.imshow('top',grayTop)
    
    ret, frame = bottomCamera.read()
    frame = cv2.resize(frame, (bottomWidth,bottomHeight), interpolation = cv2.INTER_AREA)
    grayBottom = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    cv2.imshow('bottom',grayBottom)
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

while(True):
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

topCamera.release()
bottomCamera.release()
cv2.destroyAllWindows()
