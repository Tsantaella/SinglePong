![SinglePongImage](https://github.com/Tsantaella/SinglePong/blob/main/img/header_img.png)
<div style=text-align:center>


<h3>SINGLE PONG</h3>



Arnau Revelles Segalés - 1527873

Gerard Martínez Espelleta - 1531236

Toni Santaella Trujillo - 1526913

Kilian Niubó Vinuesa - 1497932

<hr>
<h2>Table of Contents</h2>



* [What's this?](#what's-this?)                                                                                                                       

* [Requirements](#requirements)                                                                                                              

* [Hardware scheme](#hardware-scheme)                                                                                                                        

* [Software Arquitecture](#software-arquitecture)                                                                                                                 

* [Ball Detection](#ball-detection)                                                                                                                                                                                                                                             
* [Structural Components](#structural-components)                                                                                             

* [Simulation](#simulation)                                                                                                                     

* [Resultados](#resultados)
  
* [Future Work](#future_work) 
  
  
* [References](#references)
  

</div>

# What's this?
SinglePong is a robot that allows you to play ping pong without a partner.


It's based on a solid square of the size of a normal ping pong raket, which is able to detect a ball in movement and intercept it to throw it back. In order to know where to move the robot, we'll use a camera and an AI algorithm able to detect the ball.

The frame is subjected to the pìng pong table, offering a light and easy to  assemble structure.

Features:
1. Fun robot to practice ping pong for novices.
2. Cheap and easy to assemble.
3. Simple yet effective.



# Requirements
For assembling the Robot:

●    ***Raspberry Pi 4 2GB RAM***

●    ***4 Nema Motor ***

●    ***Raspberry pi camera V2 8MPX***

●    ***Controller I2c***

●    ***Raspberry Pi Power Supply***

●    ***Stepper Controller Pack 10u***

# Hardware scheme

![ArduinoModel](https://github.com/Tsantaella/SinglePong/blob/main/img/image003.png)

El esquema básico con el que manejaremos los motores será el de la figura de arriba.
![RaspberryModel](https://github.com/Tsantaella/SinglePong/blob/main/img/image005.png)
Dispondremos de una Raspberry a la que irá conectada la cámara. Con los datos recogidos por esta se realizarán los cálculos necesarios y se mandaran a los motores que también estarán conectados a la Raspberry. 


# Software Architecture

![SoftwareImage](https://github.com/Tsantaella/SinglePong/blob/main/img/Software_Architecture.png)

This scheme shows us the steps followed at each iteration, the concatenation of steps is what gives us the ability to know where to move the robot. As we can see the camera takes an image of the scene, EnviaPython.py reads it and send it to the server, which calls BallDetection.py to detect the position and size of the ball, after this, the server sends the coordinates back to EnviaPython.cs which will move the robot.

# Ball Detection:
The ball detection module uses the colour and shape of the ball to find it's position inside the image, to make it robust to background noise we have used an unusual colour to paint the ball.

The algorithm detection works as follows:

1. We transform the image to the HSV space.
2. Since we know the ball colour we look for the range of values that match that colour. (in our case red).
3. We create a mask of the biggest cluster of that colour.
4. Finally we calculate the centroid's position.

The simplicity of the algorithm is due to the need of a fast detection of the position to be able to move the robot in time.


![Square](https://github.com/Tsantaella/SinglePong/blob/main/img/image009.png)




# Structural Components
●       ***Ping Pong Table:***  Our robot will be fixed on the table frame to give a stable and minimal structure.

![MesaPingPong](https://github.com/Tsantaella/SinglePong/blob/main/img/image011.png)


●       ***Estructura del robot:***  Estructura global del robot con los railes de movimiento y las barras de fijación las cuales van fijadas a la tabla.

![SinglePongImage](https://github.com/Tsantaella/SinglePong/blob/main/img/image013.png)


●       ***Sistema movimiento vertical:*** parte encargada del movimiento vertical del robot, los motores (amarillo) se encargan de subir y bajar la pala mediante los railes verticales

![SinglePongImage](https://github.com/Tsantaella/SinglePong/blob/main/img/image015.png)


●       ***Zona central:*** parte central del robot, donde se sitúa la pala con la que devolvemos la bola y la cámara con la que la detectamos.

![SinglePongImage](https://github.com/Tsantaella/SinglePong/blob/main/img/image017.png)

●       ***Sistema movimiento horizontal:** sistema situado junto la pala y la cámara el cual mediante un motor se desplaza en el eje horizontal.*

![SinglePongImage](https://github.com/Tsantaella/SinglePong/blob/main/img/image019.png)

# Simulation
![Simulation_Gif](https://github.com/Tsantaella/SinglePong/blob/main/img/simulation.gif)

To create the robot simulation we have used the Unity graphic engine, since it allows us to correctly simulate the physics involved in the elastic collisions.

To test the robot efectivity we'll simulate a ping pong match between the robot and the user. To do that we'll capture an image of the Unity scene to process it with the python script and obtain the ball position and area so we know where we have to move the racket to make it impact with the ball.

In order to stablish the comunication between the grapgic engine and our bal detection script, we have created two auxiliar scripts, one in python and one in C# which will have the role of client and server, stablishing a comunication via sockets.

Each iteration we take a new image and send it from unity to the python server, then we process the image in python and send it back to unity, where the robot will use the coordinates to move towards the ball.

Once the coordinates are recived we look at the sign of the ball position, for example, if they have a negative value on the "x" axis it means that we have to move the robot to the left, and if the "y" axis has a positive value it means that we have to move the robot upwards. We can do this because the camera is fixed to the rear of the racket, so if we want the ball to hit the center of the racket we just have to make sure that we follow it correctly.

Since the ball and the robot will be in movement when the images are taken, a noisy result can be obtained when the slight variation of the ball position respect the racket makes it have a "vibratory" movement, to avoid that, we use a small threshold, if the position  of the ball is smaller than the threshold, then we don't move the racket.

## Simulation Controls
During the simulation we'll be able to

➢     Rotate the camera with the mouse movement to control where we throw the ball.

➢     Throw a ball to the desired location each time we click.

This simple controls allows us to test all the possible movements of the robot, trying different angles and inclinations.

# Results 
The robot correctly detects the ball's position and moves towards it to intercept it, being very precise on the horizontal axis. The conection between the python and C#(unity) scripts also works perfectly and in the needed time. The simulation also works as desired, with  a feel of real physics on the ball and robot movements.

# Future Work
The algorithm used to move the robot would need to be improved to have a better performance on the vertical axis, since, even if it hits the ball most of the times, there are cases where the parabolic trajectory isn't contemplated fast enough and the ball slips under the racket. To avoid that we could improve the algorithm, calculating not only the actual position but what's going to be the final position. Although doing this can be easy, it's hard to implement it fast enough that the robot has time to move after the calculation and intercept the ball.

An other improvement  to implement is the racket rotation, to give angle and inclination to the ball when it impacts.


# References

We were inspired by some diferent projects  that aimed to do similar things, one in ping pong, the other in basketball. The main difference is the simpicity and size of our solution. This allows this project to be reproduced by anyone willing to spend a good time and learn, while the before mentioned projects are for a professional use or for a way bigger budget.



<https://www.youtube.com/watch?v=FycDx69px8U&ab_channel=StuffMadeHere>



https://www.youtube.com/watch?v=AxSyXMbV3Yg

