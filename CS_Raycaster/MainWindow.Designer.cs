﻿namespace CS_Raycaster
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.FPS = new System.Windows.Forms.Label();
            this.FPSTimer = new System.Windows.Forms.Timer(this.components);
            this.PlayerX = new System.Windows.Forms.Label();
            this.PlayerY = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            // 
            // FPS
            // 
            this.FPS.AutoSize = true;
            this.FPS.BackColor = System.Drawing.Color.Transparent;
            this.FPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FPS.ForeColor = System.Drawing.Color.Lime;
            this.FPS.Location = new System.Drawing.Point(-3, 0);
            this.FPS.Name = "FPS";
            this.FPS.Size = new System.Drawing.Size(24, 17);
            this.FPS.TabIndex = 1;
            this.FPS.Text = "30";
            // 
            // FPSTimer
            // 
            this.FPSTimer.Tick += new System.EventHandler(this.FPSTimer_Tick);
            // 
            // PlayerX
            // 
            this.PlayerX.AutoSize = true;
            this.PlayerX.BackColor = System.Drawing.Color.Transparent;
            this.PlayerX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerX.ForeColor = System.Drawing.Color.Lime;
            this.PlayerX.Location = new System.Drawing.Point(-3, 17);
            this.PlayerX.Name = "PlayerX";
            this.PlayerX.Size = new System.Drawing.Size(24, 17);
            this.PlayerX.TabIndex = 2;
            this.PlayerX.Text = "12";
            // 
            // PlayerY
            // 
            this.PlayerY.AutoSize = true;
            this.PlayerY.BackColor = System.Drawing.Color.Transparent;
            this.PlayerY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerY.ForeColor = System.Drawing.Color.Lime;
            this.PlayerY.Location = new System.Drawing.Point(-3, 34);
            this.PlayerY.Name = "PlayerY";
            this.PlayerY.Size = new System.Drawing.Size(24, 17);
            this.PlayerY.TabIndex = 3;
            this.PlayerY.Text = "12";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.PlayerY);
            this.Controls.Add(this.PlayerX);
            this.Controls.Add(this.FPS);
            this.Controls.Add(this.pictureBoxMain);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Label FPS;
        private System.Windows.Forms.Timer FPSTimer;
        private System.Windows.Forms.Label PlayerX;
        private System.Windows.Forms.Label PlayerY;
    }
}

