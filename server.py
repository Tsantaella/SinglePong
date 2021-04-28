import socket
import cv2
import numpy as np

HOST = '127.0.0.1'
PORT = 65430

toni = []
i = 0
while i < 10:
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.bind((HOST, PORT))
        print("binded...")
        s.listen()
        print("listening...")
        conn, addr = s.accept()
        print('Connected by', addr)
        data = conn.recv(786432)
        #while data:
        a = cv2.imdecode(np.frombuffer(data, np.uint8), -1)
        print(len(toni))
        toni.append(a)
        #data = conn.recv(786432)
        #cv2.imshow('nombre', a)
        #cv2.waitKey(0)
        #sendData = "Mersi :)"
        #conn.send(sendData.encode())
        conn.close
        del data
        i = i + 1
    except KeyboardInterrupt:
        exit()

cv2.imshow('nombre', toni[0])
cv2.waitKey(0)
cv2.imshow('nombre', toni[2])
cv2.waitKey(0)
cv2.imshow('nombre', toni[5])
cv2.waitKey(0)
cv2.imshow('nombre', toni[9])
cv2.waitKey(0)