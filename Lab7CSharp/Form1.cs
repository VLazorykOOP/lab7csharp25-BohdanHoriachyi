using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        
        private TextBox textBox1;
        private Button button1;
        private RichTextBox richTextBox1;

        public Form1()
        {
            InitializeComponent();

            
            textBox1 = new TextBox();
            textBox1.Location = new Point(10, 10);
            textBox1.Width = 300;

            button1 = new Button();
            button1.Text = "Додати";
            button1.Location = new Point(320, 10);
            button1.Click += button1_Click;

            richTextBox1 = new RichTextBox();
            richTextBox1.Location = new Point(10, 50);
            richTextBox1.Width = 380;
            richTextBox1.Height = 200;

            
            this.Controls.Add(textBox1);
            this.Controls.Add(button1);
            this.Controls.Add(richTextBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sentence = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(sentence))
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                richTextBox1.AppendText($"[{time}] {sentence}\n");
                textBox1.Clear();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
