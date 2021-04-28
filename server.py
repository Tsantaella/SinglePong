import socket
import cv2
import numpy as np

HOST = '127.0.0.1'
PORT = 65430

#i = 0
while True:
    try:
        #creamos el socket
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        #le hacemos bind al puerto y host
        s.bind((HOST, PORT))
        print("binded...")
        #esperamos a escuchar una conexión
        s.listen()
        print("listening...")
        #aceptamos cuando tengamos una conexión
        conn, addr = s.accept()
        print('Connected by', addr)
        #recibimos la imagen
        data = conn.recv(786432)
        #la pasamos de bytes a numeros
        a = cv2.imdecode(np.frombuffer(data, np.uint8), -1)
        #cv2.imshow('nombre', a)
        #cv2.waitKey(0)

        #sendData = "Mersi :)"
        #conn.send(sendData.encode())

        #cerramos conexión del socket
        conn.close
        #aliberamos memoria
        del data

    except KeyboardInterrupt:
        exit()
