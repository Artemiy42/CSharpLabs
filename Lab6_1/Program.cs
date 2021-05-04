using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Lab6_1
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.Run(new MainWindow());
        }        
    }

    class MainWindow : Form
    {
        private Timer tmrAnimation;
        private Image[] images = new Image[4];
        private int currentImage = 0;

        public MainWindow()
        {
            Size = new Size(835, 660);

            ResourceManager resourceManager = new ResourceManager("Lab6_1.Properties.picslst", typeof(Program).Assembly);
            images[0] = (Image) resourceManager.GetObject("pic1");
            images[1] = (Image) resourceManager.GetObject("pic2");
            images[2] = (Image) resourceManager.GetObject("pic3");
            images[3] = (Image) resourceManager.GetObject("pic4");

            tmrAnimation = new Timer();
            tmrAnimation.Enabled = true; // разрешить таймеру тикать
            tmrAnimation.Interval = 500; // каждые полсекунды
            tmrAnimation.Tick += TimerOnTick;
        }

        protected override void OnPaint(PaintEventArgs ea)
        {
            Image image = images[currentImage];
            Graphics grfx = ea.Graphics;
            grfx.DrawImage(image, 10, 10, image.Width, image.Height);
        }
        private void TimerOnTick(object obj, EventArgs ea)
        {
            Invalidate();
            currentImage++;
            
            if (currentImage > 3)
            {
                currentImage = 0;
            }
        }
    }
}
