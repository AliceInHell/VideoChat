namespace VideoChat
{
    partial class Form1
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
            this.setPort_button = new System.Windows.Forms.Button();
            this.remotePort_textBox = new System.Windows.Forms.TextBox();
            this.localPort_textBox = new System.Windows.Forms.TextBox();
            this.uploadedVideo_pictureBox = new System.Windows.Forms.PictureBox();
            this.receivedVideo_pictureBox = new System.Windows.Forms.PictureBox();
            this.startDialog_button = new System.Windows.Forms.Button();
            this.stopDialog_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedVideo_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receivedVideo_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // setPort_button
            // 
            this.setPort_button.Location = new System.Drawing.Point(834, 469);
            this.setPort_button.Name = "setPort_button";
            this.setPort_button.Size = new System.Drawing.Size(75, 23);
            this.setPort_button.TabIndex = 0;
            this.setPort_button.Text = "SET";
            this.setPort_button.UseVisualStyleBackColor = true;
            this.setPort_button.Click += new System.EventHandler(this.setPort_button_Click);
            // 
            // remotePort_textBox
            // 
            this.remotePort_textBox.Location = new System.Drawing.Point(715, 431);
            this.remotePort_textBox.Name = "remotePort_textBox";
            this.remotePort_textBox.Size = new System.Drawing.Size(100, 20);
            this.remotePort_textBox.TabIndex = 1;
            // 
            // localPort_textBox
            // 
            this.localPort_textBox.Location = new System.Drawing.Point(715, 469);
            this.localPort_textBox.Name = "localPort_textBox";
            this.localPort_textBox.Size = new System.Drawing.Size(100, 20);
            this.localPort_textBox.TabIndex = 2;
            // 
            // uploadedVideo_pictureBox
            // 
            this.uploadedVideo_pictureBox.Location = new System.Drawing.Point(715, 144);
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
            this.startDialog_button.Location = new System.Drawing.Point(800, 288);
            this.startDialog_button.Name = "startDialog_button";
            this.startDialog_button.Size = new System.Drawing.Size(75, 23);
            this.startDialog_button.TabIndex = 5;
            this.startDialog_button.Text = "START";
            this.startDialog_button.UseVisualStyleBackColor = true;
            this.startDialog_button.Click += new System.EventHandler(this.startDialog_button_Click);
            // 
            // stopDialog_button
            // 
            this.stopDialog_button.Location = new System.Drawing.Point(800, 317);
            this.stopDialog_button.Name = "stopDialog_button";
            this.stopDialog_button.Size = new System.Drawing.Size(75, 23);
            this.stopDialog_button.TabIndex = 6;
            this.stopDialog_button.Text = "STOP";
            this.stopDialog_button.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 526);
            this.Controls.Add(this.stopDialog_button);
            this.Controls.Add(this.startDialog_button);
            this.Controls.Add(this.receivedVideo_pictureBox);
            this.Controls.Add(this.uploadedVideo_pictureBox);
            this.Controls.Add(this.localPort_textBox);
            this.Controls.Add(this.remotePort_textBox);
            this.Controls.Add(this.setPort_button);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.uploadedVideo_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receivedVideo_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setPort_button;
        private System.Windows.Forms.TextBox remotePort_textBox;
        private System.Windows.Forms.TextBox localPort_textBox;
        private System.Windows.Forms.PictureBox uploadedVideo_pictureBox;
        private System.Windows.Forms.PictureBox receivedVideo_pictureBox;
        private System.Windows.Forms.Button startDialog_button;
        private System.Windows.Forms.Button stopDialog_button;
    }
}

