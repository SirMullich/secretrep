using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в Супер Программу!");
            bool run = true;
            List<int> id = new List<int>();
            List<string> value = new List<string>();
            while (run)
            {
                int command = 0;
                Console.WriteLine("1-Добавить запись; 2-Удалить запись; 3-Поиск записи; 4-Изменить запись; 5-Вывести базу; 6-Выход");
                Console.Write("Выберите действие: ");
                command = int.Parse(Console.ReadLine());
                int input;  //Ввод ID для операций
                
                switch (command)
                {
                    //Добавить
                    case 1:
                        Console.Write("Введите ID: ");
                        input = int.Parse(Console.ReadLine());
                        if (!id.Contains(input))
                        {
                            id.Add(input);
                            Console.Write("Введите имя: ");
                            value.Add(Console.ReadLine());
                        }
                        else
                        {
                            Console.WriteLine("Такой ID уже существует!");
                        }
                        break;
                    //Удалить
                    case 2:
                        Console.Write("Введите ID: ");
                        input = int.Parse(Console.ReadLine());
                        value.RemoveAt(id.IndexOf(input));
                        id.RemoveAt(id.IndexOf(input));
                        break;
                    //Поиск
                    case 3:
                        Console.Write("Введите ID: ");
                        input = int.Parse(Console.ReadLine());
                        Console.WriteLine("Результат поиска: " + value[id.IndexOf(input)]);
                        break;
                    //Изменить запись
                    case 4:
                        Console.Write("Введите ID: ");
                        input = int.Parse(Console.ReadLine());
                        if (id.Contains(input))
                        {
                            Console.Write("Введите новое значение: ");
                            value[id.IndexOf(input)] = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Такой ID НЕ существует!");
                        }
                        break;
                    //Вывод
                    case 5:
                        for (int i = 0; i < id.Count; ++i)
                        {
                            Console.WriteLine("{0} имеет значение {1}", id[i], value[i]);
                        }
                        Console.WriteLine();
                        break;
                    //Выход
                    case 6:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Неправильная команда!");
                        run = false;
                        break;
                }
            }
        }
    }
}
