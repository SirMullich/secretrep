using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab4
{
    /// <summary>
    /// Exception class for Task3 - User login
    /// </summary>
    class LoginException : Exception
    {
        //public override string Message
        //{
        //    get
        //    {
        //        return base.Message;
        //    }
        //}
        //Конструктор принимает сообщение
        public LoginException(string msg)
        {
            switch (msg)
            {
                //Если придет ошибка о пароле
                case "password":
                    Console.WriteLine("Entered password is wrong");
                    break;
                //Если придет ошибка о каптче
                case "captcha":
                    Console.WriteLine("Wrong captcha. Are you a robot?");
                    break;
                //Другие ошибки
                default:
                    Console.WriteLine("Unhandled Exception");
                    break;
            }
        }
        //default конструктор
        public LoginException()
        {

        }
    }
    /// <summary>
    /// Exception class for Cirlce
    /// </summary>
    class CircleException : Exception
    {
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
        public CircleException(string msg)
            : base(msg)
        {
            Console.WriteLine("The point is outside of circle!");
        }
    }

    /// <summary>
    /// Circle class. Parametrs: Center_x, Center_y and radius
    /// </summary>
    class Circle
    {
        public double Center_x { get; set; }
        public double Center_y { get; set; }
        public double Radius { get; set; }

        //Constructors
        public Circle()
        {

        }
        public Circle(double x, double y, double r)
        {
            Center_x = x;
            Center_y = y;
            Radius = r;
        }
        /// <summary>
        /// Метод проверяет, находится ли точка в окружности
        /// </summary>
        /// <param name="x">координаты х точки</param>
        /// <param name="y">координаты y точки</param>
        /// <returns>Лежит ли точка в окружности</returns>
        public bool InCircle(double x, double y)
        {
            return ((Math.Sqrt(Math.Pow((x - Center_x), 2) + Math.Pow((y - Center_y), 2))) <= Radius);
        }
        /// <summary>
        /// Высчитывает длину окружности
        /// </summary>
        /// <returns>длина окружности</returns>
        public double Length()
        {
            return 2 * Math.PI * Radius;
        }
        /// <summary>
        /// Высчитывает площадь окружности
        /// </summary>
        /// <returns>площадь окружности</returns>
        public double Area()
        {
            return Math.PI * Radius * Radius;
        }
    }

    /// <summary>
    /// Класс содержит в себе массив Circle
    /// </summary>
    class Circles : IEnumerable, ICloneable, IComparable
    {
        public Circle[] circs = new Circle[5];

        public Circles()
        {
            circs[0] = new Circle(3, 4, 8);
            circs[1] = new Circle(0, 0, 1);
            circs[2] = new Circle(-2, -3, 4);
            circs[3] = new Circle(9, 10, 5);
            circs[4] = new Circle(2, 0, 7);
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < circs.Length; i++)
            {
                yield return circs[i];
            }
        }
        //реализация Clone
        public object Clone()
        {
            Circles cCircles = new Circles();
            for (int i = 0; i < this.circs.Length; i++)
            {
                cCircles.circs[i].Center_x = this.circs[i].Center_x;
                cCircles.circs[i].Center_y = this.circs[i].Center_y;
                cCircles.circs[i].Radius = this.circs[i].Radius;
            }
            return cCircles;
        }
        //сравнить по первому элементу массива, по сумме координат центра
        int IComparable.CompareTo(object obj)
        {
            Circles temp = obj as Circles;
            if (temp != null)
            {
                if (this.circs[0].Center_x + this.circs[0].Center_y > temp.circs[0].Center_x + temp.circs[0].Center_y)
                {
                    return 1;
                }
                if (this.circs[0].Center_x + this.circs[0].Center_y < temp.circs[0].Center_x + temp.circs[0].Center_y)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new ArgumentException("Object is not a Circles");
            }
        }
    }

    /// <summary>
    /// Класс состоит из вдух параметров: координаты x и y
    /// </summary>
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point()
        {

        }
        public Point(int a, int b)
        {
            this.X = a;
            this.Y = b;
        }
    }

    class PointComparer : IComparer<Point>
    {
        public int Compare(Point p1, Point p2)
        {
            if (p1 != null && p2 != null)
            {
                if ((p1.X + p1.Y) > (p2.X + p2.Y))
                {
                    return 1;
                }
                if ((p1.X + p1.Y) < (p2.X + p2.Y))
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new ArgumentException("Parameter is not a Point!");
            }
        }
    }

    /// <summary>
    /// Интерфейс для стэка
    /// </summary>
    interface IStack
    {
        int Pop();
        bool Push(int num);
    }
    /// <summary>
    /// Класс, который имплементирует интерфейс IStack
    /// </summary>
    class Stacky : IStack
    {
        public int index;
        public int [] elements = new int[10];
        public Stacky()
        {
            index = -1;
        }
        public bool Push(int num)
        {
            bool success = false;
            try
            {
                //увеличивает индекс и вводит новый элемент в стэк
                if (index > 8)
                {
                    throw new StackException("indexBig");
                }
                index++;
                elements[index] = num;
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                throw new StackException("indexBig");
            } 
            return success;
        }
        public int Pop()
        {
            int result = -1;
            try
            {
                //возвращает текущий элемент и уменьшает индекс
                if (index < 0)
                {
                    throw new StackException("indexSmall");
                }
                index--;
                result =  elements[index+1];
            }
            catch (Exception)
            {
                throw new StackException("indexSmall");
            }
            
            return result;
        }
    }

    /// <summary>
    /// Класс ошибок стэка
    /// </summary>
    class StackException : Exception
    {
        public StackException()
        {

        }
        public StackException(string msg)
        {
            switch (msg)
            {
                case "indexBig":
                    Console.WriteLine("Произошла ошибка, индекс вышел за массив!");
                    break;
                case "indexSmall":
                    Console.WriteLine("Произошла ошибка, индекс негативный");
                    break;
                default:
                    Console.WriteLine("Неопазнанная ошибка");
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Для запуска, уберите комментарии перед одним из Task

            //Exception Handling Task1
            //Task1();

            //Exception Handling Task2
            //Task2();

            //Exception Handling Task3
            //Task3();

            //Interfaces Task1
            //Task4();

            //Interfaces Task2
            Task5();
        }
        public static void Task1()
        {
            int[] arr = new int[] { -2, 9, 1, 0, 2, 4, -5, 7, -20, 23 };
            int input;
            Console.Write("Какой элемент вы хотите посмотреть: ");
            try
            {
                //Ввод индекса
                input = int.Parse(Console.ReadLine());
                //Вывод числа с таких индексом
                Console.WriteLine(arr[input - 1]);
            }
            //Exception когда индекс вне массива
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("такой индекс вне границ массива");
            }
            //Exception когда пользователь вводит не число
            catch (FormatException)
            {
                Console.WriteLine("Неправильный ввод. Нужно ввести целое число");
            }
        }
        public static void Task2()
        {
            //Создаем окружность и точку
            Circle circ = new Circle(2.0, 1.0, 5.0);
            double x = 7.0, y = -2.0;
            try
            {
                //Проверяем находится ли точка внутри окружности
                //Если да, то выводим сообщение
                //Если нет, то выкидываем кастомную ошибку
                if (circ.InCircle(x, y))
                {
                    Console.WriteLine("Point is circle, all OK!");
                }
                else
                {
                    throw new CircleException("Oops");
                }
            }
            //Обработка ошибки
            catch (Exception ex)
            {
                Console.WriteLine("Exception was caught! Message: {0}; \n\rTargetSite: {1}; \n\rStackTrace: {2}; \n\rInnerEx: {3}", ex.Message, ex.TargetSite, ex.StackTrace, ex.InnerException);
            }
            //В любом случае выводим длину и площадь окружности
            finally
            {
                Console.WriteLine("Circle has a perimeter {0:F2} and length {1:F2}", circ.Length(), circ.Area());
            }

        }
        public static void Task3()
        {
            //Создаем dictionary, где Key - имя пользователя, а Value - пароль 
            Dictionary<string, string> admins = new Dictionary<string, string>();

            //Добавляем трех админов в dictionary
            admins["Daulet"] = "temppassword";
            admins["Sattar"] = "1234";
            admins["Ivan"] = "pass123";

            //Каптча будет массивом строк. Каждый раз случайным образом будет выбираться одна строка
            string[] captcha = new string[] { "rewea", "432p", "nm291", "poe9", "mc782", "shw2", "mcnz82", "o3u2em" };
            Random rnd = new Random();

            string user;
            string pass;
            string capt;

            //У пользователя 3 попытки ввести логин и пароль
            for (int i = 0; i < 3; i++)
            {
                //Считываем ввод: логин, пароль и каптчу
                Console.Write("user: ");
                user = Console.ReadLine();

                Console.Write("password: ");
                pass = Console.ReadLine();

                //Вывод случайной каптчи
                string randomCapt = captcha[rnd.Next(captcha.Length)];
                Console.Write("captcha: {0}\t: ", randomCapt);
                capt = Console.ReadLine();

                try
                {
                    //Проверка на правильность пароля
                    //Если  такой админ не существуйт, выйдет KeyNotFoundException
                    if (admins[user] == pass)
                    {
                        if (capt == randomCapt)
                        {
                            //каптча и другие данные правильные
                            Console.WriteLine("Вы залогинились!");
                            //выход из цикла
                            break;
                        }
                        else
                        {
                            //Выкинуть ошибку о неправильной каптче
                            throw new LoginException("captcha");
                        }
                    }
                    //Если пользователь существует, но неправильный пароль
                    else
                    {
                        //Выкинуть ошибку о неправльном пароле
                        throw new LoginException("password");
                    }
                }
                //Поймать ошибку когда нет такого пользователя
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("No such user. Please try again...");
                }
                //Поймать ошибку типа LoginException, когда неправильный пароль или каптча
                catch (LoginException)
                {
                    Console.WriteLine("There was an error. Please try again...");
                }
            }
        }
        public static void Task4()
        {
            // (a) Cirlces array of Circle and implements IEnumerable
            Circles circleArray = new Circles();
            foreach (Circle circle in circleArray)
            {
                Console.WriteLine("Radius is: {0}", circle.Radius);
            }
            // (b) Class Cicle implements ICloneable interface
            Circles circles = new Circles();
            Circles cloneCircles = (Circles)circles.Clone();
            cloneCircles.circs[0].Center_x = 10;
            cloneCircles.circs[0].Center_y = -2;
            Console.WriteLine("Circles coordinates: {0}, {1}; Clone coordinates: {2}, {3}", circles.circs[0].Center_x, circles.circs[0].Center_y, cloneCircles.circs[0].Center_x, cloneCircles.circs[0].Center_y);

            Circles circles2 = new Circles();
            circles2.circs[0].Center_x = 1;
            circles2.circs[0].Center_y = 2;
            // (c) создание массива с этими экземплярами и еще одним. Затем сортировка массива (IComparable)
            Circles[] circlesArray = new Circles[3] 
            {
                circles, cloneCircles, circles2
            };
            Array.Sort(circlesArray);
            Console.WriteLine("Sorted array: {0}, {1}; {2}, {3}; {4}, {5}", circlesArray[0].circs[0].Center_x, circlesArray[0].circs[0].Center_y, circlesArray[1].circs[0].Center_x, circlesArray[1].circs[0].Center_y, circlesArray[2].circs[0].Center_x, circlesArray[2].circs[0].Center_y);

            // (d) Implement a strongly types interface to compare the two points. Then create a list of points and sort them
            List<Point> pointList = new List<Point>();
            pointList.Add(new Point(1, 2));
            pointList.Add(new Point(-1, 0));
            pointList.Add(new Point(-5, -3));
            pointList.Add(new Point(4, -4));
            pointList.Sort(new PointComparer());

            foreach (Point p in pointList)
            {
                Console.WriteLine("Point has coordinates x = {0}, y = {1}", p.X, p.Y);
            }

        }
        public static void Task5()
        {
            Stacky stck = new Stacky();


            //Push в стэк четные числа
            for (int i = 0; i < 12; i++)
            {
                //Если ошибка, обработать ошибку типа StackException
                try
                {
                    stck.Push(i * 2);
                }
                catch (StackException e)
                {
                    Console.WriteLine("Поймал ошибку типа StackException!");
                }
            }
          
            for (int i = 0; i < 13; i++)
            {
                //Пробуем еще Pop элемент. Если стэк пустой обробатываем ошибку типа StackException
                try
                {
                    Console.WriteLine("Pop element: {0}", stck.Pop());
                }
                catch (StackException e)
                {
                    Console.WriteLine("Поймал ошибку типа StackException!");
                }
            }
        }
    }
}
