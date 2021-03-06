﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace VideoChat
{
    public class WebCamera
    {
        private FilterInfoCollection captureDevice;
        private VideoCaptureDevice finalFrame;

        public Image currentImage;
        private PictureBox mainWindow, hiddenWindow;

        public object currentImageLocker = new object();
        public object pictureboxLocker = new object();

        public void startTranslation(PictureBox hiddenWindow, PictureBox mainWindow)
        {
            this.mainWindow = mainWindow;
            this.hiddenWindow = hiddenWindow;
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            finalFrame = new VideoCaptureDevice(captureDevice[0].MonikerString);
            finalFrame.NewFrame += FinalFrame_NewFrame;
            finalFrame.Start();
        }

        public static void setFrame(object tmpCamera)
        {
            WebCamera Camera = (WebCamera)tmpCamera;
            while (true)
            {
                if (!ImageAndAudioTransfer.stopTranslation)
                {
                    lock (Camera.currentImageLocker)
                    {
                        Camera.setCurrentImage();
                    }
                }
                else
                    return;
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            lock(this.pictureboxLocker)
            {
                Bitmap tmp = (Bitmap)eventArgs.Frame.Clone();
                hiddenWindow.Image = new Bitmap(tmp, new Size(640, 480));
                mainWindow.Image = new Bitmap(tmp, new Size(160, 120));
            }
        }

        public void setCurrentImage()
        {
            lock(this.pictureboxLocker)
            {
                if (hiddenWindow.Image != null)
                    currentImage = new Bitmap((Bitmap)hiddenWindow.Image.Clone(), new Size(640, 480));
            }
        }

        public void stopTranslation()
        {
            if (finalFrame != null && finalFrame.IsRunning)
            {
                finalFrame.Stop();
            }
        }
    }
}
