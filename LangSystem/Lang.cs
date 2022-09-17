using System.IO;
using LogSystem;

namespace LangSystem
{
    public class Lang
    {
        #region 使用教程
        /*
         using LangSystem;
         Lang lang = new Lang();
         public static void main(string[] args)
         {
            lang.Init();
            String[] LangType = {"Chinese", "English"};
            lang.SetLangType(LangType);
            langInit();
            CRLT(0) // 切换Chinsese
            Console.WriteLine(lang.RLS("wtl.start.title"));
            Console.WriteLine(lang.RLS("wtl.start.tip"));
         }
         public void langInit()
         {
            lang.CCLT(0); // Chinese
            lang.CLS("欢迎来到WT", "wtl.start.title");
            lang.CLS("你可以在这里做任何你想做的事". "wtl.start.tip");
            lang.CCLT(1) // English
            lang.CLS("Welcome to WindyTown!", "wtl.start.title");
            lang.CLS（"U can do everything u want to do", "wtl.start.tip")
         }
         */
        #endregion

        #region 相关变量定义
        private bool IsInit = false;
        private bool IsSetType = false;
        private string[] LangTypeText = null;    // 设置全局语言数组
        private int NowTypeIndex = 0;    // 不设置默认为LangType的第一位语言
        private int CreateTypeIndex = 0; // 上面那个是读的，这是写的
        INI ini = new INI();            // ini导包
        Log log = new Log();            // 自己的log导包
        # endregion

