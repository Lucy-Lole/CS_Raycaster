using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Input;

namespace CS_Raycaster
{
    public partial class MainWindow : Form
    {
        private const int W_WIDTH = 1200;
        private const int W_HEIGHT = 800;

        private Thread logicThread;

        private Stopwatch frameTime = new Stopwatch();

        private Raycaster RC = new Raycaster();

        private bool forward = false, back = false, left = false, right = false;

        private double frameRate = 30;

        public MainWindow()
        {
            InitializeComponent();

            this.pictureBoxMain.Size = new Size(W_WIDTH, W_HEIGHT);
            this.ClientSize = new Size(W_WIDTH, W_HEIGHT);

            FPSTimer.Start();

            logicThread = new Thread(RunLoop);
            logicThread.Start();
        }

        public void RunLoop()
        {
            double frameTimeDouble = 1000;
            while (true)
            {
                this.frameTime.Restart();
                this.MovePlayer();
                RC.UpdateFramerate(frameTimeDouble);
                SetImage(RC.NewFrame(W_WIDTH, W_HEIGHT));
                this.frameTime.Stop();
                frameRate = (1 / (frameTimeDouble/1000));
                frameTimeDouble = this.frameTime.ElapsedMilliseconds;
            }
        }

        private void SetImage(Image img)
        {
            this.pictureBoxMain.Image = img;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.logicThread.Abort();
        }

        private void FPSTimer_Tick(object sender, EventArgs e)
        {
            this.FPS.Text = this.frameRate.ToString("###");
            this.PlayerX.Text = this.RC.PlayerPosition.x.ToString("##.##");
            this.PlayerY.Text = this.RC.PlayerPosition.y.ToString("##.##");
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W))
            {
                this.forward = true;
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                this.back = true;
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                this.left = true;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                this.right = true;
            }
        }

        private void MainWindow_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.W))
            {
                this.forward = false;
            }

            if (!Keyboard.IsKeyDown(Key.S))
            {
                this.back = false;
            }
            if (!Keyboard.IsKeyDown(Key.A))
            {
                this.left = false;
            }
            if (!Keyboard.IsKeyDown(Key.D))
            {
                this.right = false;
            }
        }

        private void MovePlayer()
        {
            if (this.forward)
            {
                RC.Move(true);
            }

            if (this.back)
            {
                RC.Move(false);
            }

            if (this.left)
            {
                RC.Turn(false);
            }

            if (this.right)
            {
                RC.Turn(true);
            }
        }
    }
}
