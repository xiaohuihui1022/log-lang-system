using System;
using System.Windows.Forms;
using LogSystem;
using LangSystem;

namespace WindowTest
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();


        Log log = new Log();
        Lang lang = new Lang();

        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new TextBoxWriter(textBox1));
            textBox1.WordWrap = true;
            log.LogWriteInit();
            log.TimeShowSet(true);
            lang.Init();
            string[] LangType = { "Chinese", "English" };
            lang.SetLangType(LangType);
            comboBox1.Items.AddRange(LangType);
            comboBox1.SelectedIndex = 0;
            langChanged();
        }
        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            log.Info(lang.RLS("main.openwindow"));
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            log.Info(lang.RLS("main.windowmove") + lang.RLS("main.location")
                + $"{Location.X}, {Location.Y}" + "，" + lang.RLS("main.size")
                + $"{Size.Width}, {Size.Height}");
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            log.Info(lang.RLS("main.windowsize") + lang.RLS("main.location")
                + $"{Location.X}, {Location.Y}" + "，" + lang.RLS("main.size")
                + $"{Size.Width}, {Size.Height}");
            textBox1.Size = new System.Drawing.Size(
                Width - 163, Height - 62);
            comboBox1.Location = new System.Drawing.Point(
                Width - 145, 12);
        }

        private void langChanged()
        {
            lang.ChangeCreateLangType(0);
            lang.CreateLangStr("已开启窗口", "main.openwindow");
            lang.CLS("窗体位置移动，", "main.windowmove");
            lang.CLS("窗体大小改变，", "main.windowsize");
            lang.CLS("位置：", "main.location");
            lang.CLS("大小：", "main.size");
            lang.CLS("语言已切换为:", "lang.change");
            lang.ChangeCreateLangType(1);
            lang.CreateLangStr("Opened Window", "main.openwindow");
            lang.CLS("Window's location has been moved，", "main.windowmove");
            lang.CLS("Window's size has been changed，", "main.windowsize");
            lang.CLS("Location:", "main.location");
            lang.CLS("Size:", "main.size");
            lang.CLS("Lang has been changed to:", "lang.change");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lang.ChangeReadLangType(comboBox1.SelectedIndex);
            log.Info(lang.RLS("lang.change") + comboBox1.SelectedItem);
        }
    }
}
