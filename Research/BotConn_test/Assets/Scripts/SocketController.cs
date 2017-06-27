using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SocketController : MonoBehaviour {

    //Variable del editor
    public InputField ifield;
    public Text returnText;
    public Button connectedButton;
    public Button SocketButton;
    // Encode the data string into a byte array.
    private byte[] msg = Encoding.ASCII.GetBytes("This is a test");
    //Socket para la conexión
    Socket sender;
    // Data buffer for incoming data.
    byte[] bytes = new byte[1024];

    private void Start()
    {
        connectedButton.image.color = Color.red;
        SocketButton.image.color = Color.red;
    }

    public void StartClient()
    {
        // Connect to a remote device.
        try
        {
            // Establish the remote endpoint for the socket.
            // This example uses port 8888 on the local computer.
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            UnityEngine.Debug.Log(ipAddress.ToString());
            //IPEndPoint myEP = new IPEndPoint(IPAddress.Any, 8888);

            // Create a TCP/IP  socket.
            sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(ipAddress, 8888);
                connectedButton.image.color = Color.green;
                UnityEngine.Debug.Log(receiveMessage());
                        
                

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

    public void SetMsg(string message)
    {
        msg = Encoding.ASCII.GetBytes(message);
    }

    public void TextBoxMsg()
    {
        SetMsg(ifield.text);
        SendMessage();
        try
        {
            returnText.text = receiveMessage();
        }
        catch(SocketException e)
        {
            UnityEngine.Debug.Log("e:" + e.ToString());
        }
        //Si no recibo mensaje de vuelta se bloquea
        //ifield.text = receiveMessage();
    }

    public void SendMessage()
    {
        try
        {
            // Send the data through the socket.
            sender.Send(msg);
        }
        catch(SocketException se)
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
        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
        connectedButton.image.color = Color.red;
        SocketButton.image.color = Color.red;
    }

    public void RunSocketServer()
    {
        Process p = new Process();
        p.StartInfo.FileName = "python";
        p.StartInfo.Arguments = "serverSocket.py";
        // Pipe the output to itself - we will catch this later
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.RedirectStandardOutput = true;
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

        //UnityEngine.Debug.Log(p.ToString());
        //p.Close();
    }

   

}
