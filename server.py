import socket
import cv2
import numpy as np

HOST = '127.0.0.1'
PORT = 65430
a = False

while True:
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.bind((HOST, PORT))
        print("binded...")
        s.listen()
        print("listening...")
        conn, addr = s.accept()
        print('Connected by', addr)
        data = conn.recv(65507)
        while data:
            print(cv2.imdecode(np.frombuffer(data, np.uint8), -1))
            data = conn.recv(65507)

        sendData = "Mersi :)"
        conn.send(sendData.encode())
        conn.close
    except KeyboardInterrupt:
        exit()

