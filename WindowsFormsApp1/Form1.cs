using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int count_timer = 0;
        int unit_timer = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.Enabled = false;
            button2.BackColor = Color.White;
            button2.Enabled = false;
            textBox1.Text = Environment.CurrentDirectory.ToString();
            textBox2.Text = "숫자 입력";
            this.ActiveControl = textBox2;
        }

        public static void Copy(string outputFilename)
        {

            Rectangle rect = Screen.PrimaryScreen.Bounds;

            int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if(bitsPerPixel<=16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if(bitsPerPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            using(Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            bmp.Save(outputFilename);
            bmp.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string DirPath = Environment.CurrentDirectory + @"\capture";
            DirectoryInfo di = new DirectoryInfo(DirPath);
            if (!di.Exists) Directory.CreateDirectory(DirPath);
            button1.BackColor = Color.Gold;
            button2.BackColor = Color.White;
            timer1_Tick(sender, e);
            timer1.Interval = count_timer*unit_timer;
            timer1.Start();            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Copy(Environment.CurrentDirectory +@"\capture\"+ DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button2.BackColor = Color.Red;
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int check = 0;
            if(int.TryParse(textBox2.Text,out check) && unit_timer !=0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                count_timer = int.Parse(textBox2.Text);
            }
           
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==0) //sec
            {
                unit_timer = 1*1000;
            }
            else if(comboBox1.SelectedIndex==1) //min
            {
                unit_timer = 60*1000;
            }
            if(comboBox1.SelectedIndex ==2) //hour
            {
                unit_timer = 3600*1000;
            }
        }
    }
}
