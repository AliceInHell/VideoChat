using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace udp_chat
{
    class Program
    {
        public struct User
        {
            public int id;
            public IPAddress ip;
            public int portForServer;
            public int portForClient;
            public string name;
            public bool call;
        }

        public struct Packet
        {
            public byte[] message;
            public IPAddress ip;
            public int port;
        }

        public static List<User> users;
        const int remotePort = 8002;
        const int localPort = 8001;
        public static Thread receiveThread;
        static void Main(string[] args)
        {
            Console.WriteLine(LocalIpAddress());
            users = new List<User>();
            receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
        }

        private static void SendMessage(Packet pack)
        {
            UdpClient sender = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(pack.ip, pack.port);
            try
            {            
                sender.Send(pack.message,pack.message.Length,endPoint);
               // Console.WriteLine(Encoding.Unicode.GetString(pack.message));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort);
            IPEndPoint remoteIP = null;
            string localAddress = LocalIpAddress();
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIP);
                    string message = Encoding.Unicode.GetString(data);
                    Console.WriteLine(String.Format("Client : {0}",message));
                    Packet pack = Analyzer(message);
                    if (pack.port != 0)
                    {
                        //Thread.Sleep(500);
                        SendMessage(pack);
                        Console.WriteLine(String.Format("Server : {0}",Encoding.Unicode.GetString(pack.message)));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

        private static Packet Analyzer(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            if (message!="")
            {
                switch (message[0])
                {
                    case '0':
                        {
                            return FindServer(message);
                        }
                    case '1':
                        {
                            return Welcome(message);
                        }
                    case '2':
                        {
                            return ByeUser(message);
                        }
                    case '3':
                        {
                            return OnlineList(message);
                            //DispatchOnlineList(0);
                        }
                    case '4':
                        {
                            return Calling(message);
                        }
                    case '5':
                        {
                            return StopDialog(message);
                        }
                }
            }
            return pack;
            
        }

        public static Packet Welcome(string message)
        {
            Packet pack;
            String[] temp = message.Split('|');
            User element = new User();
            element.name = temp[1];
            element.ip = IPAddress.Parse(temp[2]);
            element.portForServer = Int32.Parse(temp[3]);
            element.portForClient = Int32.Parse(temp[4]);
            element.id = createId();
            pack.ip = element.ip;
            pack.port = element.portForServer;
            
            bool flag = true;
            foreach(User el in users)
            {
                if (element.name == el.name)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                element.call = false;
                users.Add(element);
                //DispatchOnlineList();
                pack.message= Encoding.Unicode.GetBytes(String.Format("1|{0}",element.id));
            }
            else
            {
               pack.message = Encoding.Unicode.GetBytes("1|0");
            }
            return pack;
        }

        public static Packet OnlineList(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            String[] temp = message.Split('|');

            int id = Int32.Parse(temp[1]);
            DispatchOnlineList(id,true);
            /*string stringList = "3";
            foreach (User element in users)
            {
                if (element.id == id)
                {
                    pack.ip = element.ip;
                    pack.port = element.portForServer;
                    continue;
                }
                stringList += "|";
                stringList += element.name;
                stringList += "|";
                stringList += element.id;
            }*/

            //pack.message = Encoding.Unicode.GetBytes(stringList);
            return pack;
        }

        public static void DispatchOnlineList(int id,bool flag)
        {
            UdpClient sender = new UdpClient();
            foreach (User element in users)
            {
                string stringList = "3";
                foreach (User el in users)
                {
                    if (element.id == el.id)
                    {
                        continue;
                    }
                    if (!flag && el.id == id)
                        continue;
                    stringList += "|";
                    stringList += el.name;
                    stringList += "|";
                    stringList += el.id;
                }
                if (!flag && element.id == id)
                    continue;
                sender.Send(Encoding.Unicode.GetBytes(stringList), Encoding.Unicode.GetBytes(stringList).Length, element.ip.ToString(), element.portForServer);
            }
        }

        public static Packet FindServer(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            String[] temp = message.Split('|');

            pack.ip = IPAddress.Parse(temp[1]);
            pack.port = Convert.ToInt32(temp[2]);
            pack.message = Encoding.Unicode.GetBytes(String.Format("{0}|{1}",LocalIpAddress(),localPort.ToString()));
            return pack;
        }

        public static Packet StopDialog(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            String[] temp = message.Split('|');
            foreach (User element in users)
            {
                if (element.id == Convert.ToInt32(temp[1]))
                    if (!element.call)
                        return pack;
            }
            foreach (User element in users)
            {
                if (element.id==Convert.ToInt32(temp[2]))
                {
                    pack.ip = element.ip;
                    pack.port = element.portForServer;
                }
            }
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].id == Convert.ToInt32(temp[1]))
                {
                    User el = users[i];
                    el.call = false;
                    users[i] = el;
                }
                if (users[i].id == Convert.ToInt32(temp[2]))
                {
                    User el = users[i];
                    el.call = false;
                    users[i] = el;
                }
            }
            pack.message = Encoding.Unicode.GetBytes("5");
            return pack;
        }

        public static Packet Calling(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            String[] temp = message.Split('|');

            if (temp[1]=="T" || temp[1]=="F")
            {
                if (temp[1]=="T")
                {
                    string answer = "4|";
                    foreach (User element in users)
                    {
                        if (element.id == Convert.ToInt32(temp[3]))
                        {
                            pack.ip = element.ip;
                            pack.port = element.portForServer;
                        }
                        if (element.id == Convert.ToInt32(temp[2]))
                        {
                            answer += element.ip.ToString();
                            answer += "|";
                            answer += element.portForClient.ToString();
                            answer += "|";
                            answer += temp[2];
                        }
                    }
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].id == Convert.ToInt32(temp[2]))
                        {
                            User el = users[i];
                            el.call = true;
                            users[i] = el;
                        }
                        if (users[i].id == Convert.ToInt32(temp[3]))
                        {
                            User el = users[i];
                            el.call = true;
                            users[i] = el;
                        }
                    }
                    pack.message = Encoding.Unicode.GetBytes(answer);
                }  
                else
                {
                    foreach (User element in users)
                    {
                        if (element.id==Convert.ToInt32(temp[3]))
                        {
                            pack.ip = element.ip;
                            pack.port = element.portForServer;
                            break;
                        }
                    }
                    pack.message = Encoding.Unicode.GetBytes(String.Format("4|0",temp[2]));
                }
            }
            else
            {
                IPAddress ip = null;
                int port = 0;
                foreach (User element in users)
                {
                    if (element.id == Convert.ToInt32(temp[2]))
                    {
                        if (element.call)
                        {
                            return pack;
                        }
                        pack.ip = element.ip;
                        pack.port = element.portForServer;
                    }
                    if (element.id==Convert.ToInt32(temp[1]))
                    {
                        ip = element.ip;
                        port = element.portForClient;
                    }
                }
                for (int i=0;i<users.Count;i++)
                    if (users[i].id == Convert.ToInt32(temp[1]))
                    {
                        User element = users[i];
                        element.call = true;
                        users[i] = element;
                        break;
                    }
                pack.message = Encoding.Unicode.GetBytes(String.Format("4|{0}|{1}|{2}|0",temp[1],ip,port));
            }
            return pack;
        }
        

        private static int createId()
        {
            int tempId = 0;
            bool flag = false;
            if (users.Count == 0)
                return 1;
            while (!flag)
            {
                tempId++;
                foreach (User el in users)
                {
                    if (tempId == el.id)
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                }
            }
            return tempId;
        }

        public static Packet ByeUser(string message)
        {
            Packet pack;
            pack.ip = null;
            pack.port = 0;
            pack.message = null;
            String[] temp = message.Split('|');
            DispatchOnlineList(Int32.Parse(temp[1]),false);
            foreach (User element in users)
            {
                if (element.id== Int32.Parse(temp[1]))
                {
                    users.Remove(element);
                    break;
                }
            }
            
            return pack;
        }
        

        private static string LocalIpAddress()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;

        }

        
    }

    
}
