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

namespace CS_Raycaster
{
    public partial class MainWindow : Form
    {
        public const int W_WIDTH = 1200;
        public const int W_HEIGHT = 800;

        private Thread logicThread;

        Stopwatch stopwatch = new Stopwatch();

        Raycaster RC = new Raycaster();

        public MainWindow()
        {
            InitializeComponent();

            this.KeyPress +=
                new KeyPressEventHandler(MainWindow_KeyPress);

            logicThread = new Thread(RunLoop);
            logicThread.Start();
        }

        public void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key Pressed!");
            if (e.KeyChar == 'w')
            {
                RC.Move(true);
            }
            if (e.KeyChar == 's')
            {
                RC.Move(false);
            }
            if (e.KeyChar == 'a')
            {
                RC.TurnLeft();
            }
            if (e.KeyChar == 'd')
            {
                RC.TurnRight();
            }
        }

        public void RunLoop()
        {
            double frameTimeDouble = 0;
            while (true)
            {
                this.stopwatch.Restart();
                RC.UpdateFramerate(frameTimeDouble);
                SetImage(RC.NewFrame(W_WIDTH, W_HEIGHT));
                this.stopwatch.Stop();
                frameTimeDouble = this.stopwatch.ElapsedMilliseconds;
            }

        }

        private void SetImage(Image img)
        {
            pictureBoxMain.Image = img;
        }
    }
}
