import numpy as np
import cv2
import math

global mouseX
global mouseY
mouseX = 0
mouseY = 0

topWidth = int(1920 * .67)
topHeight = int(1080 * .67)
coinRadius = 356


#At X = 602:
bottomWidth = 1378 #int(1280 * 1.08)
bottomHeight = 778 # int(720 * 1.08)
bottomXOffset = -70
bottomYOffset = 42


coinSquareRadius = int(math.sqrt(.5) * coinRadius) - 12
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
top = np.zeros((topWidth,topHeight,3), np.uint8)
bottom = np.zeros((bottomWidth,bottomHeight,3), np.uint8)
topSaved = np.zeros((topWidth,topHeight,3), np.uint8)
bottomSaved = np.zeros((bottomWidth,bottomHeight,3), np.uint8)
#edgeCrops = np.zeros((56,56,3), np.uint8)

# mouse callback function
def interactive_drawing(event,x,y,flags,param):
    global mouseX
    global mouseY
    if event==cv2.EVENT_MOUSEMOVE:
        mouseX = x
        mouseY = y

        if x < 400:
            mouseX = 400
        
        if x > 820:
            mouseX = 820

        if y < 356:
            mouseY = 356

        if y > 367:
            mouseY = 367
       

    if event==cv2.EVENT_LBUTTONDOWN:
        print x,y
    return x,y

cv2.namedWindow('top')
cv2.setMouseCallback('top',interactive_drawing)
cv2.namedWindow('bottom')
cv2.namedWindow('edgeCrops')

cameraOn = 1

while(True):
    if cameraOn:
        ret, frame = topCamera.read()
        top = cv2.resize(frame, (topWidth,topHeight), interpolation = cv2.INTER_AREA)
        #topgrey = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        ret, bottom = bottomCamera.read()
        #bottomgrey = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    else:
        top = topSaved.copy()
        bottom = bottomSaved.copy()


    OffsetCenterX = 600
    #Offset the bottom camera X because the cameras lenes are not the same focal length:
    offsetBottomWidth = bottomWidth + int(abs(OffsetCenterX-mouseX)*0.05)
    #Offset the Bottom camera X because the cameras lenes are not the same focal length:
    offsetX = mouseX  - int(abs(400-mouseX)*0.03)
    #Offset the Bottom camera Y because the cameras are not on the same angle(as they are twisted to each other):
    offsetY = mouseY + int((OffsetCenterX-mouseX)*0.02)
    

    bottom = cv2.resize(bottom, (offsetBottomWidth,bottomHeight), interpolation = cv2.INTER_AREA)


    if cameraOn:
        vectorColor = (0,0,255)
    else:
        vectorColor = (0,255,0)
                        
    cv2.circle(top,(mouseX,mouseY), coinRadius,vectorColor)

    x1 = mouseX+coinSquareRadius-edgeCropRadius
    y1 = mouseY-coinSquareRadius-edgeCropRadius
    x2 = mouseX+coinSquareRadius+edgeCropRadius
    y2 = mouseY-coinSquareRadius+edgeCropRadius
    edgeCrop1 = top[y1:y2,x1:x2] 
    cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX-coinSquareRadius-edgeCropRadius
    y1 = mouseY-coinSquareRadius-edgeCropRadius
    x2 = mouseX-coinSquareRadius+edgeCropRadius
    y2 = mouseY-coinSquareRadius+edgeCropRadius
    edgeCrop2 = top[y1:y2,x1:x2] 
    cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX-coinSquareRadius-edgeCropRadius
    y1 = mouseY+coinSquareRadius-edgeCropRadius
    x2 = mouseX-coinSquareRadius+edgeCropRadius
    y2 = mouseY+coinSquareRadius+edgeCropRadius
    edgeCrop3 = top[y1:y2,x1:x2] 
    cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX+coinSquareRadius-edgeCropRadius
    y1 = mouseY+coinSquareRadius-edgeCropRadius
    x2 = mouseX+coinSquareRadius+edgeCropRadius
    y2 = mouseY+coinSquareRadius+edgeCropRadius
    edgeCrop4 = top[y1:y2,x1:x2] 
    cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))


    #cv2.rectangle(top,(mouseX-coinSquareRadius-edgeCropRadius,mouseY+coinSquareRadius-edgeCropRadius),(mouseX-coinSquareRadius+edgeCropRadius,mouseY+coinSquareRadius+edgeCropRadius),(0,255,0))
    #cv2.rectangle(top,(mouseX+coinSquareRadius-edgeCropRadius,mouseY+coinSquareRadius-edgeCropRadius),(mouseX+coinSquareRadius+edgeCropRadius,mouseY+coinSquareRadius+edgeCropRadius),(0,255,0))


    #cv2.rectangle(top,(mouseX+coinRadius-28 ,mouseY - edgeCropRadius),(mouseX+coinRadius,mouseY + edgeCropRadius),(0,255,0))
    cv2.imshow('top',top)
    cv2.circle(bottom,(bottomWidth+bottomXOffset-offsetX,offsetY+bottomYOffset), coinRadius,vectorColor)
    cv2.imshow('bottom',bottom)

    edgeCropT = np.concatenate((edgeCrop2, edgeCrop1), axis=1)
    edgeCropB = np.concatenate((edgeCrop3, edgeCrop4), axis=1)
    edgeCrops = np.concatenate((edgeCropT, edgeCropB), axis=0)
    cv2.imshow('edgeCrops',edgeCrops)


    

    if cv2.waitKey(1) & 0xFF == ord('w'):
        cameraOn =  not cameraOn
        ret, frame = topCamera.read()
        topSaved = cv2.resize(frame, (topWidth,topHeight), interpolation = cv2.INTER_AREA)
        ret, frame = bottomCamera.read()
        bottomSaved = cv2.resize(frame, (bottomWidth,bottomHeight), interpolation = cv2.INTER_AREA)
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


"""
    if cv2.waitKey(1) & 0xFF == ord('y'):
        bottomWidth -= 1
        print 'bottomWidth ' + str(bottomWidth)
    if cv2.waitKey(1) & 0xFF == ord('h'):
        bottomWidth += 1
        print 'bottomWidth ' + str(bottomWidth)
    
    if cv2.waitKey(1) & 0xFF == ord('u'):
        bottomHeight -= 1
        print 'bottomHeight ' + str(bottomHeight)
    if cv2.waitKey(1) & 0xFF == ord('j'):
        bottomHeight += 1
        print 'bottomHeight ' + str(bottomHeight)

    if cv2.waitKey(1) & 0xFF == ord('i'):
        bottomXOffset -= 1
        print 'bottomXOffset ' + str(bottomXOffset)
    if cv2.waitKey(1) & 0xFF == ord('k'):
        bottomXOffset += 1
        print 'bottomXOffset ' + str(bottomXOffset)

    if cv2.waitKey(1) & 0xFF == ord('o'):
        bottomYOffset -= 1
        print 'bottomYOffset ' + str(bottomYOffset)
    if cv2.waitKey(1) & 0xFF == ord('l'):
        bottomYOffset += 1
        print 'bottomYOffset ' + str(bottomYOffset)
    if cv2.waitKey(1) & 0xFF == ord('x'):
        print 'mouseX ' + str(mouseX)
"""


topCamera.release()
bottomCamera.release()
cv2.destroyAllWindows()
