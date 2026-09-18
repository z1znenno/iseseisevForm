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
        Button elPilt, jargPilt, taustCol, sule, lisaPilt;
        OpenFileDialog openFile;
        List<string> images = new List<string>();
        int pilt = 0;

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

            Button lisaPilt = new Button();
            lisaPilt.Text = "Lisa pilt";
            lisaPilt.Font = MainForm.font;
            lisaPilt.Top = 550;
            lisaPilt.Left = 90;
            Controls.Add(lisaPilt);

            Button elPilt = new Button();
            elPilt.Text = "Eelmine pilt";
            elPilt.Font = MainForm.font;
            elPilt.Top = 550;
            elPilt.Left = 165;
            elPilt.Click += (s, e) => 
            {
                try
                {
                    pilt -= 1;
                }
                catch
                {
                    
                }
            };
            Controls.Add(elPilt);

            Button jargPilt = new Button();
            jargPilt.Text = "Jargmine pilt";
            jargPilt.Font = MainForm.font;
            jargPilt.Top = 550;
            jargPilt.Left = 240;
            jargPilt.Click += (s, e) =>
            {
                try
                {
                    pilt -= 1;
                }
                catch
                {

                }
            };
            Controls.Add(jargPilt);
        }
    }
}
