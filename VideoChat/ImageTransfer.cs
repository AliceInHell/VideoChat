using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace VideoChat
{
    class ImageTransfer
    {
        public static int localPort, remotePort;
        //public static IPAddress remote_address;
        public static string remote_address;
        public ImageTransfer(string IP, int lP, int rP)
        {
            localPort = lP;
            remotePort = rP;
            remote_address = IP;
        }

        public static void SendMessage(object tmpCamera)
        {
            Thread.Sleep(100);
            UdpClient sender = new UdpClient();
            //IPEndPoint endPoint = new IPEndPoint(remote_address, remotePort);
            WebCamera Camera = (WebCamera)tmpCamera;
            Image tmpImage;
            byte[] data;
            try
            {
                while (true)
                {
                    lock (Camera.locker)
                    {
                        if (Camera.currentImage != null)
                        {
                            tmpImage = new Bitmap(Camera.currentImage);
                            data = imageToByteArray(tmpImage);
                            sender.Send(data, data.Length, remote_address, remotePort);

                            Camera.currentImage = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public static void ReceiveMessage(object tmpWindow)
        {
            Thread.Sleep(200);
            UdpClient receiver = new UdpClient(localPort);
            //receiver.JoinMulticastGroup(remote_address, 50);
            IPEndPoint remoteIP = null;
            //string localAddress = LocalIpAddress();
            PictureBox Window = (PictureBox)tmpWindow;
            Image image;
            byte[] data;
            try
            {
                while (true)
                {
                    data = receiver.Receive(ref remoteIP);
                    //if (remoteIP.Address.ToString().Equals(localAddress))
                    //    continue;

                    image = byteArrayToImage(data);
                    Window.Image = image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
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

        private static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
