using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogSystem
{
    public class Log
    {
        // 定义基本变量
        public string normal = "[LogSys]"; // 基本输出字符
        
        private Placeholders places = new Placeholders();
        private readonly string[] LangType = { "Chinese", "English" };
        
        public void TimeShowSet(bool isTimeShow)
        {
            places.Is_Time_Get_Set = isTimeShow;
        }
        // INFO
        public void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string text = "[INFO] " + info;
            string textWithTime = "[" + places.TimeGet + "]" + "[INFO] " + info;
            if (places.Is_Time_Get_Set)
            {
                Console.WriteLine(textWithTime);
            } else
            {
                Console.WriteLine(text);
            }
            LogWrite(info, "INFO");
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // WARNING
        public void Warning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string text = "[WARN] " + warning;
            string textWithTime = "[" + places.TimeGet + "]" + "[WARN] " + warning;
            if (places.Is_Time_Get_Set)
            {
                Console.WriteLine(textWithTime); 
            }
            else
            {
                Console.WriteLine(text);
            }
            LogWrite(warning, "WARN");
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // ERROR
        public void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string text = "[ERROR] " + error;
            string textWithTime = "[" + places.TimeGet + "]" + "[ERROR] " + error;
            if (places.Is_Time_Get_Set)
            {
                Console.WriteLine(textWithTime);
            }
            else
            {
                Console.WriteLine(text);
            }
            LogWrite(error, "ERROR");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        // 2017-07-24格式
        private readonly string NowDate = DateTime.Now.ToString("yyyy.MM.dd");

        public void LogWriteInit()
        {
            // log文件不存在，就创建
            if (!Directory.Exists(@".\logs\"))
            {
                Directory.CreateDirectory(@".\logs\");
            }
            // nowdate不存在，就创建
            if (!Directory.Exists(@".\logs\" + NowDate))
            {
                Directory.CreateDirectory(@".\logs\" + NowDate);
            }
            // 如果昨天的还存在就打包且删除
            /*
            if (Directory.Exists(@".\logs\" + YesterDate))
            {
                ZipFile(YesterDate, @".\logs\" + YesterDate);
                Directory.Delete(@".\logs\" + YesterDate);
            }*/
        }

        private void LogWrite(string log, string LogLevel)
        {
            Task.Factory.StartNew(() =>
            {
                if (LogLevel == "INFO")
                {
                    LogAsyncWriter.Default.Info(log, "Thread.Main");
                }
                else if (LogLevel == "ERROR")
                {
                    LogAsyncWriter.Default.Error(log, "Thread.Main");
                }
                else if (LogLevel == "WARN")
                {
                    LogAsyncWriter.Default.Warn(log, "Thread.Main");
                }
                else
                {
                    LogError("请让X_huihui修bug：\n" +
                        "LogSystem.Log.LogWrite line 166, Argument:LogLevel:" +
                        LogLevel + "error: Unknow found");
                }
            });
            
            // File.WriteAllText(File.ReadAllText(@".\logs\" + NowDate + @"\lastest.log") + @".\logs\" + NowDate + @"\lastest.log", log + "\n");
        }

        /*
        /// <summary>
        /// 创建Zip对象
        /// </summary>
        /// <param name="filename">文件名.</param>
        /// <param name="directory">将要压缩的目录.</param> 
        private void ZipFile(string filename, string directory)
        {
            try
            {
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        */

        private void LogError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(normal + "[" + places.TimeGet + "]" + "[ERROR]" + error);
            LogAsyncWriter.Default.Error(error, "Thread.Main");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
