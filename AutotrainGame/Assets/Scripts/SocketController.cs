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

public class SocketController : MonoBehaviour
{
    public delegate void keyDelegate(Vector3 values);
    public event keyDelegate keyEvent;
    public delegate void OnlineEvent();
    public event OnlineEvent ToggleOnline;

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
    int processID;
    // Data buffer para el incoming data.
    byte[] bytes = new byte[1024];
    char[] delimiterChars = { ',', '[', ']', '(', ')', ' ' };
    [HideInInspector]

    public string[] sPrueba = new string[8] { " 0", "0", " 0", "0", " 0", "0", " 0", "0" };


    float nextConnTry = 0;

    private void Start()
    {
        nextConnTry = Time.time + 0.5f;
        connectedButton.image.color = Color.red;
        RunSocketServer();
    }

    void Update()
    {

        if (online == false && Time.time > nextConnTry)
        {
            StartClient();
            nextConnTry = Time.time + 0.3f;
        }


    }

    public void StartClient()
    {
        if (online == false)
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
                    sender.Connect("localhost", 8888);
                    sender.ReceiveTimeout = 1;
                    connectedButton.image.color = Color.green;
                    ConnectionText.text = ("Socket connected to  " +
                    sender.RemoteEndPoint.ToString());
                    //UnityEngine.Debug.Log(receiveMessage());
                    ToggleOnline();
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
            returnText.text = retorno2string(sPrueba);
            //UnityEngine.Debug.Log(returnText.text);

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
            ToggleOnline();
            online = false;
        }

    }

    public void RunSocketServer()
    {
        p = new Process();
        //Buscar id proseso para matarlo
        p.StartInfo.FileName = "python";
        p.StartInfo.Arguments = "serverSocket.py";
        p.StartInfo.CreateNoWindow = true;
        // Where the script lives
        p.StartInfo.WorkingDirectory = Application.dataPath;
        p.StartInfo.UseShellExecute = false;

        try
        {
            p.Start();
            processID = p.Id;
            UnityEngine.Debug.Log(processID);
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
        //UnityEngine.Debug.Log("process-Kill on Quit");
        KillProcess();
    }

    //
    void OnDestroy()
    {
        //UnityEngine.Debug.Log("process-Kill on Destroy");
        KillProcess();
    }

    void KillProcess()
    {
        UnityEngine.Debug.Log(Process.GetProcessById(processID));
        if (Process.GetProcessById(processID) != null)
            Process.GetProcessById(processID).Kill();
        else
        {
            UnityEngine.Debug.Log("El proceso no existe");
        }
    }

    string retorno2string(string[] s)
    {
        StringBuilder sb = new StringBuilder();
        if (s.Length >= 5 )
        {
            sb.Append(s[3]);
            sb.Append(s[1]);
            sb.Append(s[5]);
            keyEvent(new Vector3(int.Parse(s[3]), int.Parse(s[1]), int.Parse(s[5])));
        }


        
        return sb.ToString();
    }


}
