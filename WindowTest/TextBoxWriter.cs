﻿using System.IO;
using System.Text;
using System.Windows.Forms;

internal class TextBoxWriter : TextWriter

{
    TextBox textBox;
    delegate void WriteFunc(string value);
    WriteFunc write;
    WriteFunc writeLine;

    public override Encoding Encoding => throw new System.NotImplementedException();

    public TextBoxWriter(TextBox textBox)
    {
        this.textBox = textBox;
        write = Write;
        writeLine = WriteLine;
    }

    // 最低限度需要重写的方法
    public override void Write(string value)
    {
        if (textBox.InvokeRequired)
            textBox.BeginInvoke(write, value);
        else
            textBox.AppendText(value);
    }


    // 为提高效率直接处理一行的输出
    public override void WriteLine(string value)
    {
        if (textBox.InvokeRequired)
            textBox.BeginInvoke(writeLine, value);
        else
        {
            textBox.AppendText(value);
            textBox.AppendText(this.NewLine);
        }
    }
}