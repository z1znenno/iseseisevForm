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

namespace iseseisevForm
{
    public partial class PictureViewForm : Form
    {
        PictureBox picture;
        Button elPilt, jargPilt, taustVar, sule, lisaPilt, salvestaKui;
        Button joonistaNupp, joonistusVarvNupp;
        OpenFileDialog openFile;
        List<string> images = new List<string>();
        int pilt = 2;

        // ----- Рисование -----
        bool joonistamine = false;   
        bool hiirAll = false;        
        Point viimanePunkt;           
        Color joonistusVarv = Color.Red; 

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
            picture.MouseDown += Picture_MouseDown;
            picture.MouseMove += Picture_MouseMove;
            picture.MouseUp += Picture_MouseUp;
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

            salvestaKui = new Button();
            salvestaKui.Text = "Salvesta kui...";
            salvestaKui.Font = MainForm.font;
            salvestaKui.Top = 550;
            salvestaKui.Left = 400;
            salvestaKui.Width = 90;
            salvestaKui.Click += SalvestaKui_Click;
            Controls.Add(salvestaKui);

            sule = new Button();
            sule.Text = "Sule";
            sule.Font = MainForm.font;
            sule.Top = 550;
            sule.Left = 495;
            sule.Click += (s, e) => Close();
            Controls.Add(sule);

            joonistaNupp = new Button();
            joonistaNupp.Text = "Joonista";
            joonistaNupp.Font = MainForm.font;
            joonistaNupp.Top = 585;
            joonistaNupp.Left = 90;
            joonistaNupp.Width = 100;
            joonistaNupp.Click += JoonistaNupp_Click;
            Controls.Add(joonistaNupp);

            joonistusVarvNupp = new Button();
            joonistusVarvNupp.Text = "Joonistamise värv";
            joonistusVarvNupp.Font = MainForm.font;
            joonistusVarvNupp.Top = 585;
            joonistusVarvNupp.Left = 195;
            joonistusVarvNupp.Width = 120;
            joonistusVarvNupp.BackColor = joonistusVarv;
            joonistusVarvNupp.Click += JoonistusVarvNupp_Click;
            Controls.Add(joonistusVarvNupp);

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

        private void SalvestaKui_Click(object sender, EventArgs e)
        {
            if (picture.Image == null)
                return;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter =
                "PNG pilt (*.png)|*.png|" +
                "JPEG pilt (*.jpg)|*.jpg|" +
                "Bitmap (*.bmp)|*.bmp|" +
                "GIF pilt (*.gif)|*.gif|" +
                "TIFF pilt (*.tiff)|*.tiff";
            saveFile.Title = "Salvesta pilt kui";
            saveFile.FileName = Path.GetFileNameWithoutExtension(images[pilt]);

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format;
                switch (saveFile.FilterIndex)
                {
                    case 1:
                        format = ImageFormat.Png;
                        break;
                    case 2:
                        format = ImageFormat.Jpeg;
                        break;
                    case 3:
                        format = ImageFormat.Bmp;
                        break;
                    case 4:
                        format = ImageFormat.Gif;
                        break;
                    case 5:
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = ImageFormat.Png;
                        break;
                }

                try
                {
                    using (var copy = new Bitmap(picture.Image))
                    {
                        copy.Save(saveFile.FileName, format);
                    }

                    MessageBox.Show(
                        "Pilt salvestatud: " + saveFile.FileName,
                        "Salvestatud",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Pildi salvestamine ebaõnnestus: " + ex.Message,
                        "Viga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        private void JoonistaNupp_Click(object sender, EventArgs e)
        {
            joonistamine = !joonistamine;

            if (joonistamine)
            {
                joonistaNupp.Text = "Lõpeta joonistamine";

                if (picture.Image != null)
                {
                    Bitmap muudetav = new Bitmap(picture.Image);
                    picture.Image.Dispose();
                    picture.Image = muudetav;
                }
            }
            else
            {
                joonistaNupp.Text = "Joonista";
            }
        }

        private void JoonistusVarvNupp_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.FullOpen = true;
            color.Color = joonistusVarv;

            if (color.ShowDialog() == DialogResult.OK)
            {
                joonistusVarv = color.Color;
                joonistusVarvNupp.BackColor = joonistusVarv;
            }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (!joonistamine || e.Button != MouseButtons.Left)
                return;

            hiirAll = true;
            viimanePunkt = e.Location;

            JoonistaJoon(viimanePunkt, viimanePunkt);
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!joonistamine || !hiirAll)
                return;

            JoonistaJoon(viimanePunkt, e.Location);
            viimanePunkt = e.Location;
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            hiirAll = false;
        }

        private void JoonistaJoon(Point algus, Point lopp)
        {
            if (!(picture.Image is Bitmap bitmap))
                return;

            using (Graphics g = Graphics.FromImage(bitmap))
            using (Pen pliiats = new Pen(joonistusVarv, 3))
            {
                pliiats.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pliiats.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                g.DrawLine(pliiats, algus, lopp);
            }

            picture.Invalidate();
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