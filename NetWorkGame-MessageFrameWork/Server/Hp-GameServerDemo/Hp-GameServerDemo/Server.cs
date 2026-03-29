using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPSocket;
using HPSocket.Tcp;

namespace Hp_GameServerDemo
{
    public class Server
    {
        //声明一个TCP给的接受消息用的Socket服务端
        public TcpServer server = new TcpServer();
        //构造
        public Server()
        {
            //ip地址(服务器在网络中的IP，这里做测试用的本地ip)
            server.Address = "127.0.0.1";
            //端口
            server.Port = Convert.ToUInt16(5566);

            //三个回调
            //接收到客户端链接
            server.OnAccept += Server_OnAccept;
            //客户端断开链接
            server.OnClose += Server_OnClose;
            //接收到客户端发来的消息
            server.OnReceive += Server_OnReceive;



            //服务器启动
            server.Start();


        }

        //当收到客户端发来的消息会调用

        private HandleResult Server_OnReceive(IServer sender, IntPtr connId, byte[] data)
        {
            //测试接收客户端消息
            string msg = Encoding.UTF8.GetString(data);
            Console.WriteLine(msg);
            //给客户端回复一个消息，服务端收到了
            byte[] data2 = Encoding.UTF8.GetBytes("服务端收到了");
            //测试给该客户端回消息
            server.Send(connId, data2, data2.Length);

            return HandleResult.Ok;
        }

        private HandleResult Server_OnClose(IServer sender, IntPtr connId, SocketOperation socketOperation, int errorCode)
        {
            
            return HandleResult.Ok;
        }

        private HandleResult Server_OnAccept(IServer sender, IntPtr connId, IntPtr client)           
        {
            Console.WriteLine("有客户端成功链接");
            return HandleResult.Ok;
        }
    }
}
