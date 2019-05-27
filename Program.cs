using System;

namespace PathOfThal
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Path())
                game.Run();
        }
    }
}
