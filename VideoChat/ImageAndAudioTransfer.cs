using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using NAudio.Wave;

namespace VideoChat
{
    public class ImageAndAudioTransfer
    {
        public static int localPort, remotePort;
        public static string remote_address;
        public static bool stopTranslation;

        public ImageAndAudioTransfer(string IP, int lP, int rP)
        {
            localPort = lP;
            remotePort = rP;
            remote_address = IP;
            ImageAndAudioTransfer.stopTranslation = false;
        }

        public static WaveInEvent input;
        public static WaveOutEvent output;
        public static BufferedWaveProvider bufferStream;
        public static Thread audioListenerThread;
        

        public static void SendMessage(object tmpCamera)
        {
            Thread.Sleep(100);
            UdpClient sender = new UdpClient();
            WebCamera Camera = (WebCamera)tmpCamera;
            Image tmpImage;
            byte[] data;

            input = new WaveInEvent();
            input.WaveFormat = new WaveFormat(44100, 16, 2);
            input.DataAvailable += Voice_Input;
            output = new WaveOutEvent();
            bufferStream = new BufferedWaveProvider(new WaveFormat(44100, 16, 2));
            output.Init(bufferStream);

            input.StartRecording();
            try
            {
                while (true)
                {
                    lock (Camera.currentImageLocker)
                    {
                        if (!stopTranslation)
                        {
                            if (Camera.currentImage != null)
                            {
                                tmpImage = new Bitmap(Camera.currentImage);
                                data = imageToByteArray(tmpImage);
                                data = Combine(data, 2);
                                sender.Send(data, data.Length, remote_address, remotePort);
                            }
                        }
                        else
                            return;
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

        private static void Voice_Input(object sender, WaveInEventArgs e)
        {
            UdpClient audioSender = new UdpClient();
            try
            {
                byte[] tmp;
                tmp = Combine(e.Buffer, 1);
                audioSender.Send(tmp, tmp.Length, remote_address, remotePort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ReceiveMessage(object tmpWindow)
        {
            Thread.Sleep(1000);
            UdpClient receiver = new UdpClient(localPort);
            IPEndPoint remoteIP = null;
            PictureBox Window = (PictureBox)tmpWindow;
            Image image;
            byte[] data;
            output.Play();

            try
            {
                while (true)
                {
                    if (!stopTranslation)
                    {
                        Thread.Sleep(5);
                        data = receiver.Receive(ref remoteIP);

                        //проверка
                        if (data[data.Length - 1] == 1)
                        {
                            bufferStream.ClearBuffer();
                            Array.Resize(ref data, data.Length - 1);
                            bufferStream.AddSamples(data, 0, data.Length);
                        }
                        else
                        {
                            Array.Resize(ref data, data.Length - 1);
                            image = byteArrayToImage(data);
                            Window.Image = new Bitmap(image);
                        }
                    }
                    else
                        return;
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

        private static byte[] Combine(byte[] a1, byte a2)
        {
            Array.Resize(ref a1, a1.Length + 1);
            a1[a1.Length - 1] = a2;
            return a1;
        }
    }
}
