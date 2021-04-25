import cv2
import numpy as np

v = cv2.VideoCapture('pong.mp4')
output = []
while(v.isOpened()):
    ret,frame = v.read()
    if ret == False:
        break
    
    img = cv2.medianBlur(frame,5)

    cimg = cv2.cvtColor(img,cv2.COLOR_BGR2GRAY)

    # cv2.imshow('Original image',img)
    # cv2.imshow('Gray image', cimg)
    # cv2.waitKey()
    
    circles = cv2.HoughCircles(cimg,cv2.HOUGH_GRADIENT, 1, 20, param1=50,param2=30,minRadius=0,maxRadius=0)
    if(circles is not None):
        circles = np.uint16(np.around(circles))
        for i in circles[0,:]:
        # draw the outer circle
            cv2.circle(img,(i[0],i[1]),i[2],(0,255,0),2)
            # draw the center of the circle
            cv2.circle(img,(i[0],i[1]),2,(0,0,255),3)
            # cv2.imshow('detected circles',cimg)
            # cv2.waitKey(0)
            # cv2.destroyAllWindows()    
    output.append(img)


out = cv2.VideoWriter('output_video.avi', cv2.VideoWriter_fourcc(*'mp4v'), 10, frameSize=(1280, 720), isColor=True)

for i in range(len(output)):
    out.write(output[i])
out.release()