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

    public InputField ifield;
    // Encode the data string into a byte array.
    private byte[] msg = Encoding.ASCII.GetBytes("This is a test");
    //Socket para la conexión
    Socket sender;
    // Data buffer for incoming data.
    byte[] bytes = new byte[1024];

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       
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
                UnityEngine.Debug.Log("Socket connected to {0}" +
                    sender.RemoteEndPoint.ToString());          
                

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
        msg = Encoding.ASCII.GetBytes(ifield.text);
    }

    public void SendMessage()
    {
        try
        {
            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
        }
        catch(SocketException se)
        {
            UnityEngine.Debug.Log("SocketException : {0}" + se.ToString());
        }  
    }

    string receiveMessage()
    {
        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        return ("Echoed test = {0}" +
            Encoding.ASCII.GetString(bytes, 0, bytesRec));

    }

    public void releaseSocket()
    {
        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }

}
