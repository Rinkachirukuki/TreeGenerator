using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeGenerator
{
    public partial class Form1 : Form
    {
        private Graphics main_graphics;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            UpdatePicBoxParams();
        }
        private void UpdatePicBoxParams()
        {

            pictureBox1.Image = new Bitmap(pictureBox1.Width > 0 ? pictureBox1.Width : 1, pictureBox1.Height > 0 ? pictureBox1.Height : 1);
            main_graphics = Graphics.FromImage(pictureBox1.Image);
            main_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UpdatePicBox();
        }
        private void UpdatePicBox(bool refresh = true)
        {
            main_graphics.Clear(Color.White);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void CreateTree(int n, int n2, float width, Pen pen, Color start_color, Color end_color, Graphics gr, float x, float y, double angle, double angle2, int length, int newUpLineChance, int newRightLineChance, int newLeftLineChance)
        {
            if (n == 0) return;

            pen.Color = Color.FromArgb(
                (int)Math.Abs((255 - 155f / n2)),
                (int)(Math.Abs(end_color.R - Math.Abs((float)(end_color.R - start_color.R)) / n2 * n)),
                (int)(Math.Abs(end_color.G - Math.Abs((float)(end_color.G - start_color.G)) / n2 * n)),
                (int)(Math.Abs(end_color.B - Math.Abs((float)(end_color.B - start_color.B)) / n2 * n))
                );

            pen.Width = Math.Abs(1 - Math.Abs(1 - width) / n2 * n);

            //pen.Color = Color.FromArgb(255, (int)(Math.Sqrt(((Math.Pow(start_color.R,2) * (1 - 1/n) + Math.Pow(end_color.R, 2) * (1 / n)) / 2))), (int)(Math.Sqrt(((Math.Pow(start_color.G, 2) * (1 - 1 / n) + Math.Pow(end_color.G, 2)* (1 / n)) / 2))), (int)(Math.Sqrt(((Math.Pow(start_color.B, 2) * (1 - 1 / n) + Math.Pow(end_color.B, 2) * (1 / n)) / 2))));


            gr.DrawLine(pen, x, y,
                (float)((x) * Math.Cos(angle / 180 * Math.PI) - (y - length) * Math.Sin(angle / 180 * Math.PI) - x * Math.Cos(angle / 180 * Math.PI) + y * Math.Sin(angle / 180 * Math.PI) + x),
                (float)((x) * Math.Sin(angle / 180 * Math.PI) + (y - length) * Math.Cos(angle / 180 * Math.PI) - x * Math.Sin(angle / 180 * Math.PI) - y * Math.Cos(angle / 180 * Math.PI) + y));


            

            if (rnd.Next(0, 100) <= newUpLineChance)
                CreateTree(n - 1, n2,width, pen, start_color, end_color, gr,
                (float)((x) * Math.Cos(angle / 180 * Math.PI) - (y - length) * Math.Sin(angle / 180 * Math.PI) - x * Math.Cos(angle / 180 * Math.PI) + y * Math.Sin(angle / 180 * Math.PI) + x),
                (float)((x) * Math.Sin(angle / 180 * Math.PI) + (y - length) * Math.Cos(angle / 180 * Math.PI) - x * Math.Sin(angle / 180 * Math.PI) - y * Math.Cos(angle / 180 * Math.PI) + y),
                angle + rnd.Next(-2, 2), angle2, length - 4 - rnd.Next(0, 2), newUpLineChance, newRightLineChance, newLeftLineChance);

            if (rnd.Next(0, 100) <= newRightLineChance)
                CreateTree(n - 1,n2,width, pen, start_color, end_color, gr,
                (float)((x) * Math.Cos(angle / 180 * Math.PI) - (y - length) * Math.Sin(angle / 180 * Math.PI) - x * Math.Cos(angle / 180 * Math.PI) + y * Math.Sin(angle / 180 * Math.PI) + x),
                (float)((x) * Math.Sin(angle / 180 * Math.PI) + (y - length) * Math.Cos(angle / 180 * Math.PI) - x * Math.Sin(angle / 180 * Math.PI) - y * Math.Cos(angle / 180 * Math.PI) + y),
                angle + angle2 - rnd.Next(0, 5), angle2, length - 4 - rnd.Next(0, 2), newUpLineChance, newRightLineChance, newLeftLineChance);

            if (rnd.Next(0, 100) <= newLeftLineChance)
                CreateTree(n - 1,n2,width, pen, start_color, end_color, gr,
                (float)((x) * Math.Cos(angle / 180 * Math.PI) - (y - length) * Math.Sin(angle / 180 * Math.PI) - x * Math.Cos(angle / 180 * Math.PI) + y * Math.Sin(angle / 180 * Math.PI) + x),
                (float)((x) * Math.Sin(angle / 180 * Math.PI) + (y - length) * Math.Cos(angle / 180 * Math.PI) - x * Math.Sin(angle / 180 * Math.PI) - y * Math.Cos(angle / 180 * Math.PI) + y),
                angle - angle2 + rnd.Next(0, 5), angle2, length - 4 - rnd.Next(0, 2), newUpLineChance, newRightLineChance, newLeftLineChance);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            UpdatePicBox(false);
            CreateTree(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox1.Text), 10, new Pen(Color.Brown),Color.Brown,Color.Teal, main_graphics, pictureBox1.Width / 2, pictureBox1.Height, 0, Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text));
            pictureBox1.Refresh();

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            UpdatePicBoxParams();
        }
    }
}
