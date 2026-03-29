using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HPSocket;
using HPSocket.Tcp;
using System;
using System.Text;

public class Client : MonoBehaviour
{
    //Client单例
    public static Client Instance;
    //客户端链接对象
    TcpClient tcpClient = new TcpClient();

    void Awake()
    {
        Instance = this;
        //让当前游戏物体不被销毁,保证切换场景也可以进行发送消息的操作
        DontDestroyOnLoad(gameObject);
        //设置服务器的IP地址和端口
        tcpClient.Address = "127.0.0.1";
        tcpClient.Port = Convert.ToUInt16(5566);

        //回调
        //接收服务端发来的消息
        tcpClient.OnReceive += TcpClient_OnReceive;


        //链接客户端
        tcpClient.Connect();
    }

    private HandleResult TcpClient_OnReceive(IClient sender, byte[] data)
    {
        //测试接到服务端消息，打印出来
        string msg = Encoding.UTF8.GetString(data);
        Debug.Log(msg);

        return HandleResult.Ok;
    }

    void Update()
    {
        //测试客户端发送消息
        if (Input.GetMouseButtonDown(0))
        {
            byte[] data = Encoding.UTF8.GetBytes("你好，我是客户端");
            tcpClient.Send(data,data.Length);
        }
    }
}
