using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

[System.Serializable]
public class EnviaPython : MonoBehaviour
{
    //creamos un socket
    SMsg socket = new SMsg();

    [SerializeField] int FPS = 1; //modificada desde unity

    //creamos dos variables de temporizadores
    float time = 0;
    float timer;

    //variables para almacenar las coordenadas y el area de la pelota
    float x, y;
    int area;

    //variable que permite que la pala no vibre
    [SerializeField] float limit = 5; //modificada desde unity

    //velocidad de la pala
    [SerializeField] float speed = 1.0f; //modificada desde unity

    //direccion del golpe
    [SerializeField] Vector3 golpe; //modificada desde unity

    //velocidad de rotacion
    [SerializeField] float rotationSpeed; // modificada desde unity

    //objeto del eje que sostiene la pala
    GameObject eix;

    //objeto de la pala
    GameObject pala;

    //el objeto esta rotado?
    public bool rotated = false;

    //Objeto de la camara
    Rigidbody rb;

    //temporizador para golpear la pelota
    float timerHit;
    float timeHit = 1.0f;


    void Start()
    {
        //buscamos el objeto eix y pala
        eix = GameObject.Find("Eix");
        pala = GameObject.Find("pala");


        //definimos el tiempo segun los FPS de la camara
        time = 1 / FPS;
        timer = time;

        //inicializamos las variables a 0
        x = y = 0;
        area = 0;

        //buscamos el objeto de RobotCamera
        rb = GameObject.Find("RobotCamera").GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        //vamos restando tiempo al temporizador
        timer -= Time.deltaTime;

        //cunado el tiempo es 0
        if (timer < 0)
        {
            //creamos un objeto camara
            Cam cam = new Cam();
            //capturamos una imagen desde la camara
            byte[] img = cam.Captura();
            //enviamos la imagen al servidor y recibimos las coordenadas y el area
            (x, y, area) = socket.Main(img);

            //reiniciamos el temporizador
            timer = time;
        }
    }



    //FixedUpdate se ejecuta mas de una vez por frame, por 
    //lo que es mejor para calcular las físicas
    private void FixedUpdate()
    {
        //movemos la pala a las coordenadas recibidas
        mouPala(x, y, area);
    }



    /// <summary>
    /// Función para mover la pala a partir de los datos
    /// previamente recibidos del socket
    /// </summary>
    /// <param name="x"> valor x de la pelota</param>
    /// <param name="y">valor y de la pelota</param>
    /// <param name="area">area de la pelota</param>
    async void mouPala(double x, double y, int area)
    {
        //guardamos la posición inicial de la pala
        Vector3 positionPala = transform.localPosition;
        //guardamos la posición inicial del eje
        Vector3 positionEix = eix.transform.localPosition;

        //definimos los vectores de movimiento vertical y horizontal
        //los cuales dependen de la velocidad de movimiento
        Vector3 horizontal = new Vector3(speed, 0, 0);
        Vector3 vertical = new Vector3(0, speed, 0);

        //calculamos el desplazamiento vertical y horizontal
        Vector3 despH = (horizontal * Time.deltaTime);
        Vector3 despV = (vertical * Time.deltaTime);



        //si la posicion de la pelota es inferior al límite aceptado
        if (x <= -limit)
        {
            //si la pala no se sale del eje
            if ((positionPala.x - despH.x) > -20)
            {
                //la movemos
                rb.MovePosition(transform.position + (-despH));
            }
        }

        //si la posicion de la pelota es superior al límite aceptado
        else if (x >= limit)
        {
            //si la pala no se sale del eje
            if ((positionPala.x + despH.x) < 118)
            {
                //la movemos
                rb.MovePosition(transform.position + (despH));
            }
        }
        //si la posicion de la pelota es inferior al límite aceptado
        if (y <= -limit)
        {
            //si la pala no se sale del eje
            if ((positionEix.y - despV.y) > -14)
            {
                //la movemos
                eix.transform.Translate(-despV);
            }

        }
        //si la posicion de la pelota es superior al límite aceptado
        else if (y >= limit)
        {
            //si la pala no se sale del eje
            if ((despV.y + positionEix.y) < 34)
            {
                //la movemos
                eix.transform.Translate(despV);
            }

        }

        //vamos descontando el tiempo de golpeo
        timerHit -= Time.deltaTime;

        //si el area es superior a lo establecido y
        //no hemos rotado aún la pala
        if ((area > 39000) && (!rotated))
        {
            //la marcamos como rotada
            rotated = true;
            //la rotamos
            pala.transform.RotateAround(pala.GetComponent<Renderer>().bounds.center, Vector3.left, rotationSpeed * Time.deltaTime);
        }

        //si el tiempo ha terminado
        else if (timerHit < 0)
        {
            //y la pala está rotada
            if (rotated)
            {
                //reseteamos el tiempo
                timerHit = timeHit;
                //marcamos la pala como no rotada
                rotated = false;
                //y la devolvemos a su posición original
                pala.transform.RotateAround(pala.GetComponent<Renderer>().bounds.center, Vector3.right, rotationSpeed * Time.deltaTime);
            }
        }
    }
}


