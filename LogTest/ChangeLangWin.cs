using LangSystem;
using System;
using System.Windows.Forms;

namespace LogLangTest
{
    public partial class ChangeLangWin : Form
    {
        INI ini = new INI();
        LangGs lgs = new LangGs();

        public ChangeLangWin()
        {
            InitializeComponent();
            Lang lang = lgs.LangGS;
            if (ini.INIRead("langSys", "IsSetType", @".\lang\settings.ini") != "true")
            {
                Console.WriteLine("[LangSystem] 请先SetType");
                Close();
                return;
            }
            langBox.DataSource = lang.String2Array(ini.INIRead(
                "langSys", "LangTypeText", @".\lang\settings.ini"
                ), ';'
            );
        }

        private void langBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Lang lang = lgs.LangGS;
            // 当值被改变时就改变当前的语言
            lang.CRLT(langBox.SelectedIndex);
        }
    }
}
