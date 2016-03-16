import numpy as np
import cv2
import math
import serial

imageID = 30000
filePath = 'G:/2Camera/'


top = np.zeros((1280,720,3), np.uint8)
bottom = np.zeros((1280,720,3), np.uint8)
cv2.namedWindow('top')
cv2.namedWindow('bottom')

ser = serial.Serial('COM4',250000)
topCamera = cv2.VideoCapture(0)
topCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
topCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
bottomCamera = cv2.VideoCapture(1)
bottomCamera.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
bottomCamera.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

while(True):
    ser.flushInput()
    while True:
        if ser.inWaiting() > 0:
            break

    ret, top = topCamera.read()
    ret, bottom = bottomCamera.read()
    filename = filePath + 'raw/' + str(imageID) + '.png'
    cv2.imwrite(filename,top)
    imageID +=1
    filename = filePath + 'raw/' + str(imageID) + '.png'
    cv2.imwrite(filename,bottom)
    imageID +=1
    cv2.imshow('top',top)
    cv2.imshow('bottom',bottom)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

topCamera.release()
bottomCamera.release()
cv2.destroyAllWindows()
ser.close()
