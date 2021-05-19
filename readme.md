+----------------------------------------------------------------------+
|   ----------------                                                   |
| -------------------------------------------------------------------- |
|   ![](media/imag                                                     |
| e14.png){width="4.9099475065616796in" height="2.7536198600174977in"} |
|   ----------------                                                   |
| -------------------------------------------------------------------- |
+======================================================================+
| SINGLE PONG                                                          |
|                                                                      |
| Single pong permite jugar al ping pong sin necesidad de una pareja.  |
+----------------------------------------------------------------------+
| PROJECT SPRINT \#2.\                                                 |
| DATE: 28^th^ April 2021                                              |
+----------------------------------------------------------------------+

Arnau Revelles Segalés - 1527873

Gerard Martínez Espelleta - 1531236

Toni Santaella Trujillo - 1526913

Kilian Niubó Vinuesa - 1497932\
Table of Contents

[Project description](#project-description) 1

[Electronic components](#electronic-components) 1

[Hardware Scheme](#hardware-scheme) 2

[Software Architecture](#software-architecture) 3

[Ball Detection:](#ball-detection) 3

[Amazing contributions](#amazing-contributions) 5

[Extra components and 3D pieces](#extra-components-and-3d-pieces) 6

[Simulation Strategy](#simulation-strategy) 9

[Foreseen risks and contingency
plan](#foreseen-risks-and-contingency-plan) 9

References 11

Single Pong

Project description
===================

*SinglePong permite jugar al ping pong sin necesidad de una pareja.*

*Consta de una estructura rectangular que se fija a una mesa de ping
pong estándar.*

*Esta estructura sujeta una parte central móvil que hará las funciones
de pala de ping pong de nuestro robot.*

*En la parte exterior de la estructura se instala una cámara. La cámara
rastrea el movimiento de la pelota y manda la información a una
Raspberry de forma que mediante unos motores se pueda ajustar la
posición de la parte central en un eje bidimensional para colisionar con
ella y, de este modo, poder devolverla a la parte contraria de la mesa.*

Electronic components
=====================

Esta es la lista de componentes:

-   ***Raspberry Pi 4 2GB RAM***

-   ***Motor NEMA***

-   ***Cámara Raspberry pi V2 8MPX***

-   ***Controlador I2c***

-   ***Fuente Alimentación Raspberry Pi***

-   ***Stepper Controller Pack 10u***

Hardware Scheme
===============

![](media/image7.png){width="5.661458880139983in"
height="2.685563210848644in"}

![](media/image10.png){width="3.486365923009624in"
height="2.1954057305336834in"}

El esquema básico con el que manejaremos los motores será el de la
figura de arriba.

Dispondremos de una Raspberry a la que irá conectada la cámara. Con los
datos recogidos por esta se realizarán los cálculos necesarios y se
mandaran a los motores que también estarán conectados a la Raspberry.

Software Architecture
=====================

![](media/image13.png){width="6.267716535433071in"
height="1.4722222222222223in"}

Es un esquema bastante sencillo de como funcionará la parte software del
robot. En este entendemos que a partir de varios frames donde se vaya
detectando la pelota podremos saber la trayectoria que esta sigue,
calcular donde impactará y mover el robot a la posición.

Ball Detection:
===============

El módulo de ball detection ha sido actualizado pero sigue en la misma
línea que ya teníamos codificada. si bien es cierto que la apariencia es
distinta en el momento de la detección de la pelota, el proceso para
capturar la imagen es muy similar.

En el momento de lanzar el código se nos activa la cámara y comienza a
capturar frames, de cara a reconocer mediante el color la esfera y a
partir de ella detectar la posición respecto al centro de la cámara.

El funcionamiento es el siguiente:

1.  Pasamos la imagen al espacio de colores HSV.

2.  Debido a que la pelota será de un color determinado buscamos el
    > rango de color de la pelota (en este caso naranja).

3.  Creamos una máscara para obtener la zona que nos interesa (la
    > pelota).

4.  Finalmente dibujamos unos ejes que nos dividan la imagen de la
    > cámara y otros que nos indiquen la posición de la pelota,
    > obteniendo así sus coordenadas.

![](media/image11.png){width="2.776042213473316in"
height="2.131603237095363in"}

Pese a que actualmente este método de detección de la pelota nos parece
suficiente para realizar las funcionalidades previstas inicialmente, se
pretende, en caso de disponer del tiempo suficiente, realizar un análogo
mediante inteligencia artificial. Si logramos su desarrollo se
compararan las dos alternativas por tal de quedarnos con la más rápida,
ya que, por una parte, creemos que la sencillez de la primera opción
puede darnos resultados más veloces con el fin de mover antes la pala
hacia la dirección deseada, pero por otra, con la segunda, esperamos
obtener resultados más precisos, adquiriendo así mayor precisión en los
movimientos y mayor capacidad de detectar comportamientos de la pelota
como efectos.

La posición de la cámara se encontrara en el centro de la pala ya que
hemos convenido que es la opción más óptima en este momento. Dado que la
posición relativa de la pala respecto a los ejes de la estructura va
variando, se antoja más complicado realizar predicciones o entrenar a
una posible IA. Sin embargo, las funcionalidades básicas con el método
escogido parecen a priori más asequibles y están dando buenos
resultados.

Amazing contributions 
=====================

Hemos encontrado que existen distintos tipos de robots que ya realizan
una función similar al nuestro. La diferencia radica en la sencillez y
el tamaño ya que en los robots encontrados se precisa de un gran espacio
debido a que se tratan de grandes brazos robóticos con un sistema
complejo de cámaras.

Nuestro proyecto, debido a su sencilla estructura, facilita su
transporte y su almacenaje, lo que lo haría idóneo para un uso
quotidiano. Adicionalmente, sus características permiten una fácil
instalación o desmontaje, de modo que se pueda volver a disponer de la
mesa despejada en caso de que encontremos alguna pareja o montarlo en
caso de que no la tengamos

Si se llegan a cumplir todos los requisitos establecidos al inicio,
creemos que nuestro proyecto aspira a la mayor nota alcanzable, ya que
se tratará de un robot funcional, sencillo y optimizado, que requerirá
de dispositivos ópticos para el seguimiento de la pelota, cálculos para
activar los motores en función de ello y partes móviles para impactar
con la pelota, aplicando así parte de las competencias en materia de
robótica adquiridas en la asignatura.

Extra components and 3D pieces
==============================

-   ***Mesa de ping pong:*** Nuestro robot estará sujeto en el borde de
    > la mesa de ping-pong donde jugará el
    > usuario.![](media/image5.png){width="2.872055993000875in"
    > height="1.6569553805774277in"}

-   ***Estructura del robot:*** Estructura global del robot con los
    > railes de movimiento y las barras de fijación las cuales van
    > fijadas a la tabla.

![](media/image4.png){width="4.765625546806649in"
height="2.1995188101487315in"}

-   ***Sistema movimiento vertical:*** parte encargada del movimiento
    > vertical del robot, los motores (amarillo) se encargan de subir y
    > bajar la pala mediante los railes verticales

![](media/image12.png){width="3.744792213473316in"
height="2.6438549868766406in"}

-   ***Zona central:*** parte central del robot, donde se sitúa la pala
    > con la que devolvemos la bola y la cámara con la que la
    > detectamos.

![](media/image8.png){width="3.7343755468066493in"
height="2.7782852143482066in"}

-   ***Sistema movimiento horizontal:** sistema situado junto la pala y
    > la cámara el cual mediante un motor se desplaza en el eje
    > horizontal.*

![](media/image6.png){width="3.394701443569554in"
height="2.368208661417323in"}

Simulation Strategy
===================

Para realizar la simulación de nuestro robot, hemos usado el motor
gráfico de Unity, ya que nos permite simular correctamente las físicas
necesarias para comprobar que el robot funciona como es debido.

Para poder comprobar la efectividad de nuestro robot, simularemos una
partida de ping pong entre el robot y el usuario mediante el motor
gráfico Unity 3D. Para conseguir eso, capturaremos la imagen vista desde
la cámara de nuestro robot en unity y la trataremos con un script de
python, utilizando herramientas de visión por computador que nos
permitirán calcular la dirección de la pelota y predecir donde tenemos
que mover la pala para que impacte con la bola.

Con tal de establecer la comunicación entre el motor gráfico y nuestro
script en python hemos creado dos scripts, uno en C\# y otro en python
que tomarán la función de cliente y servidor comunicándose mediante
sockets.

Cada cierto tiempo tomamos una imágen de la cámara y la pasaremos desde
el cliente (unity) al servidor (python), entonces desde este último
trataremos la imágen y devolveremos las coordenadas de la imágen donde
hemos detectado que hay una pelota.

Una vez recibidas las coordenadas en el simulador, nos fijamos en el
signo de éstas. Si las coordenadas en "x" son positivas, eso significa
que la pelota se encuentra a la derecha de nuestra imagen, o lo que es
lo mismo, a la derecha de nuestra cámara. En caso contrario, si las
coordenadas son negativas, sabemos que se encontrará a la izquierda de
ésta.\
Podemos aplicar la misma lógica a las coordenadas "y".

Además, podemos definir un "threshold" que nos ayudará a ignorar
pequeños movimientos de la pelota con el fín de evitar que la pala esté
continuamente "temblando".

Simulation Controls
===================

Durante la ejecución de la simulación de nuestro robot seremos capaces
de:

-   Rotar la cámara con el ratón para poder controlar donde lanzamos la
    > pelota.

-   Lanzar una pelota hacia la dirección deseada para comprobar el
    > funcionamiento del robot.

Los controles son simples y parecidos a los de cualquier juego
convencional de disparos. Con el ratón rotamos la cámara y con el click
izquierdo del mismo lanzamos una pelota.

Results
=======

Ya hemos podido realizar las primeras pruebas con la primera versión del
robot. Hemos logrado realizar el primer objetivo planteado: Detectar
correctamente la bola y mover la pala hacia esta. La pala solo sirve
para que la pelota rebote, aún no golpea con efecto a esta para
devolverla de manera que el robot pueda hacer el papel de oponente.

Destacar que en este punto del proyecto el robot aún no es capaz de dar
impulso a la pelota para devolverla con más energía.

![](media/image9.png){width="3.807292213473316in"
height="2.3333333333333335in"}

Foreseen risks and contingency plan
===================================

  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  **Risk \#**   **Description**                                   **Probability\        **Impact\                 **Contingency plan**
                                                                  **(High/Medium/Low)   **(High/Medium/Low**)**   
  ------------- ------------------------------------------------- --------------------- ------------------------- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  1             Insuficiente fuerza en los motores                Medium                High                      Se tratarán de optimizar los algoritmos de trackeo y movimiento para que no se necesite tanta velocidad. Adicionalmente y si fuese necesario se trataría de mejorar el sistema motor, ya sea con número o potencia de motores.

  2             Falta de protección en la cámara                  Media                 Medio                     Pese a no ser un riesgo alto debido a que las pelotas son ligeras, se debería contemplar la posibilidad de proteger la cámara.

  3             Falta de fuerza de agarre del soporte a la mesa   Baja                  Alto                      En caso de precisarse se usarán soportes extra.
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

References

This project has been inspired by the following Internet projects:

<https://www.youtube.com/watch?v=MHTizZ_XcUM>

https://www.youtube.com/watch?v=AxSyXMbV3Yg

### 
