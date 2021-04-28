from collections import deque
from imutils.video import VideoStream
import numpy as np
import argparse
import cv2
import imutils
import time

ap = argparse.ArgumentParser()
ap.add_argument("-im", "--image",
	help="path to the (optional) video file")
args = vars(ap.parse_args())

if not args.get("image", False):
	im = cv2.imread('example.jpg')
else:
    im = cv2.imread(args["image"])

im = cv2.medianBlur(im,19)
greenLower = (36, 25, 25)
greenUpper = (86, 255, 255)
#pts = deque(maxlen=args["buffer"])

frame = imutils.resize(im, width=600)
blurred = cv2.GaussianBlur(frame, (11, 11), 0)
hsv = cv2.cvtColor(blurred, cv2.COLOR_BGR2HSV)

mask = cv2.inRange(hsv, greenLower, greenUpper)
mask = cv2.erode(mask, None, iterations=2)
mask = cv2.dilate(mask, None, iterations=2)
cv2.imshow("Frame", mask)
cv2.waitKey(0)
cnts = cv2.findContours(mask.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
cnts = imutils.grab_contours(cnts)
center = None

if len(cnts) > 0:
    c = max(cnts, key=cv2.contourArea)
    ( (x,y), radius) = cv2.minEnclosingCircle(c)
    M = cv2.moments(c)
    center = (int(M["m10"] / M["m00"]), int(M["m01"] / M["m00"]))

    if radius > 10:
        cv2.circle(frame, (int(x), int(y)), int(radius), 
        (0,255,255), 2)
        cv2.circle(frame, center, 5, (0,0,255), -1)

cv2.imshow("Frame", frame)
cv2.waitKey(0)