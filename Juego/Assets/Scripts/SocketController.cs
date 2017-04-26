﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SocketController : MonoBehaviour
{
    Process p;
    public bool online = false;
    //Variable del editor
    public Text ConnectionText;
    public Text returnText;
    public Button connectedButton;
    public Button SocketButton;
    // Codifica data string into a byte array.
    private byte[] msg = null;
    //Socket para la conexión
    Socket sender;
    // Data buffer para el incoming data.
    byte[] bytes = new byte[1024];
    char[] delimiterChars = {',', '[', ']', '(',')', ' '};
    [HideInInspector]
    
    public string[] sPrueba =  new string[8] {" 0", "0", " 0", "0", " 0", "0", " 0", "0"};


    private void Start()
    {
        connectedButton.image.color = Color.red;
        SocketButton.image.color = Color.red;
        //RunSocketServer();
    }


    public void StartClient()
    {
        if (!online)
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 8888 on the local computer.
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                UnityEngine.Debug.Log(ipAddress.ToString());

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);



                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(ipAddress, 8888);
                    sender.ReceiveTimeout = 1;
                    connectedButton.image.color = Color.green;
                    ConnectionText.text = ("Socket connected to  " +
                    sender.RemoteEndPoint.ToString());
                    //UnityEngine.Debug.Log(receiveMessage());
                    online = true;

                }
                catch (ArgumentNullException ane)
                {
                    UnityEngine.Debug.Log("ArgumentNullException : {0}" + ane.ToString());
                }
                catch (SocketException se)
                {
                    UnityEngine.Debug.Log("SocketException : {0}" + se.ToString());
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.Log("Unexpected exception : {0}" + e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

    public void SetMsg(string message)
    {
        msg = Encoding.ASCII.GetBytes(message);
    }


    public void SendMessage()
    {
        try
        {
            // Send the data through the socket.
            sender.Send(msg);
            sPrueba = receiveMessage().Split(delimiterChars);
            returnText.text = retorno2string(sPrueba) ;
            UnityEngine.Debug.Log(returnText.text);

        }
        catch (SocketException se)
        {
            UnityEngine.Debug.Log("SocketException :" + se.ToString());
        }

    }

    string receiveMessage()
    {
        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        return (Encoding.ASCII.GetString(bytes, 0, bytesRec));

    }

    public void releaseSocket()
    {
        if (online)
        {
            // Release the socket.
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
            connectedButton.image.color = Color.red;
            SocketButton.image.color = Color.red;
            online = false;
        }

    }

    public void RunSocketServer()
    {
        p = new Process();
        ///Buscar id proseso para matarlo
        p.StartInfo.FileName = "python";
        p.StartInfo.Arguments = "serverSocket.py";
        p.StartInfo.CreateNoWindow = true;
        // Where the script lives
        p.StartInfo.WorkingDirectory = Application.dataPath;
        p.StartInfo.UseShellExecute = false;

        try
        {
            p.Start();
            SocketButton.image.color = Color.green;
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.Log(e.ToString());
            SocketButton.image.color = Color.red;
        }

        
    }


    //Mato el proceso de pythom
    void OnApplicationQuit()
    {
        UnityEngine.Debug.Log("process-Kill on Quit");
        Process[] pname = Process.GetProcessesByName("python");
        if (pname.Length != 0)
            p.Kill();
    }

    //
    void OnDestroy()
    {
        UnityEngine.Debug.Log("process-Kill on Destroy");
        Process[] pname = Process.GetProcessesByName("python");
        if (pname.Length != 0)
            p.Kill();
    }

     string retorno2string(string[] s) {
        StringBuilder sb = new StringBuilder();

        sb.Append(s[3]);
        sb.Append(s[1]);
        sb.Append(s[5]);
        return sb.ToString();
    }


}