        # region DLL初始化阶段
        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Init()
        {
            if (CheckInitType("checkinit"))
            {
                return;
            }
            if (!Directory.Exists(@".\lang\"))
            {
                Directory.CreateDirectory(@".\lang\");
            }
            IsInit = true;
            log.TimeShowSet(true);
            // log.Info("语言模块已加载");
            ini.INIWrite("langSys", "IsInit", "true", @".\lang\settings.ini");
        }

        /// <summary>
        /// 设置语种
        /// </summary>
        /// <param name="Type">语种数组</param>
        public void SetLangType(string[] Type)
        {
            if (CheckInitType("checklangtype"))
            {
                return;
            }
            if (!IsInit)
            {
                log.Error("您还没有init，请先init再执行此指令。");
                return;
            }
            LangTypeText = Type;
            IsSetType = true;
            ini.INIWrite("langSys", "IsSetType", "true", @".\lang\settings.ini");
            ini.INIWrite("langSys", "LangTypeText", Array2String(Type), @".\lang\settings.ini");
            // log.Info("语种设置成功");
        }
        #endregion

        #region 主类(缩写模式)
        /// <summary>
        /// CreateLangStr
        /// 向INI写入 缩写模式
        /// </summary>
        /// <param name="LangStr">欲写入文本</param>
        /// <param name="INIPointer">欲写入值</param>
        public void CLS(string LangStr, string INIPointer)
        {
            if (CheckInitType("bool") == false)
            {
                return;
            }
            try
            {
                ini.INIWrite("lang", INIPointer, LangStr, @".\lang\" + LangTypeText[CreateTypeIndex] + ".ini");
                // log.Info("已为语种 " + LangTypeText[CreateTypeIndex] + " 写入值 " + INIPointer);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ChangeCreateLangType
        /// 设置CreateType(即现在的语种)创建用 缩写模式
        /// </summary>
        /// <param name="ToChangeIndex">欲修改的语种数组下标</param>
        public void CCLT(int ToChangeIndex)
        {
            if (LangTypeText == null)
            {
                log.Error("请先设置一个LangType");
                return;
            }
            CreateTypeIndex = ToChangeIndex;
            try
            {
                ini.INIWrite("langSys", "NowTypeIndex", ToChangeIndex.ToString(), @".\lang\settings.ini");
                // log.Info("已修改CreateLangType");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ReadLangStr
        /// 读字符串 缩写模式
        /// </summary>
        /// <param name="INIPointer">欲读值</param>
        /// <returns></returns>
        public string RLS(string INIPointer)
        {
            if (CheckInitType("bool") == false)
            {
                return "";
            }
            try
            {
                // log.Info("已从语种 " + LangTypeText[NowTypeIndex] + " 读出值 " + INIPointer);
                return ini.INIRead("lang", INIPointer, @".\lang\" + LangTypeText[NowTypeIndex] + ".ini");
            }
            catch
            {
                log.Error("程序出现了一个致命错误！可能是您的INIPointer或LangTypeIndex没填对。");
                throw;
            }
            
        }

        /// <summary>
        /// ChangeReadLangType
        /// 设置NowType(即现在的语种)设置完需要重启 缩写模式
        /// </summary>
        /// <param name="ToChangeIndex">欲修改的语种数组下标</param>
        public void CRLT(int ToChangeIndex)
        {
            if (LangTypeText == null)
            {
                log.Error("请先设置一个LangType");
                return;
            }
            NowTypeIndex = ToChangeIndex;
            try
            {
                ini.INIWrite("langSys", "NowTypeIndex", ToChangeIndex.ToString(), @".\lang\settings.ini");
                // log.Info("已设置NowLangType");
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 主类(全拼模式)(调用缩写)
        /// <summary>
        /// 向INI写入
        /// </summary>
        /// <param name="LangStr">欲写入文本</param>
        /// <param name="INIPointer">欲写入值</param>
        public void CreateLangStr(string LangStr, string INIPointer)
        {
            CLS(LangStr, INIPointer);
        }

        /// <summary>
        /// 设置NowType(即现在的语种)设置完需要重启
        /// </summary>
        /// <param name="ToChangeIndex">欲修改的语种数组下标</param>
        public void ChangeCreateLangType(int ToChangeIndex)
        {
            CCLT(ToChangeIndex);
        }

        /// <summary>
        /// 设置NowType(即现在的语种)设置完需要重启
        /// </summary>
        /// <param name="ToChangeIndex">欲修改的语种数组下标</param>
        public void ChangeReadLangType(int ToChangeIndex)
        {
            if (LangTypeText.Equals(null))
            {
                log.Error(" 请先设置一个LangType");
                return;
            }
            NowTypeIndex = ToChangeIndex;
            try
            {
                ini.INIWrite("langSys", "NowTypeIndex", ToChangeIndex.ToString(), @".\lang\settings.ini");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读字符串
        /// </summary>
        /// <param name="INIPointer">欲读值</param>
        /// <returns></returns>
        public string ReadLangStr(string INIPointer)
        {
            return RLS(INIPointer);
        }
        #endregion

        #region 私有类/重复使用类
        /// <summary>
        /// 私有类，检查Init用
        /// </summary>
        /// <param name="mode">模式(check+bool)</param>
        /// <returns>是否成功</returns>
        private bool CheckInitType(string mode)
        {
            if (mode == "checkinit")
            {
                log.TimeShowSet(true);
                if (Directory.Exists(@".\lang\"))
                {
                    if (ini.INIRead("langSys", "IsInit", @".\lang\settings.ini") == "true")
                    {
                        IsInit = true;
                    }
                    return true;
                }
                return false;
            }
            else if (mode == "checklangtype")
            {
                if (Directory.Exists(@".\lang\"))
                {
                    if (ini.INIRead("langSys", "IsSetType", @".\lang\settings.ini") == "true")
                    {
                        IsSetType = true;
                        LangTypeText = String2Array(
                            ini.INIRead("langSys", "LangTypeText", @".\lang\settings.ini"), ';'
                            );
                        return true;
                    }
                }
                return false;
            }
            else if (mode == "bool")
            {
                if (IsInit == false)
                {
                    log.Error("请先Init");
                    return false;
                }
                if (IsSetType == false)
                {
                    log.Error(" 请先SetType");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// string[]转String
        /// </summary>
        /// <param name="s">欲转数组</param>
        /// <returns>转成功的String</returns>
        public string Array2String(string[] s)
        {
            string temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (i == s.Length - 1)
                {
                    temp += s.GetValue(i).ToString();
                }
                else
                {
                    temp += s.GetValue(i).ToString() + ";";
                }
            }
            return temp;
        }

        /// <summary>
        /// string转string[]
        /// </summary>
        /// <param name="s">欲转string</param>
        /// <param name="SplitChar">欲分割符号</param>
        /// <returns>转成功的string[]</returns>
        public string[] String2Array(string s, char SplitChar)
        {
            string[] result = s.Split(SplitChar);
            return result;
        }
        #endregion
    }
}
