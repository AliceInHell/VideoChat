namespace VideoChat
{
    partial class mainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.setInput_button = new System.Windows.Forms.Button();
            this.uploadedVideo_pictureBox = new System.Windows.Forms.PictureBox();
            this.receivedVideo_pictureBox = new System.Windows.Forms.PictureBox();
            this.startDialog_button = new System.Windows.Forms.Button();
            this.stopDialog_button = new System.Windows.Forms.Button();
            this.name_textBox = new System.Windows.Forms.TextBox();
            this.hidden_Picturebox = new System.Windows.Forms.PictureBox();
            this.remoteIP_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.users_listBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedVideo_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receivedVideo_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hidden_Picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // setInput_button
            // 
            this.setInput_button.Location = new System.Drawing.Point(427, 323);
            this.setInput_button.Name = "setInput_button";
            this.setInput_button.Size = new System.Drawing.Size(75, 23);
            this.setInput_button.TabIndex = 0;
            this.setInput_button.Text = "SET";
            this.setInput_button.UseVisualStyleBackColor = true;
            this.setInput_button.Click += new System.EventHandler(this.setInput_button_Click);
            // 
            // uploadedVideo_pictureBox
            // 
            this.uploadedVideo_pictureBox.Location = new System.Drawing.Point(732, 18);
            this.uploadedVideo_pictureBox.Name = "uploadedVideo_pictureBox";
            this.uploadedVideo_pictureBox.Size = new System.Drawing.Size(160, 120);
            this.uploadedVideo_pictureBox.TabIndex = 3;
            this.uploadedVideo_pictureBox.TabStop = false;
            // 
            // receivedVideo_pictureBox
            // 
            this.receivedVideo_pictureBox.Location = new System.Drawing.Point(12, 12);
            this.receivedVideo_pictureBox.Name = "receivedVideo_pictureBox";
            this.receivedVideo_pictureBox.Size = new System.Drawing.Size(640, 480);
            this.receivedVideo_pictureBox.TabIndex = 4;
            this.receivedVideo_pictureBox.TabStop = false;
            // 
            // startDialog_button
            // 
            this.startDialog_button.Location = new System.Drawing.Point(817, 159);
            this.startDialog_button.Name = "startDialog_button";
            this.startDialog_button.Size = new System.Drawing.Size(75, 23);
            this.startDialog_button.TabIndex = 5;
            this.startDialog_button.Text = "CALL";
            this.startDialog_button.UseVisualStyleBackColor = true;
            this.startDialog_button.Click += new System.EventHandler(this.startDialog_button_Click);
            // 
            // stopDialog_button
            // 
            this.stopDialog_button.Location = new System.Drawing.Point(817, 188);
            this.stopDialog_button.Name = "stopDialog_button";
            this.stopDialog_button.Size = new System.Drawing.Size(75, 23);
            this.stopDialog_button.TabIndex = 6;
            this.stopDialog_button.Text = "STOP";
            this.stopDialog_button.UseVisualStyleBackColor = true;
            this.stopDialog_button.Click += new System.EventHandler(this.stopDialog_button_Click);
            // 
            // name_textBox
            // 
            this.name_textBox.Location = new System.Drawing.Point(412, 231);
            this.name_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(100, 20);
            this.name_textBox.TabIndex = 7;
            // 
            // hidden_Picturebox
            // 
            this.hidden_Picturebox.Location = new System.Drawing.Point(678, 18);
            this.hidden_Picturebox.Name = "hidden_Picturebox";
            this.hidden_Picturebox.Size = new System.Drawing.Size(0, 0);
            this.hidden_Picturebox.TabIndex = 8;
            this.hidden_Picturebox.TabStop = false;
            // 
            // remoteIP_textBox
            // 
            this.remoteIP_textBox.Location = new System.Drawing.Point(412, 285);
            this.remoteIP_textBox.Name = "remoteIP_textBox";
            this.remoteIP_textBox.Size = new System.Drawing.Size(100, 20);
            this.remoteIP_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(409, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "input NAME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "input REMOTE_IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(380, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 46);
            this.label3.TabIndex = 11;
            this.label3.Text = "HELLO!";
            // 
            // users_listBox
            // 
            this.users_listBox.FormattingEnabled = true;
            this.users_listBox.Location = new System.Drawing.Point(732, 260);
            this.users_listBox.Name = "users_listBox";
            this.users_listBox.Size = new System.Drawing.Size(160, 199);
            this.users_listBox.TabIndex = 12;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 526);
            this.Controls.Add(this.users_listBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hidden_Picturebox);
            this.Controls.Add(this.name_textBox);
            this.Controls.Add(this.stopDialog_button);
            this.Controls.Add(this.startDialog_button);
            this.Controls.Add(this.receivedVideo_pictureBox);
            this.Controls.Add(this.uploadedVideo_pictureBox);
            this.Controls.Add(this.remoteIP_textBox);
            this.Controls.Add(this.setInput_button);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uploadedVideo_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receivedVideo_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hidden_Picturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setInput_button;
        private System.Windows.Forms.PictureBox uploadedVideo_pictureBox;
        private System.Windows.Forms.PictureBox receivedVideo_pictureBox;
        private System.Windows.Forms.Button startDialog_button;
        private System.Windows.Forms.Button stopDialog_button;
        private System.Windows.Forms.TextBox name_textBox;
        private System.Windows.Forms.PictureBox hidden_Picturebox;
        private System.Windows.Forms.TextBox remoteIP_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox users_listBox;
    }
}

