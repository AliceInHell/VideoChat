﻿namespace VideoChat
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedVideo_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receivedVideo_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // setPort_button
            // 
            this.setPort_button.Location = new System.Drawing.Point(1112, 577);
            this.setPort_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setPort_button.Name = "setPort_button";
            this.setPort_button.Size = new System.Drawing.Size(100, 28);
            this.setPort_button.TabIndex = 0;
            this.setPort_button.Text = "SET";
            this.setPort_button.UseVisualStyleBackColor = true;
            this.setPort_button.Click += new System.EventHandler(this.setPort_button_Click);
            // 
            // remotePort_textBox
            // 
            this.remotePort_textBox.Location = new System.Drawing.Point(953, 530);
            this.remotePort_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.remotePort_textBox.Name = "remotePort_textBox";
            this.remotePort_textBox.Size = new System.Drawing.Size(132, 22);
            this.remotePort_textBox.TabIndex = 1;
            // 
            // localPort_textBox
            // 
            this.localPort_textBox.Location = new System.Drawing.Point(953, 577);
            this.localPort_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.localPort_textBox.Name = "localPort_textBox";
            this.localPort_textBox.Size = new System.Drawing.Size(132, 22);
            this.localPort_textBox.TabIndex = 2;
            // 
            // uploadedVideo_pictureBox
            // 
            this.uploadedVideo_pictureBox.Location = new System.Drawing.Point(953, 177);
            this.uploadedVideo_pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uploadedVideo_pictureBox.Name = "uploadedVideo_pictureBox";
            this.uploadedVideo_pictureBox.Size = new System.Drawing.Size(213, 148);
            this.uploadedVideo_pictureBox.TabIndex = 3;
            this.uploadedVideo_pictureBox.TabStop = false;
            // 
            // receivedVideo_pictureBox
            // 
            this.receivedVideo_pictureBox.Location = new System.Drawing.Point(16, 15);
            this.receivedVideo_pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.receivedVideo_pictureBox.Name = "receivedVideo_pictureBox";
            this.receivedVideo_pictureBox.Size = new System.Drawing.Size(853, 591);
            this.receivedVideo_pictureBox.TabIndex = 4;
            this.receivedVideo_pictureBox.TabStop = false;
            // 
            // startDialog_button
            // 
            this.startDialog_button.Location = new System.Drawing.Point(1067, 354);
            this.startDialog_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startDialog_button.Name = "startDialog_button";
            this.startDialog_button.Size = new System.Drawing.Size(100, 28);
            this.startDialog_button.TabIndex = 5;
            this.startDialog_button.Text = "START";
            this.startDialog_button.UseVisualStyleBackColor = true;
            this.startDialog_button.Click += new System.EventHandler(this.startDialog_button_Click);
            // 
            // stopDialog_button
            // 
            this.stopDialog_button.Location = new System.Drawing.Point(1067, 390);
            this.stopDialog_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stopDialog_button.Name = "stopDialog_button";
            this.stopDialog_button.Size = new System.Drawing.Size(100, 28);
            this.stopDialog_button.TabIndex = 6;
            this.stopDialog_button.Text = "STOP";
            this.stopDialog_button.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(953, 488);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 647);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stopDialog_button);
            this.Controls.Add(this.startDialog_button);
            this.Controls.Add(this.receivedVideo_pictureBox);
            this.Controls.Add(this.uploadedVideo_pictureBox);
            this.Controls.Add(this.localPort_textBox);
            this.Controls.Add(this.remotePort_textBox);
            this.Controls.Add(this.setPort_button);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.TextBox textBox1;
    }
}

