using System.Runtime.InteropServices;
//using System.Diagnostics;
//using System.Threading;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
////////////////////////////////
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
/////////////////////////////////
using System.Globalization;

[System.Serializable]
public class EnviaPython : MonoBehaviour
{
    SMsg socket = new SMsg();

    [SerializeField] int FPS = 1; //modificada desde unity
    float time = 0; 
    float timer;
    
    //float timer_;
    //float time_ = 0;
    float x,y;
    int area;
    [SerializeField] float limit = 5; //modificada desde unity
    [SerializeField] float speed = 1.0f; //modificada desde unity
    GameObject eix;
    //Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        eix = GameObject.Find("Eix");
        time = 1/FPS;
        timer = time;
        x = y = 0;
        area = 0;
        //originalPos = GameObject.Find("RobotCamera").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            //Debug.Log("premain");
            Cam cam = new Cam();
            byte[] img = cam.Captura();
            (x, y, area) = socket.Main(img);
            mouPala(x,y,area);
            timer = time;
        }
    }

    void mouPala(double x, double y, int area)
    {
        Vector3 positionPala = transform.localPosition;
        Vector3 positionEix = eix.transform.localPosition;
        //print(positionEix);


        Vector3 horizontal = new Vector3(speed, 0, 0);
        Vector3 vertical = new Vector3(0, 0, speed);
        Vector3 despH = (horizontal * Time.deltaTime);
        Vector3 despV = (vertical * Time.deltaTime);

        if (x <= -limit) {
            if ((positionPala.x - despH.x) > -20)
            {

                transform.Translate(-1f * despH);
            }
        }

        else if (x >= limit) {
            print(limit);
            if ((positionPala.x + despH.x) < 118)
            {

                transform.Translate(despH);
            }
        }

        if (y <= -limit) {
            if ((positionEix.z - despV.z) > -14)
                eix.transform.Translate(-despV);
        }
        else if (y >= limit)
        {
            if ((despV.z + positionEix.z) < 34)
                eix.transform.Translate(despV);
        }      
    }
}


public class SMsg
{
    // private Socket activa(string server)
    // {
    //     Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //     System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
    //     System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 65430);
    //     soc.Bind(remoteEP);  
    //     return soc;
    // }
    private Socket connecta(string server)
    {
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
        System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 65430);
        soc.Connect(remoteEP);
        return soc;
    }

    private void envia(Socket soc, byte[] msg)
    {
        //byte[] byData = BitConverter.GetBytes(msg);//System.Text.Encoding.ASCII.GetBytes(msg);
        soc.Send(msg);
    }

    private string rep(Socket listener)
    {
        // listener.Listen(5);
        // Socket handler = listener.Accept(); 
        string data = null;  
        byte[] bytes = null;  

        bytes = new byte[1024];  
        int bytesRec = listener.Receive(bytes);  
        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

        return data;
    }

    public (float, float, int) Main(byte[] msg)
    {
        Socket s = connecta("127.0.0.1");
        //Debug.Log("socket obert");
        envia(s, msg);
        //Debug.Log("missatge enviat");
        //s.Close();
        //Debug.Log("socket tancat");

        //s = activa("127.0.0.1");
        //Debug.Log("socket obert");
        string answ = rep(s);
        float x = 0;
        float y = 0;
        int area = 0;
        if(answ != "-1")
        {
            x = float.Parse(answ.Split(',')[0].Replace("'", "_").Split('_')[1], CultureInfo.InvariantCulture);
            y = float.Parse(answ.Split(',')[1].Replace("'", "_").Split('_')[1], CultureInfo.InvariantCulture);
            area = int.Parse(answ.Split(',')[2].Split(' ')[1].Split(')')[0]);
            //area = float.Parse(answ.Split(',')[2].Substring(1), CultureInfo.InvariantCulture);
            //print(area);
            //Debug.Log();
            //Debug.Log(y);
        }
        else
        {
            x = 0;
            y = 0;
        }
        
        s.Close();
        //Debug.Log("socket tancat");
        return (x,y,area);
    }
}

public class Cam {
 

 
    public byte[] Captura()
    {
        //Debug.Log("pre cam trobada");

        Camera cam = GameObject.Find("RobotCamera").GetComponent<Camera>();

        if(cam == null)
        {
            Debug.Log("cam NO trobada");
        }

        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        cam.Render();

        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();

        RenderTexture.active = activeRenderTexture;
        byte[] bytes = image.EncodeToPNG();
        //Destroy(image);
        //File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + 1 + ".png", bytes);

        return bytes;
    }
   
}

