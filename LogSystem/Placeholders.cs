using System;
using LogSystem;

namespace LogSystem
{
    public class Placeholders
    {
        public string TimeGet => DateTime.Now.ToString("T");
        public bool Is_Time_Get_Set { get; set; }
    }
}
