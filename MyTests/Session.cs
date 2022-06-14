using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTests
{
    class Session
    {
        public static int UserId = 1; //{ get; set; }
        public static Tests OpenedTest { get; set; }
        public static int Points = 0;
        public static int CurQuestion = 1;
        public class Quest
        {
            public static string[] Content = new string[OpenedTest.Questions.Count];
            public static string[] Answer = new string[OpenedTest.Questions.Count];
        }
    }
}
