namespace MyTests
{
    class Session
    {
        public static Users User { get; set; }
        public static Tests OpenedTest { get; set; }
        public static int Points = 0;
        public static int CurQuestion = 0;
        public static class Quest
    {
            public static string[] Content;
            public static string[] Answer;
        }
    }
}
