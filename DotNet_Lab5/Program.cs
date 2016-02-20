using System;
using System.Collections.Generic;
//using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab5
{
    class TwoArrayLists
    {
        
        public static ArrayList Union (ArrayList A, ArrayList B)
        {
            ArrayList result = new ArrayList();
            foreach (int ele in A)
            {
                if (!result.Contains(ele))
                {
                    result.Add(ele);
                }
            }
            foreach (int ele in B)
            {
                if (!result.Contains(ele))
                {
                    result.Add(ele);
                }
            }
            return result;
        }
        public static ArrayList Intersection(ArrayList A, ArrayList B)
        {
            ArrayList result = new ArrayList();
            foreach (int elA in A)
            {
                if (B.Contains(elA))
                {
                    result.Add(elA);
                }
            }
            return result;
        }
        public static ArrayList Add(ArrayList A, ArrayList B)
        {
            ArrayList result = new ArrayList();
            result.AddRange(A);
            result.AddRange(B);
            return result;
        }
        public static ArrayList Substract(ArrayList A, ArrayList B)
        {
            ArrayList result = new ArrayList();
            foreach (int ele in A)
            {
                if (!B.Contains(ele))
                {
                    result.Add(ele);
                }
            }
            return result;
        }
        public static ArrayList EliminateRepeat(ArrayList A, ArrayList B)
        {
            return Substract(Union(A, B), Intersection(A, B));
        }
        
    }

    class MyStack
    {
        ArrayList elements;
        public MyStack()
        {
            elements = new ArrayList();
        }
        public bool Push(object obj)
        {
            bool success = false;
            elements.Add(obj);
            success = true;
            return success;
        }
        public object Pop()
        {
            object result = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            return result;
        }
        public int Count()
        {
            return elements.Count;
        }
    }

    class MyQueue
    {
        ArrayList elements;
        public MyQueue()
        {
            elements = new ArrayList();
        }
        public bool Push(object obj)
        {
            bool success = false;
            elements.Add(obj);
            success = true;
            return success;
        }
        public object Pop()
        {
            object result = elements[0];
            elements.RemoveAt(0);
            return result;
        }
        public int Count()
        {
            return elements.Count;
        }
    }

    // Класс Paint хранит координаты точки, а также её значение
    // Например монета за 60, в координатах x, y
    class Point
    {
        public int x;
        public int y;
        public int value;

        public Point()
        {

        }
    }

    // Класс для доски
    class Board
    {
        // Лист хранит правильно отгаданные точки
        public List<Point> guessed = new List<Point>();

        public Board()
        {

        }

        // Метод рисования
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Your board is: ");

            // Draw first line (1 2 3 4 5 6)
            Console.SetCursorPosition(0, 1);
            Console.Write("\t");
            for (int i = 1; i < 7; i++)
            {
                Console.Write("{0}\t", i);
            }

            // Draw "X" symbols
            Console.Write("\n\r\t");
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    Console.Write("X\t");
                }
                Console.Write("\n\r\t");
            }

            // Draw first column (A B C D E F)
            int c = 64;
            for (int i = 1; i < 7; i++)
            {
                Console.SetCursorPosition(0, i+1);
                Console.Write(Convert.ToChar(c + i));
            }

            // Draw guessed boxes
            // Рисуется в конце, поверх "Х"-ов
            foreach (Point p in guessed)
            {
                Console.SetCursorPosition(8 * p.x, p.y + 1);
                Console.Write(p.value);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Collections Task 1
            //Task1();

            // Collections Task 2
            //Task2();

            // Generics Task1
            Task3();
        }
        public static void Task1() 
        {
            ArrayList firstArrayList = new ArrayList();
            firstArrayList.Add(1);
            firstArrayList.Add(5);
            firstArrayList.Add(-2);
            firstArrayList.Add(6);

            ArrayList secondtArrayList = new ArrayList();
            secondtArrayList.Add(3);
            secondtArrayList.Add(6);
            secondtArrayList.Add(10);
            secondtArrayList.Add(-5);
            secondtArrayList.Add(1);
            secondtArrayList.Add(4);

            Console.Write("A : ");
            foreach (int ele in firstArrayList)
            {
                Console.Write("{0} ", ele);
            }
            Console.Write("\nB : ");
            foreach (int ele in secondtArrayList)
            {
                Console.Write("{0} ", ele);
            }
            Console.Write("\n\nA union B: ");
            foreach (int ele in TwoArrayLists.Union(firstArrayList, secondtArrayList))
            {
                Console.Write("{0} ", ele.ToString());
            }
            Console.Write("\nA + B: ");
            foreach (int ele in TwoArrayLists.Add(firstArrayList, secondtArrayList))
            {
                Console.Write("{0} ", ele);
            }
            Console.Write("\nA intersection B: ");
            foreach (int ele in TwoArrayLists.Intersection(firstArrayList, secondtArrayList))
            {
                Console.Write("{0} ", ele);
            }
            Console.Write("\nA - B: ");
            foreach (int ele in TwoArrayLists.Substract(firstArrayList, secondtArrayList))
            {
                Console.Write("{0} ", ele);
            }
            Console.Write("\n(A u B) - (A ^ B): ");
            foreach (int ele in TwoArrayLists.EliminateRepeat(firstArrayList, secondtArrayList))
            {
                Console.Write("{0} ", ele);
            }
            Console.WriteLine();
        }

        public static void Task2()
        {
            MyStack stck = new MyStack();
            stck.Push("First");
            stck.Push(6);
            stck.Push("hehe");
            stck.Push(8);
            stck.Push("Last");
            int count = stck.Count();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(stck.Pop().ToString());    
            }
            Console.WriteLine();
            MyQueue que = new MyQueue();
            que.Push("First");
            que.Push(3);
            que.Push("hehe");
            que.Push(0);
            que.Push("Last");
            count = que.Count();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(que.Pop().ToString());    
            }
        }

        public static void Task3()
        {
            Board brd = new Board();

            // Dictionary для хранения монет
            Dictionary<string, int> coins = new Dictionary<string, int>();

            Random rnd = new Random();

            // Переменная для генерации номинала монет. Умножается на 10. Серия: 10, 20, ..., 100
            int number = 1;

            // Ключ для dictionary, который мы случайно генерируем
            string key = "";

            // Пока в dictionary не 10 монет, случайно генерируем монеты
            while (coins.Count < 10)
            {
                // Используем ASCII код символов
                // Например, А = 64, B = 65 и т.д.
                key += Convert.ToChar(rnd.Next(1, 7) + 64);
                key += rnd.Next(1, 7);

                // Сохраняем только уникальные монеты, которых нет в листе
                if (!coins.ContainsKey(key))
                {
                    coins[key] = number * 10;
                    number++;
                }
                // В конце обнуляем ключ, чтобы туда же сгенерировать новый
                key = "";
            }

            // Хранит общие количество очков
            int score = 0;

            // Хранит количество очков полученных в выигранном раунде
            int scoredNow = 0;

            // Ввод пользователя
            string input;

            // Флаг - отгадал или нет
            bool sniped = false;

            // Цикл для 15 раундов
            for (int i = 0; i < 15; i++)
            {
                // Сначала рисуем доску
                Console.Clear();
                brd.Draw();

                // For Debug purposes
                // Справа внизу выписываем сколько у нас точек в листе угаданных точек
                //Console.SetCursorPosition(40, 20);
                //Console.Write(brd.guessed.Count);

                Console.SetCursorPosition(0, 9);

                // For debug purposes
                // Выводит положение монет
                //foreach (string k in coins.Keys)
                //{
                //    Console.Write(k + "  ");
                //}
                //Console.WriteLine();

                Console.WriteLine("Enter the key and value: ");
                input = Console.ReadLine();

                // если угадал
                if (coins.ContainsKey(input))
                {
                    scoredNow = coins[input];
                    score += scoredNow;

                    // Создаем и добавляем точку со значением в лист угаданных точек
                    // Также корректируем координаты. Используем ASCII код символов
                    // Для цифр 0-9 ASCII код: "0" = 48, "1" = 49, "2" = 50 и т.д.
                    brd.guessed.Add(new Point() { x = Convert.ToInt32(input[1] - 48), y = Convert.ToInt32(input[0]) - 64, value = coins[input] });

                    // Удаляем этот ключ из монет, чтобы нельзя было отгадать одну и ту же монету несколько раз
                    coins.Remove(input);
                    sniped = true;
                }

                // Если отгадал
                if (sniped)
                {
                    // Проверка на победу
                    if (coins.Keys.Count == 0)
                    {
                        Console.Clear();
                        brd.Draw();

                        // Перемещаем курсор вниз доски
                        Console.SetCursorPosition(0, 10);

                        // Пишем сообщение
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("!!!!!!YOU WON!!!!!!!");
                        Console.ResetColor();
                        Console.WriteLine("Good Game, Well Played!");

                        // Выход из цикла
                        break;
                    }
                    else
                    {
                        // Отгадал, но еще не выиграл
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Well Done! You earned: {0}", scoredNow);
                        Console.ResetColor();
                    }
                }
                else
                {
                    // Не отгадал
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oops! Missed!");
                    Console.ResetColor();
                }

                // Если не выиграл и это последний раунд
                if (i == 14)
                {
                    // Всё стираем и выводим сообщение о проигрыше
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Game over!");
                    Console.ResetColor();
                    Console.WriteLine("Your final score is: {0}", score);
                    Console.WriteLine("\n\rYour coins were at: ");
                    foreach (string k in coins.Keys)
                    {
                        Console.WriteLine("{0} - {1}", k, coins[k]);
                    }
                    break;
                }
                else
                {
                    // Если не отгадал и это не последний раунд
                    Console.WriteLine("Your score is: {0} \n\rAttempts left: {1} \n\rCoins to find: {2}", score, 14 - i, coins.Keys.Count);
                    sniped = false;

                    // Это нужно чтобы экран сразу не стирался
                    // Ждем любового нажатия клавиши
                    Console.ReadKey();
                }
            }
        }
    }
}
