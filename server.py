import socket
import cv2
import numpy as np
from ballDetection import detectBall

HOST = '127.0.0.1'
PORT = 65430

print("SERVIDOR INICIADO EN LOCALHOST:65430")
while True:
    try:
        #creamos el socket
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        #le hacemos bind al puerto y host
        s.bind((HOST, PORT))
        #esperamos a escuchar una conexión
        s.listen()
        #aceptamos cuando tengamos una conexión
        conn, addr = s.accept()

        #recibimos la imagen
        data = conn.recv(786432)
        #la pasamos de bytes a numeros
        img = cv2.imdecode(np.frombuffer(data, np.uint8), -1)

        #creamos datos a enviar
        sendData = str(detectBall(img))
        #los enviamos al socket
        conn.send(sendData.encode())

        #cerramos conexión del socket
        s.close
        #aliberamos memoria
        del data

    except KeyboardInterrupt:
        exit()
