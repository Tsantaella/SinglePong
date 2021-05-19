# -*- coding: utf-8 -*-
"""
RLP
@author: Grup03
"""
import cv2
import numpy as np
 
#Iniciamos la camara
# camara = cv2.VideoCapture(0)
# activada = True
# while(activada):

#     #Capturamos una imagen y la convertimos de RGB -> HSV
#     ret, imagen = camara.read()

#     if (ret):

def detectBall(imagen):
    hsv = cv2.cvtColor(imagen, cv2.COLOR_BGR2HSV)
    h,w,m=imagen.shape
    
    #En este caso un tapon rojo
    bajos = np.array([0,100,100], dtype=np.uint8)
    altos = np.array([20,255,255], dtype=np.uint8)
    

    #Crear una mascara con solo los pixeles dentro del rango 
    mask = cv2.inRange(hsv,bajos,altos)
    #Con la funcion moments buscamos el centroide
    moments = cv2.moments(mask)
    
    area =int(moments['m00'])
    #Descomentar para ver el area por pantalla, iniciamos valores de coordenadas a '0'
    #print(area)
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
        #print ("Coordenadas, Area: ", (x-w/2),(-(y-h/2)),float(area))
        x = str("%.2f" %(x-w/2))
        y = str("%.2f" %(-(y-h/2)))
        #print(area)
        return (x,y,area)
        #Rectangulo blanco en medio del objeto
        #cv2.rectangle(imagen, (x-5, y-5), (x+5, y+5),(255,255,255), 18)
        
        #Ejes vertical y perpendicular al objeto
    else:
        return str(-1)
    # cv2.line(imagen,(0,y),(w,y),(0,0,0),2)        
    # cv2.line(imagen,(x,0),(x,h),(0,0,0),2) 

    # #Divide la imagen en cuatro cuadrantes
    # cv2.line(imagen,(0,int(h/2)),(w,int(h/2)),(255,255,255),2)        
    # cv2.line(imagen,(int(w/2),0),(int(w/2),h),(255,255,255),2)                   

    #Mostramos la imagen original con la marca del centro y la mascara
    # cv2.imshow('mask', mask)
    # cv2.imshow('Camara', imagen)
    # tecla = cv2.waitKey(5) & 0xFF
    # if tecla == ord('q'):
        
    #     break 
# cv2.destroyAllWindows()
