using System.Windows.Forms;
using LogSystem;

namespace WindowTest
{
    public partial class logviewer : Form
    {
        public logviewer()
        {
            InitializeComponent();
            Log log = new Log();
            textBox1.Text = log.GetTodayLog();
        }

        private void logviewer_SizeChanged(object sender, System.EventArgs e)
        {
            textBox1.Size = new System.Drawing.Size(
                Width - 13, Height - 31
            );
        }
    }
}
