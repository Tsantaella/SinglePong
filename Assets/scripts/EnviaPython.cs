using System.Runtime.CompilerServices;
//using System.Reflection.PortableExecutable;
//using System.Diagnostics;
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


[System.Serializable]
public class EnviaPython : MonoBehaviour
{
    SMsg socket = new SMsg();

    [SerializeField] float time = 5.0f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
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
            socket.Main(img);
            timer = time;
        }
    }
}


public class SMsg
{
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

    private string rep(Socket soc)
    {
        byte[] b = new byte[786432];
        int k = soc.Receive(b);
        string recv = Encoding.ASCII.GetString(b,0,k); 

        return recv;
    }

    public void Main(byte[] msg)
    {
        Socket s = connecta("127.0.0.1");
        Debug.Log("socket obert");
        envia(s, msg);
        Debug.Log("missatge enviat");
        //string answ = rep(s);
        //Debug.Log(answ);
        s.Close();
        Debug.Log("socket tancat");
    }
}

public class Cam : MonoBehaviour {
 

 
    public byte[] Captura()
    {
        Debug.Log("pre cam trobada");

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
        File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + 1 + ".png", bytes);

        return bytes;
    }
   
}

