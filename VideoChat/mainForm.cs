using System;
using System.Threading;
using System.Windows.Forms;

namespace VideoChat
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private Client myClient;

        private void startDialog_button_Click(object sender, EventArgs e)
        {
            if (users_listBox.SelectedIndex != -1)
                myClient.call(users_listBox.SelectedIndex);
        }

        private void disposeCamera()
        {
            if (myClient != null && myClient.isConnected && myClient.myCamera != null)
            {
                myClient.disposeAndClear();
            }
        }

        private void leaveServer()
        {
            if (myClient != null && myClient.isConnected)
                myClient.leaveServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myClient.isCalling)
                myClient.stopDialog();
            disposeCamera();
            leaveServer();
        }

        private void stopDialog_button_Click(object sender, EventArgs e)
        {
            myClient.isCalling = false;
            disposeCamera();
            ImageAndAudioTransfer.stopTranslation = true;
            myClient.stopDialog();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.uploadedVideo_pictureBox.Visible = false;
            this.hidden_Picturebox.Visible = false;
            this.startDialog_button.Visible = false;
            this.stopDialog_button.Visible = false;
            this.receivedVideo_pictureBox.Visible = false;
            this.users_listBox.Visible = false;
        }

        private void hideEntranceCompoments()
        {
            this.uploadedVideo_pictureBox.Visible = true;
            this.hidden_Picturebox.Visible = true;
            this.startDialog_button.Visible = true;
            this.stopDialog_button.Visible = true;
            this.receivedVideo_pictureBox.Visible = true;
            this.users_listBox.Visible = true;

            this.name_textBox.Visible = false;
            this.remoteIP_textBox.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.setInput_button.Visible = false;
        }

        private void setInput_button_Click(object sender, EventArgs e)
        {
            if (name_textBox.Text != "" && remoteIP_textBox.Text != "")
            {
                try
                {
                    myClient = new Client(name_textBox.Text, this.hidden_Picturebox, this.uploadedVideo_pictureBox, this.receivedVideo_pictureBox, users_listBox);
                    myClient.connectToServer(remoteIP_textBox.Text);
                    Thread.Sleep(2000);
                    if (myClient.isConnected)
                    {
                        hideEntranceCompoments();
                        myClient.getUserList();
                    }
                    else
                        MessageBox.Show("Could not connect to server(");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Input fields");
        }
    }
}
