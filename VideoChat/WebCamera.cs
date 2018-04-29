using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace VideoChat
{
    class WebCamera
    {
        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        public Image currentImage;
        public object locker = new object();

        public void startTranslation(PictureBox window)
        {
            CamContainer = new DirectX.Capture.Filters();

            try
            {
                int no_of_cam = CamContainer.VideoInputDevices.Count;

                for (int i = 0; i < no_of_cam; i++)
                {
                    try
                    {
                        // get the video input device 
                        Camera = CamContainer.VideoInputDevices[i];

                        // initialize the Capture using the video input device
                        CaptureInfo = new DirectX.Capture.Capture(Camera, null);

                        // set the input video preview window 
                        CaptureInfo.PreviewWindow = window;

                        //set frame size
                        CaptureInfo.FrameSize = new Size(640, 480);

                        CaptureInfo.RenderPreview();

                        // Capturing complete event handler
                        CaptureInfo.FrameCaptureComplete += new DirectX.Capture.Capture.FrameCapHandler(RefreshImage);

                        // Capture the frame from input device 
                        CaptureInfo.CaptureFrame();

                        // if device found and initialize properly then exit without   
                        // checking rest of input device 
                        break;
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex){ }
        }

        public static void setFrame(object tmpCamera)
        {
            WebCamera Camera = (WebCamera)tmpCamera;
            while (true)
            {
                lock (Camera.locker)
                {                    
                    if(Camera.currentImage == null)
                    {
                        Camera.setCurrentImage();
                        Thread.Sleep(10);
                        //через жопу но работает
                    }                    
                }
            }
        }

        private void RefreshImage(PictureBox frame)
        {
            currentImage = frame.Image;
        }

        public void setCurrentImage()
        {
            CaptureInfo.CaptureFrame();
        }
    }
}
