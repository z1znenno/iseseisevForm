using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace iseseisevForm
{
    public partial class MatchingGameForm : Form
    {
        private class CardData
        {
            public Image Piece;
            public string Symbol;
        }

        private readonly TableLayoutPanel tableLayoutPanel1;
        private readonly Timer timer1;
        private readonly Random random = new Random();

        private Label firstClicked = null;
        private Label secondClicked = null;

        private readonly string[] allIcons = new[]
        {
            "!","N",",","k","b","v","w","z","a","s","d","f","g","h","i","j","l","m","o","p","q","r"
        };

        private ComboBox difficultyCombo;
        private Button valiPiltNupp;

        private Image selectedImage = Image.FromFile(@"..\..\matchBG.jpg");

        public MatchingGameForm()
        {
            Text = "Sobitamise mäng";
            StartPosition = FormStartPosition.CenterScreen;

            timer1 = new Timer { Interval = 750 };
            timer1.Tick += Timer1_Tick;

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            var diffLabel = new Label
            {
                Text = "Raskus:",
                AutoSize = true,
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            difficultyCombo = new ComboBox
            {
                Location = new Point(70, 6),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            difficultyCombo.Items.AddRange(new object[] { "Lihtne", "Keskmine", "Raske" });
            difficultyCombo.SelectedIndex = 1;

            valiPiltNupp = new Button
            {
                Text = "Vali pilt",
                Location = new Point(200, 4),
                AutoSize = true
            };
            valiPiltNupp.Click += ValiPiltNupp_Click;

            topPanel.Controls.Add(diffLabel);
            topPanel.Controls.Add(difficultyCombo);
            topPanel.Controls.Add(valiPiltNupp);

            tableLayoutPanel1 = new TableLayoutPanel
            {
                BackColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            };

            Controls.Add(tableLayoutPanel1);
            Controls.Add(topPanel);

            StartGame();

            difficultyCombo.SelectedIndexChanged += DifficultyCombo_SelectedIndexChanged;

            FormClosed += (s, e) => selectedImage?.Dispose();
        }

        private void ChooseImage()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Pildid (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Vali pilt kaartide tagakülje jaoks";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Image uus = Image.FromFile(ofd.FileName);
                    selectedImage?.Dispose();
                    selectedImage = uus;
                }
            }
        }

        private void ValiPiltNupp_Click(object sender, EventArgs e)
        {
            ChooseImage();
            StartGame();
        }

        private void DifficultyCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide();

            StartGame();

            CenterToScreen();
            Show();
            Activate();
        }

        private void StartGame()
        {
            timer1.Stop();
            firstClicked = null;
            secondClicked = null;

            int rows, cols;
            switch (difficultyCombo.SelectedItem as string)
            {
                case "Lihtne":
                    rows = 2; cols = 4;
                    break;
                case "Raske":
                    rows = 6; cols = 6;
                    break;
                default:
                    rows = 4; cols = 4;
                    break;
            }

            BuildGrid(rows, cols);
            AssignIconsToSquares(rows, cols);
        }

        private void BuildGrid(int rows, int cols)
        {
            SuspendLayout();

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label oldLabel && oldLabel.Tag is CardData oldData && oldData.Piece != null)
                {
                    oldData.Piece.Dispose();
                }
            }

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnCount = cols;
            tableLayoutPanel1.RowCount = rows;

            for (int i = 0; i < cols; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / cols));
            for (int i = 0; i < rows; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));

            int topPanelHeight = 40;
            int cellSize = 90;
            int desiredClientWidth = Math.Max(300, cols * cellSize);
            int desiredClientHeight = Math.Max(350, topPanelHeight + rows * cellSize);
            ClientSize = new Size(desiredClientWidth, desiredClientHeight);

            float fontSize;
            if (rows <= 2) fontSize = 72F;
            else if (rows <= 4) fontSize = 48F;
            else fontSize = 32F;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image piece = selectedImage != null
                        ? GetImagePiece(selectedImage, row, col, rows, cols)
                        : null;

                    var label = new Label
                    {
                        BackColor = Color.CornflowerBlue,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", fontSize, FontStyle.Bold),
                        UseCompatibleTextRendering = true,
                        Text = "",
                        BackgroundImage = piece,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        ForeColor = Color.Black,
                        Tag = new CardData { Piece = piece, Symbol = null }
                    };
                    label.Click += Label_Click;
                    tableLayoutPanel1.Controls.Add(label, col, row);
                }
            }

            ResumeLayout(true);
        }

        private Bitmap GetImagePiece(Image source, int row, int col, int rows, int cols)
        {
            int pieceWidth = Math.Max(1, source.Width / cols);
            int pieceHeight = Math.Max(1, source.Height / rows);

            Rectangle srcRect = new Rectangle(col * pieceWidth, row * pieceHeight, pieceWidth, pieceHeight);
            Bitmap piece = new Bitmap(pieceWidth, pieceHeight);

            using (Graphics g = Graphics.FromImage(piece))
            {
                g.DrawImage(source, new Rectangle(0, 0, pieceWidth, pieceHeight), srcRect, GraphicsUnit.Pixel);
            }

            return piece;
        }

        private void AssignIconsToSquares(int rows, int cols)
        {
            int pairs = rows * cols / 2;

            var icons = new List<string>();
            for (int i = 0; i < pairs; i++)
            {
                string symbol = allIcons[i % allIcons.Length];
                icons.Add(symbol);
                icons.Add(symbol);
            }

            for (int i = icons.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var tmp = icons[i];
                icons[i] = icons[j];
                icons[j] = tmp;
            }

            int index = 0;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label iconLabel && iconLabel.Tag is CardData data)
                {
                    data.Symbol = icons[index++];
                    iconLabel.Text = "";
                }
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            if (sender is Label clickedLabel)
            {
                if (!string.IsNullOrEmpty(clickedLabel.Text))
                    return;

                OpenCard(clickedLabel);

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    return;
                }

                secondClicked = clickedLabel;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }
        private void OpenCard(Label label)
        {
            if (!(label.Tag is CardData data))
                return;

            label.BackgroundImage = null;
            label.BackColor = Color.Gray;
            label.Text = data.Symbol;
        }

        private void CloseCard(Label label)
        {
            if (!(label.Tag is CardData data))
                return;

            label.BackgroundImage = data.Piece;
            label.BackColor = Color.CornflowerBlue;
            label.Text = "";
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            CloseCard(firstClicked);
            CloseCard(secondClicked);

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label iconLabel && string.IsNullOrEmpty(iconLabel.Text))
                    return;
            }

            MessageBox.Show("Sa sobitasid kõik ikoonid!", "Õnnitleme");
            Close();
        }
    }
}