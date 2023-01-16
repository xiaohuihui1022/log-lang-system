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
            bool result = lang.Init();
            // 如果已经init了，就读取里面的langtype
            if (!result)
            {
                comboBox1.Items.AddRange(lang.LangTypeText);
            }
            // 否则，新建一个langtype
            else
            {
                string[] LangType = { "Chinese", "English" };
                lang.SetLangType(LangType);
                comboBox1.Items.AddRange(LangType);
            }

            comboBox1.SelectedIndex = lang.NowTypeIndex;
            langChanged();
        }
        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.log.Info(lang.RLS("form", "main.openwindow"));
            this.button1.Text = lang.RLS("button", "main.b1");
            this.button2.Text = lang.RLS("button", "main.b2");
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            log.Info(lang.RLS("form", "main.windowmove") + lang.RLS("form", "main.location")
                + $"{Location.X}, {Location.Y}" + "，" + lang.RLS("form", "main.size")
                + $"{Size.Width}, {Size.Height}");
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            log.Info(lang.RLS("form", "main.windowsize") + lang.RLS("form", "main.location")
                + $"{Location.X}, {Location.Y}" + "，" + lang.RLS("form", "main.size")
                + $"{Size.Width}, {Size.Height}");
            textBox1.Size = new System.Drawing.Size(
                Width - 163, Height - 62);
            comboBox1.Location = new System.Drawing.Point(
                Width - 145, 12);
        }

        private void langChanged()
        {
            lang.ChangeCreateLangType(0);
            lang.CreateLangStr("已开启窗口", "form","main.openwindow");
            lang.CLS("窗体位置移动，", "form","main.windowmove");
            lang.CLS("窗体大小改变，", "form","main.windowsize");
            lang.CLS("位置：", "form","main.location");
            lang.CLS("大小：", "form","main.size");
            lang.CLS("语言已切换为:", "form","lang.change");
            lang.CLS("查看日志", "button","main.b1");
            lang.CLS("日志预览窗口", "button","main.b2");
            lang.CLS("日志查看窗口已打开", "form","main.bopen");
            lang.CLS("日志预览窗口已打开", "form","main.b2open");
            lang.ChangeCreateLangType(1);
            lang.CreateLangStr("Opened Window", "form","main.openwindow");
            lang.CLS("Window's location has been moved，", "form","main.windowmove");
            lang.CLS("Window's size has been changed，", "form","main.windowsize");
            lang.CLS("Location:", "form","main.location");
            lang.CLS("Size:", "form","main.size");
            lang.CLS("Lang has been changed to:", "form","lang.change");
            lang.CLS("Show log", "button","main.b1");
            lang.CLS("Log Viewer", "button","main.b2");
            lang.CLS("Window:Log has been opened", "form","main.bopen");
            lang.CLS("Window:Log Viewer has been opened", "form","main.b2open");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lang.ChangeReadLangType(comboBox1.SelectedIndex);
            log.Info(lang.RLS("form", "lang.change") + comboBox1.SelectedItem);
            this.button1.Text = lang.RLS("button", "main.b1");
            this.button2.Text = lang.RLS("button", "main.b2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v_OpenFolderPath = @".\logs\" + log.NowDate + "\\" + log.NowDate + ".log";
            System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
            log.Info(lang.RLS("form", "main.bopen"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logviewer logviewer = new logviewer();
            logviewer.Show();
            log.Info(lang.RLS("form", "main.b2open"));

        }
    }
}
