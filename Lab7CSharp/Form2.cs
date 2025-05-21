using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public class Form2 : Form
    {
        private Bitmap originalImage = null;
        private Bitmap modifiedImage = null;

        private Button btnOpen;
        private Button btnSave;
        private PictureBox pictureBox;
        private GroupBox groupBox;
        private RadioButton rbRed;
        private RadioButton rbGreen;
        private RadioButton rbBlue;

        public Form2()
        {
            
            this.Text = "Завдання 2: Пригнічення кольору BMP";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            
            btnOpen = new Button();
            btnOpen.Text = "Відкрити";
            btnOpen.Location = new Point(10, 10);
            btnOpen.Click += BtnOpen_Click;

            btnSave = new Button();
            btnSave.Text = "Зберегти";
            btnSave.Location = new Point(100, 10);
            btnSave.Click += BtnSave_Click;

            
            pictureBox = new PictureBox();
            pictureBox.Location = new Point(10, 50);
            pictureBox.Size = new Size(400, 300);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            
            groupBox = new GroupBox();
            groupBox.Text = "Вибір складової";
            groupBox.Location = new Point(420, 50);
            groupBox.Size = new Size(150, 120);

            rbRed = new RadioButton();
            rbRed.Text = "Red";
            rbRed.Location = new Point(10, 20);
            rbRed.CheckedChanged += RbColor_CheckedChanged;

            rbGreen = new RadioButton();
            rbGreen.Text = "Green";
            rbGreen.Location = new Point(10, 50);
            rbGreen.CheckedChanged += RbColor_CheckedChanged;

            rbBlue = new RadioButton();
            rbBlue.Text = "Blue";
            rbBlue.Location = new Point(10, 80);
            rbBlue.CheckedChanged += RbColor_CheckedChanged;

            groupBox.Controls.Add(rbRed);
            groupBox.Controls.Add(rbGreen);
            groupBox.Controls.Add(rbBlue);

            
            this.Controls.Add(btnOpen);
            this.Controls.Add(btnSave);
            this.Controls.Add(pictureBox);
            this.Controls.Add(groupBox);
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    originalImage = null;
                }
                if (modifiedImage != null)
                {
                    modifiedImage.Dispose();
                    modifiedImage = null;
                }

                originalImage = new Bitmap(ofd.FileName);
                modifiedImage = (Bitmap)originalImage.Clone();
                pictureBox.Image = modifiedImage;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("Немає зображення для збереження.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap files (*.bmp)|*.bmp";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                modifiedImage.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        private void RbColor_CheckedChanged(object sender, EventArgs e)
        {
            if (originalImage == null)
                return;

            if (modifiedImage != null)
            {
                modifiedImage.Dispose();
                modifiedImage = null;
            }

            modifiedImage = (Bitmap)originalImage.Clone();

            if (rbRed.Checked)
                SuppressColorComponent(modifiedImage, "R");
            else if (rbGreen.Checked)
                SuppressColorComponent(modifiedImage, "G");
            else if (rbBlue.Checked)
                SuppressColorComponent(modifiedImage, "B");

            pictureBox.Image = modifiedImage;
        }

        private void SuppressColorComponent(Bitmap bmp, string component)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    Color newColor;

                    switch (component)
                    {
                        case "R":
                            newColor = Color.FromArgb(c.A, 0, c.G, c.B);
                            break;
                        case "G":
                            newColor = Color.FromArgb(c.A, c.R, 0, c.B);
                            break;
                        case "B":
                            newColor = Color.FromArgb(c.A, c.R, c.G, 0);
                            break;
                        default:
                            newColor = c;
                            break;
                    }

                    bmp.SetPixel(x, y, newColor);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();


            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
