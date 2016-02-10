using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab4
{
    /// <summary>
    /// Exception class for Cirlce
    /// </summary>
    class CircleException : Exception
    {
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
    /// Exception class for Task3 - User login
    /// </summary>
    class LoginException : Exception
    {
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
            Task3();
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
    }
}
