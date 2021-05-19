import socket
import cv2
import numpy as np
from ballDetection import *

HOST = '127.0.0.1'
PORT = 65430

print("SERVIDOR INICIADO EN LOCALHOST:65430")
while True:
    try:
        #creamos el socket
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        #le hacemos bind al puerto y host
        s.bind((HOST, PORT))
        #print("binded...")
        #esperamos a escuchar una conexión
        s.listen()
        #print("listening...")
        #aceptamos cuando tengamos una conexión
        conn, addr = s.accept()
        #print('Connected by', addr)
        #recibimos la imagen
        data = conn.recv(786432)
        #conn.close

        #la pasamos de bytes a numeros
        img = cv2.imdecode(np.frombuffer(data, np.uint8), -1)
        #cv2.imshow('nombre', a)
        #cv2.waitKey(0)

        #creamos el socket
        #s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        #le hacemos bind al puerto y host
        #s.connect((HOST, PORT))

        sendData = str(detectBall(img))
        #sendData = "Mersi :)"
        conn.send(sendData.encode())

        #cerramos conexión del socket
        s.close
        #aliberamos memoria
        del data

    except KeyboardInterrupt:
        exit()
