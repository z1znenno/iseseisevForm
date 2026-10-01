using System.Drawing;
using System.Windows.Forms;

namespace iseseisevForm
{
    internal static class Theme
    {
        public static readonly Color Bg = Color.FromArgb(15, 23, 42);   
        public static readonly Color Surface = Color.FromArgb(30, 41, 59);   
        public static readonly Color SurfaceHi = Color.FromArgb(51, 65, 85);    
        public static readonly Color Text = Color.FromArgb(241, 245, 249);
        public static readonly Color Muted = Color.FromArgb(148, 163, 184);
        public static readonly Color Accent = Color.FromArgb(99, 102, 241);
        public static readonly Color AccentHover = Color.FromArgb(129, 140, 248);
        public static readonly Color AccentDown = Color.FromArgb(79, 70, 229);
        public static readonly Color Success = Color.FromArgb(34, 197, 94);
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);

        public static Font Ui(float size, FontStyle style)
        {
            return new Font("Segoe UI", size, style);
        }

        public static void StyleButton(Button b, bool primary)
        {
            b.UseVisualStyleBackColor = false;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = primary ? Accent : SurfaceHi;
            b.ForeColor = Text;
            b.FlatAppearance.MouseOverBackColor = primary ? AccentHover : Accent;
            b.FlatAppearance.MouseDownBackColor = AccentDown;
            b.Font = Ui(9.5F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        public static void StyleCombo(ComboBox c)
        {
            c.FlatStyle = FlatStyle.Flat;
            c.BackColor = Bg;
            c.ForeColor = Text;
            c.Font = Ui(10F, FontStyle.Regular);
        }

        public static Color ContrastText(Color c)
        {
            double lum = 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
            return lum > 150 ? Color.Black : Color.White;
        }
    }
}