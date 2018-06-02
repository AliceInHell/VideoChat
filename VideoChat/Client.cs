using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace VideoChat
{
    public class Client
    {
        public string remoteIP { set; get; }
        public static int remotePort { set; get; } = 8001;   //порт сервера
        public string myName { set; get; }
        public int myID { set; get; }
        public int usersID { set; get; }
        public bool isConnected { set; get; }
        public bool isCalling { set; get; }
        private static int localPort { set; get; } = 8002;     //мой порт
        private static int openPort { set; get; } = 8003;      //порт для звонков
        public List<user> usersList = new List<user>();
        public WebCamera myCamera;
        public ImageAndAudioTransfer myImageTransfer;
        public PictureBox hidden_Picturebox, uploadedVideo_pictureBox, receivedVideo_pictureBox;
        public ListBox GUIList;
        public Thread listeningThread = new Thread(new ParameterizedThreadStart(callListener));

        public struct user
        {
            public int id;
            public string name;
        }

        public Client(string name, PictureBox hidden_Picturebox, PictureBox uploadedVideo_pictureBox, PictureBox receivedVideo_pictureBox, ListBox list)
        {
            this.hidden_Picturebox = hidden_Picturebox;
            this.uploadedVideo_pictureBox = uploadedVideo_pictureBox;
            this.receivedVideo_pictureBox = receivedVideo_pictureBox;
            GUIList = list;
            this.myName = name;
        }

        private string LocalIpAddress()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        private string getUserName(int id)
        {
            foreach (user user in usersList)
                if (user.id == id)
                    return user.name;
            return "";
        }

        private void processing(string[] tmp)
        {
            try
            {
                if (tmp[0] == "4" && tmp.Length == 5)
                {
                    byte[] output;
                    UdpClient sender = new UdpClient();
                    DialogResult result = MessageBox.Show(
                    "ВХОДЯЩИЙ ВЫЗОВ ОТ " + getUserName(Convert.ToInt32(tmp[1])),
                    "ОТВЕТИТЬ?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        output = Encoding.Unicode.GetBytes(String.Format("4|T|{0}|{1}", myID, tmp[1]));
                        usersID = Convert.ToInt32(tmp[1]);
                        startDialog(Convert.ToInt32(tmp[3]), tmp[2]);
                        isCalling = true;
                    }
                    else
                        output = Encoding.Unicode.GetBytes(String.Format("4|F|{0}|{1}", myID, tmp[1]));
                    sender.Send(output, output.Length, remoteIP, remotePort);
                    sender.Close();
                }
                if (tmp[0] == "3")
                {
                    user tmpUser;
                    usersList.Clear();
                    GUIList.Items.Clear();

                    for (int i = 1; i < tmp.Length - 1; i += 2)
                    {
                        tmpUser.name = tmp[i];
                        tmpUser.id = Convert.ToInt32(tmp[i + 1]);
                        usersList.Add(tmpUser);
                    }

                    //заполнение интерфейсного списка
                    foreach (user usr in usersList)
                        GUIList.Items.Insert(GUIList.Items.Count, usr.name);
                }
                //если ответ получен, то 
                if (tmp[0] == "4" && tmp[1] != "0" && tmp.Length == 4)
                {
                    isCalling = true;
                    startDialog(Convert.ToInt32(tmp[2]), tmp[1]);
                }
                if (tmp[0] == "4" && tmp[1] == "0")
                    MessageBox.Show("Пользователь сбросил");
                if (tmp[0] == "5")
                {
                    isCalling = false;
                    ImageAndAudioTransfer.stopTranslation = true;
                    myCamera.stopTranslation();
                    disposeAndClear();
                    MessageBox.Show("Звонок окончен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.leaveServer();
            }
        }

        public void disposeAndClear()
        {
            ImageAndAudioTransfer.stopTranslation = true;
            this.myCamera.stopTranslation();
            this.hidden_Picturebox.Image = null;
            this.receivedVideo_pictureBox.Image = null;
            this.uploadedVideo_pictureBox.Image = null;
            myCamera = null;
        }

        public void stopDialog()
        {
            try
            {
                UdpClient sender = new UdpClient();
                byte[] data = Encoding.Unicode.GetBytes(String.Format("5|{0}|{1}", myID, usersID));
                sender.Send(data, data.Length, remoteIP, remotePort);
                sender.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void callListener(object x)
        {
            try
            {
                UdpClient receiver = new UdpClient(localPort);
                IPEndPoint remoteIP = null;
                byte[] data;
                while (true)
                {
                    data = receiver.Receive(ref remoteIP);
                    string[] tmp = (Encoding.Unicode.GetString(data)).Split('|');
                    ((Client)x).processing(tmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ((Client)x).leaveServer();
            }
        }

        public void leaveServer()
        {
            UdpClient sender = new UdpClient();
            byte[] data = Encoding.Unicode.GetBytes(String.Format("2|{0}", myID));
            sender.Send(data, data.Length, remoteIP, remotePort);
            sender.Close();
        }

        public void getUserList()
        {
            //отправка запроса
            UdpClient sender = new UdpClient();
            byte[] data = Encoding.Unicode.GetBytes(String.Format("3|{0}", myID));
            sender.Send(data, data.Length, remoteIP, remotePort);
            sender.Close();
        }

        public void connectToServer(string remoteIP)
        {
            try
            {
                //отправить пакет на подключение
                UdpClient sender = new UdpClient();
                byte[] data = Encoding.Unicode.GetBytes(String.Format("1|{0}|{1}|{2}|{3}", myName, LocalIpAddress(), localPort, openPort));
                sender.Send(data, data.Length, remoteIP, remotePort);
                sender.Close();

                UdpClient receiver = new UdpClient(localPort);
                IPEndPoint remoteIp = null;
                data = receiver.Receive(ref remoteIp);
                string[] tmp = (Encoding.Unicode.GetString(data)).Split('|');

                if (tmp[0] == "1" && tmp[1] != "0" && tmp.Length == 2)
                {
                    this.isConnected = true;
                    this.remoteIP = remoteIP;
                    myID = Convert.ToInt32(tmp[1]);
                    this.remoteIP = remoteIP;
                    MessageBox.Show("WELCOME!");
                    receiver.Close();
                    listeningThread.Start(this);
                }
                else
                    receiver.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void call(int index)
        {
            //отправить пакет серверу на звонок другому пользователю в списке по index
            UdpClient sender = new UdpClient();
            byte[] data = Encoding.Unicode.GetBytes(String.Format("4|{0}|{1}", myID, usersList[index].id));
            usersID = usersList[index].id;
            sender.Send(data, data.Length, remoteIP, remotePort);
            sender.Close();
        }

        public void startDialog(int remoteUserPort, string remoteIP)
        {
            this.myImageTransfer = new ImageAndAudioTransfer(remoteIP, Convert.ToInt32(openPort), Convert.ToInt32(remoteUserPort));
            this.myCamera = new WebCamera();

            myCamera.startTranslation(hidden_Picturebox, uploadedVideo_pictureBox);
            Thread.Sleep(100);

            Thread frameThread = new Thread(new ParameterizedThreadStart(WebCamera.setFrame));
            frameThread.Name = "frameThread";
            frameThread.SetApartmentState(ApartmentState.MTA);
            frameThread.Start(myCamera);

            Thread sendThread = new Thread(new ParameterizedThreadStart(ImageAndAudioTransfer.SendMessage));
            sendThread.Name = "sendThread";
            sendThread.IsBackground = true;
            sendThread.SetApartmentState(ApartmentState.MTA);
            sendThread.Start(myCamera);

            Thread receiveThread = new Thread(new ParameterizedThreadStart(ImageAndAudioTransfer.ReceiveMessage));
            receiveThread.Name = "receiveThread";
            receiveThread.IsBackground = true;
            receiveThread.SetApartmentState(ApartmentState.MTA);
            receiveThread.Start(receivedVideo_pictureBox);
        }

    }
}
