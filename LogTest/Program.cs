using System;
using System.Threading;
using LogSystem;
using LangSystem;

namespace LogLangTest
{
    class Program
    {
        static Lang lang = new Lang();
        static Log log = new Log();
        static void Main(string[] args)
        {
            LogInit();
            lang.Init(); // 让dll 初始化
            LangInit();  // 程序语言初始化
            while (true)
            {
                lang.CRLT(0);   // 切换Chinese
                log.Info(lang.RLS("log.awa"));
                Thread.Sleep(100);
                log.Warning(lang.RLS("log.ukwarning"));
                Thread.Sleep(100);
                log.Error(lang.RLS("log.ukerror"));
                Thread.Sleep(100);
                lang.CRLT(1);   // 切换English
                log.Info(lang.RLS("log.awa"));
                Thread.Sleep(100);
                log.Warning(lang.RLS("log.ukwarning"));
                Thread.Sleep(100);
                log.Error(lang.RLS("log.ukerror"));
                Thread.Sleep(100);
            }
            
            /*
            log.Info(lang.RLS("window.clw"));
            LangGs lgs = new LangGs
            {
                LangGS = lang
            };
            ChangeLangWin c = new ChangeLangWin();
            c.Show();
            */
        }
        static void LangInit()
        {
            lang.Init();
            string[] s = { "Chinese", "English" };
            lang.SetLangType(s);
            lang.CCLT(0);
            lang.CLS("awa", "log.awa");
            lang.CLS("糟糕！程序出现了一些故障。", "log.ukwarning");
            lang.CLS("糟糕，程序有错误了！", "log.ukerror");
            lang.CLS("正在载入窗口: ChangeLangWin", "window.clw");
            lang.CCLT(1);
            lang.CLS("awa", "log.awa");
            lang.CLS("Whopps, the program seems to have an warning", "log.ukwarning");
            lang.CLS("Whopps, the program has set off and thrown the error.", "log.ukerror");
            lang.CLS("loading window: ChangeLangWin", "window.clw");
        }
        static void LogInit()
        {
            log.LogWriteInit();
            log.TimeShowSet(true);
        }
    }
}
