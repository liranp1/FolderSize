using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static string searchDirectory;
        public static long totalSize;
        public static string returnSize;

        static void Main(string[] args)
        {
            try
            {
                searchDirectory = args[0];
                //Console.WriteLine(FormatBytes(GetFileSizeSumFromDirectory(searchDirectory)));

                // Create a thread and call a background method
                Thread backgroundThread = new Thread(new ThreadStart(Program.RunAllMethods));
                // Start thread
                backgroundThread.Start();
                while (backgroundThread.IsAlive)
                {
                    Console.WriteLine("\\");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                    Console.WriteLine("|");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                    Console.WriteLine("/");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                    Console.WriteLine("-");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                }

                Console.WriteLine(FormatBytes(totalSize));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Usage of folder size tool: FolderSize.exe <PATH>");
                Console.WriteLine(ex.Message);
            }
        }

        public static void RunAllMethods()
        {
            GetFileSizeSumFromDirectory(searchDirectory);
        }

        public static long GetFileSizeSumFromDirectory(string searchDirectory)
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // get the sizeof all files in the current directory
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // get the size of all files in all subdirectories
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();
            totalSize = currentSize + subDirSize;
            return currentSize + subDirSize;
        }

        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            //returnSize = String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}