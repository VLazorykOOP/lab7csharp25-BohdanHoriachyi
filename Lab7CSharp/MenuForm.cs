using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public class MenuForm : Form
    {
        public MenuForm()
        {
            this.Text = "Вибір завдання";
            this.Size = new Size(200, 230);
            this.StartPosition = FormStartPosition.CenterScreen;

            var btnTask1 = new Button { Text = "Завдання 1", Location = new Point(30, 20), Size = new Size(120, 40) };
            var btnTask2 = new Button { Text = "Завдання 2", Location = new Point(30, 80), Size = new Size(120, 40) };
            var btnTask3 = new Button { Text = "Завдання 3", Location = new Point(30, 140), Size = new Size(120, 40) };

            btnTask1.Click += (s, e) =>
            {
                var form1 = new Form1();
                form1.Show();
                this.Hide();
                form1.FormClosed += (s2, e2) => this.Show();
            };

            btnTask2.Click += (s, e) =>
            {
                var form2 = new Form2();
                form2.Show();
                this.Hide();
                form2.FormClosed += (s2, e2) => this.Show();
            };

            btnTask3.Click += (s, e) =>
            {
                var form3 = new Form3();
                form3.Show();
                this.Hide();
                form3.FormClosed += (s2, e2) => this.Show();
            };

            Controls.Add(btnTask1);
            Controls.Add(btnTask2);
            Controls.Add(btnTask3);
        }
    }
}
