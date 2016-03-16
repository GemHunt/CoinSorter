import numpy as np
import cv2
import math
import serial

global mouseX, mouseY,saveImages
mouseX = 0
mouseY = 0
imageID = 30000
filePath = 'G:/2Camera/'

topWidth = int(1920 * .67)
topHeight = int(1080 * .67)
coinRadius = 356


#At X = 602: this will get reset anyway...
bottomWidth = 1378 #int(1280 * 1.08)
bottomHeight = 778 # int(720 * 1.08)
bottomXOffset = -70
bottomYOffset = 42

#At 640 x480 for both cameras:
#topWidth = 548 * 2
#topHeight = 411 * 2
#bottomWidth = 592 * 2
#bottomHeight = 443 * 2
#coinRadius = 203 * 2
#bottomXOffset = -32
#bottomYOffset = 25

coinSquareRadius = int(math.sqrt(.5) * coinRadius) 
edgeCropRadius = 28

topCamera = cv2.VideoCapture(0)
topCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
topCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
bottomCamera = cv2.VideoCapture(1)
bottomCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
bottomCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

top = np.zeros((topWidth,topHeight,3), np.uint8)
bottom = np.zeros((bottomWidth,bottomHeight,3), np.uint8)
topSaved = np.zeros((topWidth,topHeight,3), np.uint8)
bottomSaved = np.zeros((bottomWidth,bottomHeight,3), np.uint8)

cv2.namedWindow('top')
cv2.namedWindow('bottom')
cv2.namedWindow('edgeCrops')

cameraOn = 1
saveImages = 1

# mouse callback function
def interactive_drawing(event,x,y,flags,param):
    global mouseX, mouseY, saveImages
    if event==cv2.EVENT_MOUSEMOVE:
        mouseX = x
        mouseY = y

        if x < 400:
            mouseX = 400
        
        if x > 820:
            mouseX = 820

        if y < 200:
            mouseY = 200

        if y > 500:
            mouseY = 500

    if event==cv2.EVENT_LBUTTONDOWN:
        saveImages = 1
    return x,y

cv2.setMouseCallback('top',interactive_drawing)
ser = serial.Serial('COM4',250000)
while(True):
    ser.flushInput()
    while True:
        if ser.inWaiting() > 0:
            break

    if cameraOn:
        ret, frame = topCamera.read()
        top = cv2.resize(frame, (topWidth,topHeight), interpolation = cv2.INTER_AREA)
        ret, bottom = bottomCamera.read()
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
    bottomX = bottomWidth+bottomXOffset-offsetX
    bottomY = offsetY+bottomYOffset
       
    bottom = cv2.resize(bottom, (offsetBottomWidth,bottomHeight), interpolation = cv2.INTER_AREA)

    x1 = mouseX+coinSquareRadius-edgeCropRadius
    y1 = mouseY-coinSquareRadius-edgeCropRadius
    x2 = mouseX+coinSquareRadius+edgeCropRadius
    y2 = mouseY-coinSquareRadius+edgeCropRadius
    edgeCrop1 = top[y1:y2,x1:x2] 
    #cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX-coinSquareRadius-edgeCropRadius
    y1 = mouseY-coinSquareRadius-edgeCropRadius
    x2 = mouseX-coinSquareRadius+edgeCropRadius
    y2 = mouseY-coinSquareRadius+edgeCropRadius
    edgeCrop2 = top[y1:y2,x1:x2] 
    #cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX-coinSquareRadius-edgeCropRadius
    y1 = mouseY+coinSquareRadius-edgeCropRadius
    x2 = mouseX-coinSquareRadius+edgeCropRadius
    y2 = mouseY+coinSquareRadius+edgeCropRadius
    edgeCrop3 = top[y1:y2,x1:x2] 
    #cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    x1 = mouseX+coinSquareRadius-edgeCropRadius
    y1 = mouseY+coinSquareRadius-edgeCropRadius
    x2 = mouseX+coinSquareRadius+edgeCropRadius
    y2 = mouseY+coinSquareRadius+edgeCropRadius
    edgeCrop4 = top[y1:y2,x1:x2] 
    #cv2.rectangle(top,(x1,y1),(x2,y2),(0,255,0))

    edgeCropT = np.concatenate((edgeCrop2, edgeCrop1), axis=1)
    edgeCropB = np.concatenate((edgeCrop3, edgeCrop4), axis=1)
    edgeCrops = np.concatenate((edgeCropT, edgeCropB), axis=0)



    if saveImages == 1:
        saveImages = 1
        filename = filePath + 'crops/' + str(imageID) + '.png'
        #cv2.imwrite(filename,top[mouseY -coinRadius:mouseY + coinRadius,mouseX -coinRadius :mouseX +coinRadius] )
        filename = filePath + 'raw/' + str(imageID) + '.png'
        cv2.imwrite(filename,top)
        imageID +=1
        filename = filePath + 'crops/' + str(imageID) + '.png'
        #cv2.imwrite(filename,bottom[bottomY -coinRadius:bottomY + coinRadius,bottomX -coinRadius :bottomX +coinRadius] )
        filename = filePath + 'raw/' + str(imageID) + '.png'
        cv2.imwrite(filename,bottom)
        imageID +=1


    if cameraOn:
        vectorColor = (0,0,255)
    else:
        vectorColor = (0,255,0)

    cv2.circle(top,(mouseX,mouseY), coinRadius,vectorColor)
    cv2.imshow('top',top)

    cv2.circle(bottom,(bottomX,bottomY), coinRadius,vectorColor)
    cv2.imshow('bottom',bottom)

    cv2.imshow('edgeCrops',edgeCrops)
    cv2.moveWindow('edgeCrops',600,750)

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
ser.close()
