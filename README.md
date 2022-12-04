# 注意：本项目仅当练手写的，并非正式大项目，耗时两天左右，不算长，很多东西优化也没到位
# 请不要拿这个东西喷我说什么：“啊有现成的日志系统我不用，我来用你的垃圾日志系统，我图啥”、“这种东西也配叫日志系统和语言系统？xxs技术就别来炫耀了好吗？”、“不过是一个日志系统，网上有轮子，干嘛自己造”，“这东西传github是对github的侮辱！”，“就这技术？就这？”
# 语言+日志系统

## 基于C#的语言+日志系统

### 日志系统:

本来其实只打算做日志的，但是后来又想做一个以INI驱动的、与变量赋值类似的语言系统 （写的很垃圾就是了）

简单介绍下日志系统

（由于代码均未static所以需要new对象）

日志系统初始化比较简单，这里附上一个代码例子

```C#
using LogSystem
namespace LogTest
{
    class Prgm
    {
        static Log log = new Log();
        static void Main(string[] args)
        {
            LogInit();
            log.Info("info"); // 默认dark cyan颜色，后续更新可调颜色
            // 之前有计划过出可调颜色，不过有bug所以舍弃了
            log.Warn("warn"); // 默认dark yellow
            log.Error("error"); // 默认dark red
        }
        static void LogInit()
        {
            log.LogWriteInit();		// 多线程日志写入应用(此部分代码并非完全原创)
            log.TimeShowSet(true); // 让log输出的时候带上时间
        }
    }
}
```

### 语言系统:

语言系统完成度相对较低，缺陷和bug也不少，不过勉强能用吧

（毕竟是随手写的）

附上初始化代码

```C#
using LangSystem;
namespace langtest
{
    class prgm{
        Lang lang = new Lang();
        static void main(string[] args)
        {
            lang.Init();
            String[] LangType = {"Chinese", "English"};
            lang.SetLangType(LangType);
            langInit();
            CRLT(0) // 切换Chinsese
            Console.WriteLine(lang.RLS("wtl.start.title"));
            Console.WriteLine(lang.RLS("wtl.start.tip"));
        }
        void langInit()
        {
            lang.CCLT(0); // Chinese
            lang.CLS("欢迎来到WT", "wtl.start.title");
            lang.CLS("你可以在这里做任何你想做的事". "wtl.start.tip");
            lang.CCLT(1) // English
            lang.CLS("Welcome to WindyTown!", "wtl.start.title");
            lang.CLS（"U can do everything u want to do", "wtl.start.tip")
        }
    }
}
    
    

```
如果会报错表示xxx为null之类的话，请删掉lang文件夹然后再次打开软件

这个bug很好修，不过估计近期懒得动（


## 结尾

对这个程序不要抱有太大期望，毕竟是随手做的，加上我现在初三，没多少时间优化此项目

有意者可自行fork进行研究，不出意外的话不到明年暑假不会有很大的更新

