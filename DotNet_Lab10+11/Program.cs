using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab10_11
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            Task3();

        }
        static void Task1()
        {
            Zoo z = new Zoo();
            z.GetSound<Elephant>();
            z.GetSound<Lion>();
            z.GetSound<Monkey>();
        }
        static void Task2()
        {
            Order order = new Order();
            order.customerPhone = "8 000 88";
            order.customerStreet = "Al-Farabi, 89";

            var result = Validator.TryValidateObject(order, new ValidationContext(order, null, null), null, true);
            Console.WriteLine(result);

            order.customerPhone = "+7 (123) 458 0325";
            result = Validator.TryValidateObject(order, new ValidationContext(order, null, null), null, true);
            Console.WriteLine(result);

            order.customerPhone = "asdfa sdf 56";
            result = Validator.TryValidateObject(order, new ValidationContext(order, null, null), null, true);
            Console.WriteLine(result);

            order.customerPhone = "+7 (689) 789 6542";
            order.customerStreet = "Abai, haha";
            result = Validator.TryValidateObject(order, new ValidationContext(order), null, true);
            Console.WriteLine(result);
        }
        static void Task3()
        {
            // The path where the files we work with are located
            string path = @"D:\Dev\Test";
            DirectoryInfo dir = new DirectoryInfo(path);

            // A list to store our info about files from path directory
            List<FileSystemInfo> files = new List<FileSystemInfo>();
            files.AddRange(dir.GetFiles());


            // index of current selected file
            int index = 0;

            // The process which we start when open a file
            Process myProcess = null;

            while (true)
            {
                // Draw the files
                for (int i = 0; i < files.Count; i++)
                {
                    // Only the selected file has different background and foreground color
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    // All other files look the same
                    Console.WriteLine(files[i].Name);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // Draw the constant lower part
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("----------------------------");
                Console.WriteLine("Press Up/Down arrow keys to switch between the files");
                Console.WriteLine("Press F5 to open the selected file");
                Console.WriteLine("Press F6 to close the opened file");

                // Now we wait for user input
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    // The selected file changes to one above it unless it is already at the very top
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                        {
                            index--;
                        }
                        break;
                    // Similar to UpArrow
                    case ConsoleKey.DownArrow:
                        if (index < files.Count - 1)
                        {
                            index++;
                        }
                        break;
                    // Open the file depending on the extension of the file
                    case ConsoleKey.F5:
                        try
                        {
                            if (Extsn(files[index].Name) == "txt")
                            {
                                myProcess = Process.Start("notepad.exe", files[index].FullName);
                            }
                            else if (Extsn(files[index].Name) == "jpg")
                            {
                                // Couldn't find .exe of default picture viewer :((
                                myProcess = Process.Start("mspaint.exe", files[index].FullName);
                            }
                            else if (Extsn(files[index].Name) == "pdf")
                            {
                                // Acrobat Reader
                                myProcess = Process.Start("AcroRd32.exe", files[index].FullName);
                            }
                            else if (Extsn(files[index].Name) == "html")
                            {
                                myProcess = Process.Start("firefox.exe", files[index].FullName);
                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    // Close the File
                    case ConsoleKey.F6:
                        try
                        {
                            myProcess.Kill();
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
                // Clear the console before new cycle
                Console.Clear();
            }
        }

        // This method returns the file extension of the received file name
        public static string Extsn(string fileName)
        {
            string result = "";
            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName[i] == '.')
                {
                    break;
                }
                else
                {
                    result = fileName[i] + result;
                }   
            }
            return result;
        }
    }
}
