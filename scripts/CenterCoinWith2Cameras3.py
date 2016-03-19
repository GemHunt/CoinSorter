#This is quick & dirty script to label the center of the pennies. 
import numpy as np
import cv2
import math
import serial
import sqlite3



global mouseX, mouseY, imageID, conn
conn = sqlite3.connect('G:/2Camera/coins.db')
mouseX = 0
mouseY = 0
imageID = 30004
filePath = 'G:/2Camera/'

#topWidth = int(1920 * .67)
#topHeight = int(1080 * .67)
topWidth = 1280
topHeight = 720

coinRadius = 356

#At X = 602: this will get reset anyway...
bottomWidth = 1384 #int(1280 * 1.08)
bottomHeight = 782 # int(720 * 1.08)
bottomXOffset = -78
bottomYOffset = 45

coinSquareRadius = int(math.sqrt(.5) * coinRadius) 
edgeCropRadius = 28

top = np.zeros((topWidth,topHeight,3), np.uint8)
bottom = np.zeros((bottomWidth,bottomHeight,3), np.uint8)

cv2.namedWindow('top')
cv2.namedWindow('bottom')
cv2.namedWindow('edgeCrops')

# mouse callback function
def interactive_drawing(event,x,y,flags,param):
    global mouseX, mouseY, imageID, conn
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
        c = conn.cursor()
        c.execute('INSERT INTO coins VALUES (' + str(imageID) + ',' +  str(mouseX) + ',' + str(mouseY) + ')')
        conn.commit()
        imageID += 2
    if event==cv2.EVENT_RBUTTONDOWN:
        imageID += 2
    if event==cv2.EVENT_MBUTTONDOWN:
        imageID -= 2
      
        
    return x,y

cv2.setMouseCallback('top',interactive_drawing)
while(True):
    
    top = cv2.imread(filePath + 'raw/' + str(imageID) + '.png')
    bottom = cv2.imread(filePath + 'raw/' + str(imageID+1) + '.png')
    #top = cv2.resize(top, (topWidth,topHeight), interpolation = cv2.INTER_AREA)

    
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
    vectorColor = (0,0,255)
    
    cv2.circle(top,(mouseX,mouseY), coinRadius,vectorColor)
    cv2.putText(top, str(imageID), (10,10),cv2.FONT_HERSHEY_PLAIN, 1,vectorColor) 
    cv2.imshow('top',top)

    cv2.circle(bottom,(bottomX,bottomY), coinRadius,vectorColor)
    cv2.imshow('bottom',bottom)

    #cv2.imshow('edgeCrops',edgeCrops)
    #cv2.moveWindow('edgeCrops',600,750)
     
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


conn.close()
cv2.destroyAllWindows()



    
"""
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

    filename = filePath + 'crops/' + str(imageID) + '.png'
    cv2.imwrite(filename,top[mouseY -coinRadius:mouseY + coinRadius,mouseX -coinRadius :mouseX +coinRadius] )
    filename = filePath + 'crops/' + str(imageID) + '.png'
    cv2.imwrite(filename,bottom[bottomY -coinRadius:bottomY + coinRadius,bottomX -coinRadius :bottomX +coinRadius] )
    cv2.imwrite(filename,bottom)
"""

   

