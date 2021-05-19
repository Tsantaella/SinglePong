# -*- coding: utf-8 -*-
"""
RLP
@author: Grup03
"""
import cv2
import numpy as np

def detectBall(imagen):
    hsv = cv2.cvtColor(imagen, cv2.COLOR_BGR2HSV)
    h,w,m=imagen.shape
    
    #En este caso un balon rojo
    bajos = np.array([0,100,100], dtype=np.uint8)
    altos = np.array([20,255,255], dtype=np.uint8)
    

    #Crear una mascara con solo los pixeles dentro del rango 
    mask = cv2.inRange(hsv,bajos,altos)
    #Con la funcion moments buscamos el centroide
    moments = cv2.moments(mask)
    
    area =int(moments['m00'])
    #Iniciamos valores de coordenadas a '0'
    x=0
    y=0
    dist=0
    # Se ha de ajustar el area en funcion de la distancia de la pelota. 
    # Tenemos que comprobar como de grande detecta la pelota a distintas distancias
    if(area > 10):
        #Con el centroide, calculamos con la formula el centro
        x = float(moments['m10']/moments['m00'])
        y = float(moments['m01']/moments['m00'])

        #Mostramos sus coordenadas respecto al eje           
        x = str("%.2f" %(x-w/2))
        y = str("%.2f" %(-(y-h/2)))
        #print(area)
        return (x,y,area)
    else:
        return str(-1)
