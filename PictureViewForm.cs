using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iseseisevForm
{
    public partial class PictureViewForm : Form
    {
        PictureBox picture;
        Button elPilt, jargPilt, taustVar, sule, lisaPilt;
        OpenFileDialog openFile;
        List<string> images = new List<string>();
        int pilt = 2;

        public PictureViewForm()
        {
            InitializeComponent();
            Text = "Galery";
            Height = 700;
            Width = 700;

            for (int i = 1; i <= 7; i++)
            {
                images.Add($@"..\..\pildid\image{i}.jpg");
            }

            picture = new PictureBox();
            picture.Size = new Size(500, 500);
            picture.Left = 90;
            picture.Image = Image.FromFile(images[pilt]);
            Controls.Add(picture);

            lisaPilt = new Button();
            lisaPilt.Text = "Lisa pilt";
            lisaPilt.Font = MainForm.font;
            lisaPilt.Top = 550;
            lisaPilt.Left = 90;
            lisaPilt.Click += LisaPilt_Click;
            Controls.Add(lisaPilt);

            elPilt = new Button();
            elPilt.Text = "Eelmine pilt";
            elPilt.Font = MainForm.font;
            elPilt.Top = 550;
            elPilt.Left = 165;
            elPilt.Click += elPilt_Click;
            Controls.Add(elPilt);

            jargPilt = new Button();
            jargPilt.Text = "Jargmine pilt";
            jargPilt.Font = MainForm.font;
            jargPilt.Top = 550;
            jargPilt.Left = 240;
            jargPilt.Width = 85;
            jargPilt.Click += jargPilt_Click;
            Controls.Add(jargPilt);

            taustVar = new Button();
            taustVar.Text = "Asenda tusta värv";
            taustVar.Font = MainForm.font;
            taustVar.Top = 550;
            taustVar.Left = 325;
            taustVar.Width = 75;
            taustVar.Click += TaustVar_Click; 
            Controls.Add(taustVar);

            sule = new Button();
            sule.Text = "Sule";
            sule.Font = MainForm.font;
            sule.Top = 550;
            sule.Left = 400;
            sule.Click += (s, e) => Close();
            Controls.Add(sule);

        }

        private void TaustVar_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();

            color.FullOpen = true;

            if (color.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = color.Color;
            }
        }

        private void LisaPilt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Pildid (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            DialogResult result = openFile.ShowDialog();
            if (DialogResult.OK == result)
            {
                images.Add(openFile.FileName);
                pilt = images.Count - 1;

                UpdateImage();
            }


        }

        private void UpdateImage()
        {
            if (images.Count > 0 && pilt >= 0 && pilt < images.Count)
            {
                picture.Image?.Dispose();

                picture.Image = Image.FromFile(images[pilt]);
            }
        }

        private void jargPilt_Click(object sender, EventArgs e)
        {
            pilt++;

            if (pilt >= images.Count)
            {
                pilt = 0;
            }

            UpdateImage();
        }

        private void elPilt_Click(object sender, EventArgs e)
        {
            pilt--;

            if (pilt < 0)
            {
                pilt = images.Count - 1;
            }

            UpdateImage();
        }
    }
}
