using Microsoft.VisualBasic;
using System.Drawing;
using System.Windows.Forms;

namespace iseseisevForm
{
    public partial class MainForm : Form
    {
        Button PictureView, mathQuiz, matchingGame;

        public static Font font = new Font("Segoe Script", 9F, FontStyle.Bold);
        public MainForm()
        {
            InitializeComponent();
            Height = 500;
            Width = 500;
            BackgroundImage = Image.FromFile(@"..\..\taustaPiltMain.jpg");
            Text = "Three form chooser";
            

            Button mathQuiz = new Button();
            Button PictureView = new Button();
            Button matchingGame = new Button();

            PictureView.Text = "Galery";
            PictureView.Font = font;
            PictureView.Location = new Point(110, 50);
            PictureView.Click += PictureView_Click;
            Controls.Add(PictureView);

            mathQuiz.Text = "Math Quiz";
            mathQuiz.Font = font;
            mathQuiz.Location = new Point(185, 50);
            mathQuiz.Click += MathQuiz_Click;
            Controls.Add(mathQuiz);

            matchingGame.Text = "Matching Game";
            matchingGame.Font = font;
            matchingGame.Width = 125;
            matchingGame.Click += MatchingGame_Click;
            matchingGame.Location = new Point(260, 50);

            Controls.Add(matchingGame);


        }

        private void MatchingGame_Click(object sender, System.EventArgs e)
        {
            MatchingGameForm matchingGameForm = new MatchingGameForm();
            matchingGameForm.Show();
        }

        private void MathQuiz_Click(object sender, System.EventArgs e)
        {
            MathQuizForm mathQuizForm = new MathQuizForm();
            mathQuizForm.Show();
        }

        private void PictureView_Click(object sender, System.EventArgs e)
        {
            PictureViewForm pictureViewForm = new PictureViewForm();
            pictureViewForm.Show();
        }
    }
}
