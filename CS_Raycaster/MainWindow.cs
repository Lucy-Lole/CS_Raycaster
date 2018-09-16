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

        Raycaster RC = new Raycaster();

        public MainWindow()
        {
            InitializeComponent();

            this.pictureBoxMain.Size = new Size(W_WIDTH, W_HEIGHT);
            this.ClientSize = new Size(W_WIDTH, W_HEIGHT);

            this.KeyPress +=
                new KeyPressEventHandler(MainWindow_KeyPress);

            logicThread = new Thread(RunLoop);
            logicThread.Start();
        }

        public void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W))
            {
                RC.Move(true);
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                RC.Move(false);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                RC.Turn(false);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                RC.Turn(true);
            }
        }

        public void RunLoop()
        {
            double frameTimeDouble = 0;
            while (true)
            {
                this.frameTime.Restart();
                RC.UpdatePositions();
                RC.UpdateFramerate(frameTimeDouble);
                SetImage(RC.NewFrame(W_WIDTH, W_HEIGHT));
                this.frameTime.Stop();
                frameTimeDouble = this.frameTime.ElapsedMilliseconds;
            }

        }

        private void SetImage(Image img)
        {
            pictureBoxMain.Image = img;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.logicThread.Abort();
        }
    }
}