//clase del socket
public class SMsg
{


    /// <summary>
    /// Función que nos permite conectarnos al servidor 
    /// mediante un socket
    /// </summary>
    /// <param name="server">dirección del socket a conectar</param>
    /// <returns>
    /// <param name="soc">socket con la conexión asignada</param>
    /// </returns>
    private Socket connecta(string server)
    {
        //creamos un nuevo socket tcp
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //lo asignamos a la dirección deseada
        System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
        //lo vinculamos al puerto 65430
        System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 65430);
        //realizamos la conexión
        soc.Connect(remoteEP);
        //devolvemos el socket conectado
        return soc;
    }


    /// <summary>
    /// Función para enviar datos al servidor
    /// </summary>
    /// <param name="soc"> socket al que enviar los datos</param>
    /// <param name="msg"> datos que enviar</param>
    private void envia(Socket soc, byte[] msg)
    {
        //enviamos el mensaje al servidor
        soc.Send(msg);
    }


    /// <summary>
    /// Función para recibir datos del socket
    /// </summary>
    /// <param name="listener">socket del que queremos recibir los datos</param>
    /// <returns>
    /// <param name="data">datos recibidos</param>
    /// </returns>
    private string rep(Socket listener)
    {
        //definimos las variables donde almacenaremos los datos
        string data = null;  
        byte[] bytes = null;  

        //reservamos el espacio
        bytes = new byte[1024];
        //recibimos del socket
        int bytesRec = listener.Receive(bytes);  
        //codificamos en ascii
        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        
        //devolvemos los datos
        return data;
    }


    /// <summary>
    /// Función principal del socket
    /// </summary>
    /// <param name="msg">imagen que pasaremos al socket</param>
    /// <returns>
    /// <param name="(x,y,area)">coordenadas de la pelota y area de la misma</param>
    /// </returns>
    public (float, float, int) Main(byte[] msg)
    {
        //conectamos el socket
        Socket s = connecta("127.0.0.1");
        
        //enviamos la imagen
        envia(s, msg);
        
        //recibimos respuesta
        string answ = rep(s);

        //creamos variables para almacenar la respuesta
        float x = 0;
        float y = 0;
        int area = 0;

        //si no ha dado error
        if(answ != "-1")
        {
            //parseamos los datos a float y int
            x = float.Parse(answ.Split(',')[0].Replace("'", "_").Split('_')[1], CultureInfo.InvariantCulture);
            y = float.Parse(answ.Split(',')[1].Replace("'", "_").Split('_')[1], CultureInfo.InvariantCulture);
            area = int.Parse(answ.Split(',')[2].Split(' ')[1].Split(')')[0]);
        }
        else
        {
            //en caso de que haya dado error definimos la posición a 0
            x = 0;
            y = 0;
        }
        
        //cerramos la conexión con el socket
        s.Close();
        
        //devolvemos coordenadas y area
        return (x,y, area);
    }
}


//función de la camara
public class Cam {


    /// <summary>
    /// Función para capturar una imagen con la camara
    /// </summary>
    /// <returns>
    /// <param name="bytes"> imagen capturada por la camara </param>
    /// </returns>
    public byte[] Captura()
    {
        //bucamos el objeto cámara
        Camera cam = GameObject.Find("RobotCamera").GetComponent<Camera>();

        //si no la encontramos informamos por LOG
        if(cam == null)
        {
            Debug.Log("cam NO trobada");
        }

        //definimos una textura de renderizado
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        //ponemos la camara a renderizar
        cam.Render();

        //definimos la imagen
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();

        //la codificamos a png
        RenderTexture.active = activeRenderTexture;
        byte[] bytes = image.EncodeToPNG();
        
        //la devolvemos
        return bytes;
    }
   
}

