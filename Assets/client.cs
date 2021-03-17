using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class client : MonoBehaviour
{
    public GameObject myCube;
    public GameObject myGreenCube;

    private static byte[] sendBuffer = new byte[512];
    private static byte[] recvBuffer = new byte[512];
    private static IPEndPoint serverEndpoint;
    private static EndPoint memes;
    private static Socket client_socket;

    public static void RunClient()
    {
        serverEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111);
        memes = serverEndpoint;
        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        sendBuffer = Encoding.ASCII.GetBytes("Incoming Client");
        client_socket.SendTo(sendBuffer, memes);
    }

    void Start()
    {
        myCube = GameObject.Find("Cube");
        myGreenCube = GameObject.Find("Cube2");
        RunClient();
    }

    void Update()
    {
        sendBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());
        client_socket.SendTo(sendBuffer, serverEndpoint);
        if (client_socket.ReceiveFrom(recvBuffer, ref memes) > 0)
        {
            float x = System.BitConverter.ToSingle(recvBuffer, 0);
            myGreenCube.transform.position = new Vector3(x, myGreenCube.transform.position.y, myGreenCube.transform.position.z);
        }
    }
}