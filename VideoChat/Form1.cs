using System;
using System.Threading;
using System.Windows.Forms;

namespace VideoChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private WebCamera newCamera = new WebCamera();
        private ImageTransfer NewTransfer;

        private void startDialog_button_Click(object sender, EventArgs e)
        {
            newCamera.startTranslation(uploadedVideo_pictureBox);

            Thread frameThread = new Thread(new ParameterizedThreadStart(WebCamera.setFrame));
            frameThread.Start(newCamera);

            Thread sendThread = new Thread(new ParameterizedThreadStart(ImageTransfer.SendMessage));
            sendThread.Start(newCamera);

            Thread receiveThread = new Thread(new ParameterizedThreadStart(ImageTransfer.ReceiveMessage));
            receiveThread.Start(receivedVideo_pictureBox);
        }

        private void setPort_button_Click(object sender, EventArgs e)
        {
            NewTransfer = new ImageTransfer(textBox1.Text, Convert.ToInt32(localPort_textBox.Text), Convert.ToInt32(remotePort_textBox.Text));
        }
    }
}
