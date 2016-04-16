using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet_Lab12
{
    class Program
    {
        // class for Snowflakes
        class Snowflake
        {
            char sflake;

            // quantity of snowflakes at the start (first loop) before sleep time
            int quantity;
            private static readonly Random rnd = new Random();
            private static readonly object flakeLock = new object();

            // initial sleep time
            int sleepTime = 2000;

            public Snowflake(char snowflake, int n)
            {
                sflake = snowflake;
                this.quantity = n;
            }

            public static int RandomCoordinate(int x, int y)
            {
                // Stop other threads from accessing or changing rnd
                lock (flakeLock)
                {
                    return rnd.Next(x, y);
                }
            }

            // Draw snowflakes
            public void Draw()
            {
                int count = 0;

                while (count < 100)
                {
                    // Random position on console
                    Console.SetCursorPosition(RandomCoordinate(0, Console.WindowWidth-1), RandomCoordinate(0, Console.WindowHeight-1));
                    Console.Write(sflake);

                    if (count == quantity)
                    {
                        Thread.Sleep(sleepTime);
                        count = 0;

                        // decrease sleep time but it should not become negative
                        if (sleepTime > 200)
                        {
                            sleepTime -= 200;
                        }

                        // increase number of snowflakes by 5 each time
                        quantity += 5;
                    }
                    count++;
                }
            }
        }

        // class for Task 3, this class was used in previous lab
        [Serializable]
        public class Lion
        {
            public string Name { get; set; }
            [NonSerialized]
            int legs;
            public int Legs { 
                get { return legs; }
                set { legs = value; }
            }
            public int Size { get; set; }
            public string SkinColor { get; set; }
        
            public Lion()
            {
                Name = "I have no name!";
                legs = 4;
                Size = 10;
                SkinColor = "Yellow";
            }
            public string MakeSound()
            {
                return "Rghfhrhghgh!!! I'm a Lion";
            }
            public string Kill()
            {
                return "Killing!";
            }

        }

        /// <summary>
        /// файловый менеджер, который использует Console.ReadKey()
        /// </summary>
        static void FileMan1()
        {
            string path = @"D:\Games\Test";

            FileSystemWatcher watch = new FileSystemWatcher(path);
            watch.Changed += new FileSystemEventHandler(OnChanged);
            watch.Created += new FileSystemEventHandler(OnChanged);
            watch.Deleted += new FileSystemEventHandler(OnChanged);
            watch.EnableRaisingEvents = true;

            DirectoryInfo dir = new DirectoryInfo(path);

            //Все директории и файлы из dir запоминаем в list
            List<FileSystemInfo> items = new List<FileSystemInfo>();
            items.AddRange(dir.GetDirectories());
            items.AddRange(dir.GetFiles());

            //подсвеченный каталог или файл
            int index = 0;

            //открыт ли файл
            bool fileOpened = false;

            //флаг для остановки программы
            bool run = true;

            while (run)
            {
                //Рисование
                if (fileOpened)
                {
                    string line;
                    StreamReader sr = new StreamReader(items[index].FullName);
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                    sr.Close();
                }
                else
                {
                    //вывод папок и управление ими
                    for (int i = 0; i < items.Count; ++i)
                    {
                        if (i == index)
                        {
                            //папка
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        if (items[i].GetType() == typeof(FileInfo))
                        {
                            //файл зеленый
                            Console.ForegroundColor = ConsoleColor.Green;

                            //выпысываем файли и доп инфу
                            Console.Write("{0} \t{1} \t{2} {3}", items[i].Name, items[i].Extension, items[i].LastWriteTime, Environment.NewLine);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;

                        }

                        Console.WriteLine(items[i].Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;

                        
                    }
                    Console.WriteLine(Environment.NewLine + Environment.NewLine + "F2 to create a directory;\tF3 to create a text file;\tF4 to zip file");
                }

                //нажатие клавиш
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if ((index > 0) && (!fileOpened)) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if ((index < items.Count - 1) && (!fileOpened)) index++;
                        break;
                    case ConsoleKey.Enter:
                        //если это файл
                        if (items[index].GetType() == typeof(FileInfo))
                        {
                            fileOpened = true;
                        }
                        //если это папка
                        if (items[index].GetType() == typeof(DirectoryInfo))
                        {
                            path = items[index].FullName;
                            dir = new DirectoryInfo(path);
                            items.Clear();
                            items.AddRange(dir.GetDirectories());
                            items.AddRange(dir.GetFiles());
                            index = 0;
                        }

                        break;
                    case ConsoleKey.Escape:
                        try
                        {
                            //Выходит в родительскую папку, если не открыт файл
                            if (!fileOpened)
                            {
                                if (Directory.Exists(dir.Parent.FullName))
                                {
                                    path = dir.Parent.FullName;

                                    dir = new DirectoryInfo(path);
                                    items.Clear();
                                    items.AddRange(dir.GetDirectories());
                                    items.AddRange(dir.GetFiles());
                                    index = 0;
                                }
                                else
                                {
                                }

                            }
                            else
                            {
                                //закрываем файл
                                fileOpened = false;
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case ConsoleKey.X:
                        run = false;
                        break;
                    case ConsoleKey.F2:
                        Console.Clear();
                        Console.Write("Enter new folder name: ");
                        string newFolderName = Console.ReadLine();
                        Directory.CreateDirectory(path + "\\" + newFolderName);

                        items.Clear();
                        items.AddRange(dir.GetDirectories());
                        items.AddRange(dir.GetFiles());
                        break;
                    case ConsoleKey.F3:
                        Console.Clear();
                        Console.Write("Enter new text file name: ");
                        string newFileName = Console.ReadLine();

                        FileStream fs = new FileStream(path + "\\" + newFileName + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write("Yay, I created a file!");

                        sw.Close();
                        fs.Close();

                        items.Clear();
                        items.AddRange(dir.GetDirectories());
                        items.AddRange(dir.GetFiles());
                        break;
                    case ConsoleKey.Delete:
                        if (items[index].GetType() == typeof(FileInfo))
                        {
                            File.Delete(items[index].FullName);    
                        }

                        items.Clear();
                        items.AddRange(dir.GetDirectories());
                        items.AddRange(dir.GetFiles());
                        break;
                    case ConsoleKey.F4:
                        if (items[index].GetType() == typeof(FileInfo))
                        {
                            ZipArchive zpa = ZipFile.Open(path + @"\Archive.zip", ZipArchiveMode.Create);
                            zpa.CreateEntryFromFile(items[index].FullName, items[index].Name);
                            zpa.Dispose();
                        }
                        items.Clear();
                        items.AddRange(dir.GetDirectories());
                        items.AddRange(dir.GetFiles());
                        break;
                }
                Console.Clear();
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            WatcherChangeTypes wachertype = e.ChangeType;
            FileStream fs = new FileStream(@"D:\Games\Test\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("File {0} {1}", e.FullPath, wachertype.ToString());

            sw.Close();
            fs.Close();
        }


        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            Task3();
        }

        public static void Task1()
        {
            // Setup windows size of console
            Console.SetWindowSize(100, 35);

            Snowflake snow1 = new Snowflake('^', 5);
            Snowflake snow2 = new Snowflake('#', 10);
            Snowflake snow3 = new Snowflake('*', 20);
               
            // Create 3 threads that run method draw of variables snow1, snow2, snow3
            Thread thread1 = new Thread(snow1.Draw);
            Thread thread2 = new Thread(snow2.Draw);
            Thread thread3 = new Thread(snow3.Draw);

            //Start all threads
            thread1.Start();
            thread2.Start();

            Thread.Sleep(100);
            thread3.Start();
        }

        public static void Task2() 
        {
            FileMan1();
        }

        public static void Task3()
        {
            Lion daisy = new Lion();
            daisy.Name = "Daisy";
            Console.WriteLine("Lion name is: {0}", daisy.Name);
            daisy.Legs = 2;
            Console.WriteLine("She has: {0} legs", daisy.Legs);
            Console.WriteLine("{0}Now serialize Daisy", Environment.NewLine );

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("save.dat", FileMode.OpenOrCreate, FileAccess.Write);

            bf.Serialize(fs, daisy);
            fs.Close();

            Lion newLion = new Lion();
            Console.WriteLine("A new lion name is: {0}", newLion.Name);
            Console.WriteLine("He has: {0} legs", newLion.Legs);
            Console.WriteLine("{0}Now deserialize Daisy into this new lion", Environment.NewLine);

            FileStream fs_in = new FileStream("save.dat", FileMode.Open, FileAccess.Read);
            newLion = bf.Deserialize(fs_in) as Lion;
            fs_in.Close();

            // Notice that number of legs will not change

            Console.WriteLine("{0}A new lion name is: {1}", Environment.NewLine, newLion.Name);
            Console.WriteLine("He has: {0} legs", newLion.Legs);
        }
    }
}
