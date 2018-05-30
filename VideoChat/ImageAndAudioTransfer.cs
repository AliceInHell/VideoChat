using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using NAudio.Wave;
using NAudio.CoreAudioApi;

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
            stopTranslation = false;
        }

        //for audio
        public static WaveIn input;
        //поток для речи собеседника
        public static WaveOut output;
        //буфферный поток для передачи через сеть
        public static BufferedWaveProvider bufferStream;
        //поток для прослушивания входящих сообщений
        public static Thread audioListenerThread;

        //глобальный клиент отпрвитель
        public static UdpClient sender = new UdpClient();

        public static void SendMessage(object tmpCamera)
        {
            Thread.Sleep(100);
            
            WebCamera Camera = (WebCamera)tmpCamera;
            Image tmpImage;
            byte[] data;

            /*input = new WaveIn();
            //определяем его формат - частота дискретизации 8000 Гц, ширина сэмпла - 16 бит, 1 канал - моно
            input.WaveFormat = new WaveFormat(8000, 16, 1);
            //добавляем код обработки нашего голоса, поступающего на микрофон
            input.DataAvailable += Voice_Input;
            //создаем поток для прослушивания входящего звука
            output = new WaveOut();
            //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
            bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
            //привязываем поток входящего звука к буферному потоку
            output.Init(bufferStream);

            input.StartRecording();*/
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
            try
            {
                ImageAndAudioTransfer.sender.Send(e.Buffer, e.Buffer.Length, remote_address, remotePort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            //output.Play();
            try
            {
                while (true)
                {
                    if (!stopTranslation)
                    {
                        Thread.Sleep(5);
                        data = receiver.Receive(ref remoteIP);
                        //проверка
                        image = byteArrayToImage(data);
                        Window.Image = new Bitmap(image);

                        //bufferStream.AddSamples(data, 0, data.Length);
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

    }
}
