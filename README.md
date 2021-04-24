RPL - Computer Engineering. UAB (Barcelona) 2020-2021
# SINGLE PONG

Arnau Revelles Segalés - 1527873

Gerard Martínez Espelleta - 1531236

Toni Santaella Trujillo - 1526913

Kilian Niubó Vinuesa - 1497932

## Table of Contents

[Project description](#_heading=h.gjdgxs) 1

[Electronic components](#_heading=h.30j0zll) 1

[Hardware Scheme](#_heading=h.1fob9te) 2

[Software Architecture](#_heading=h.ik8oj8cnfs6x) 3

[Amazing contributions](#_heading=h.aulg56cwhsgi) 4

[Extra components and 3D pieces](#_heading=h.3znysh7) 5

[Simulation Strategy](#_heading=h.rx614yyclz3w) 6

[Foreseen risks and contingency plan](#_heading=h.2et92p0) 6

## Single Pong

# Project description

_Nuestro robot permite jugar al ping pong sin necesidad de una pareja._

_Consta de una estructura rectangular que se fija a una mesa de ping pong estándar._

_Esta estructura sujeta una parte central móvil que hará las funciones de pala de ping pong de nuestro robot._

_En la parte exterior de la estructura se instala una cámara. La cámara rastrea el movimiento de la pelota y manda la información a una Raspberry de forma que mediante unos motores se pueda ajustar la posición de la parte central en un eje bidimensional para colisionar con ella y, de este modo, poder devolverla a la parte contraria de la mesa._

# Electronic components

This is the list of the used components:

- _ **Raspberry Pi 4 2GB RAM** _
- _ **Motor NEMA** _
- _ **Cámara Raspberry pi V2 8MPX** _
- _ **Controlador I2c** _
- _ **Fuente Alimentación Raspberry Pi** _
- _ **Stepper Controller Pack 10u** _

# Hardware Scheme


_Dispondremos de una Raspberry a la que irá conectada la cámara. Con los datos recogidos por esta se realizarán los cálculos necesarios y se mandaran a los motores que también estarán conectados a la Raspberry._

# Software Architecture

- Calculador de posición: mediante diferentes frames capturados por la camará calculamos la trayectoria que la pelota sigue. Donde caerá la pelota lo calcularemos en un punto de la trayectoria donde el tiempo sea el menor posible para realizar el cálculo (mientras más cerca estemos del robot más preciso será el cálculo).
- Mover pala: en función de la posición calculada anteriormente la pala se mueve al punto donde cae la pelota.

# Amazing contributions

_Hemos encontrado que existen distintos tipos de robots que ya realizan una función similar al nuestro. La diferencia radica en la sencillez y el tamaño ya que en los robots encontrados se precisa de un gran espacio debido a que se tratan de grandes brazos robóticos con un sistema complejo de cámaras._

_Nuestro proyecto, debido a su sencilla estructura, facilita su transporte y su almacenaje, lo que lo haría idóneo para un uso quotidiano. Adicionalmente, sus características permiten una fácil instalación o desmontaje, de modo que se pueda volver a disponer de la mesa despejada en caso de que encontremos alguna pareja o montarlo en caso de que no la tengamos_

_Si se llegan a cumplir todos los requisitos establecidos al inicio, creemos que nuestro proyecto aspira a la mayor nota alcanzable, ya que se tratará de un robot funcional, sencillo y optimizado, que requerirá de dispositivos ópticos para el seguimiento de la pelota, cálculos para activar los motores en función de ello y partes móviles para impactar con la pelota, aplicando así parte de las competencias en materia de robótica adquiridas en la asignatura._

# Extra components and 3D pieces

- _ **Mesa de ping pong:** _ _nuestro robot estará enganchado a la mesa de ping-pong donde jugará el usuario._ ![](RackMultipart20210424-4-ru6xz6_html_883c20e5eaedc123.png)

- _ **Pala de ping pong:** _ _lo vamos a usar para devolver la pelota de ping-pong al jugador_ ![](RackMultipart20210424-4-ru6xz6_html_d150ea539bf0643f.png)

- _ **Partes de la estructura:** _ _la estructura del robot consta de un set de 4 piezas por las que se moverán dos barras a las que está enganchada la pala._ ![](RackMultipart20210424-4-ru6xz6_html_a3d07036361e991a.png) ![](RackMultipart20210424-4-ru6xz6_html_2d82ea969e45edc5.png)

#


# Simulation Strategy

Para realizar la simulación de nuestro robot, vamos a usar el motor gráfico de Unity.

Para poder comprobar la efectividad de nuestro robot, simularemos un lanzamiento de una pelota de ping pong contra el robot, entonces éste deberá detectar la pelota y calcular la posición donde colisionará mediante la función **calculadorDePosicion().** Posteriormente, usando la función **moverPala()** se ajustará la posición de la pala para devolver la pelota al campo contrario.

Este lanzamiento se realizará usando distintas velocidades de lanzamiento, desde diferentes ángulos y posiciones de la tabla de ping-pong, lo cual nos permitirá comprobar que el robot responde correctamente sea cual sea el ángulo de la pelota o la velocidad de ésta.

## References

This project has been inspired by the following Internet projects:

[https://www.youtube.com/watch?v=MHTizZ\_XcUM](https://www.youtube.com/watch?v=MHTizZ_XcUM)

https://www.youtube.com/watch?v=AxSyXMbV3Yg

###