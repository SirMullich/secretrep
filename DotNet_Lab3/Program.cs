using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet_lab2
{
    public enum Genders { Males, Females };
    class Person
    {
        public string firstName;
        public string lastName;
        public int age;
        public Genders gender;
        public Person()
        {

        }
        public Person(string name, string lname, int years)
        {
            firstName = name;
            lastName = lname;
            age = years;
        }
        public Person(string name, string lname, int years, Genders gen)
            :this(name, lname, years)
        {
            gender = gen;
        }
        public override string ToString()
        {
            return firstName + " " + lastName + " is " + age + " ages old and is a " + gender;
        }
    }
    class SubjectScores 
    {
        public string name;
        public int credits;
        public int score;
        public SubjectScores() 
        {

        }
        public int scoreToGrade() 
        {
            if ((score >= 90) && (score <= 100)) return 4;
            if (score >= 80) return 3;
            if (score >= 70) return 2;
            if (score >= 60) return 1;
            else return 0;
        }
    }
    class Student : Person
    {
        List<SubjectScores> subj = new List<SubjectScores>();
        public void AddSubject(SubjectScores s) 
        {
            subj.Add(s);
        }
        public double calculateGPA()
        {
            int creditSum = 0;
            int totalScore = 0;
            foreach (SubjectScores s in subj)
            {
                creditSum = creditSum + s.credits;
                totalScore = totalScore + s.credits * s.scoreToGrade();
            }
            return (double)totalScore / creditSum;
        }
    }
    class Worker : Person
    {
        public int Salary { get; set; }
        public int HoursWorked { get; set; }
        public Worker()
        {

        }
        public Worker(string name, string lname, int years, Genders gen, int sal, int hours)
        {
            firstName = name;
            lastName = lname;
            age = years;
            gender = gen;
            Salary = sal;
            HoursWorked = hours;
        }
        public double HourlySalary()
        {
            return (double)Salary / HoursWorked;
        }
        public double MonthlySalary()
        {
            return ((double)Salary / HoursWorked) * 160;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2_1();
            Task2_2();
            //Task3();
        }
        static void Task1()
        {
            //task 1
            Person pers = new Person()
            {
                firstName = "Ivan",
                lastName = "Ivanov",
                age = 21,
                gender = Genders.Males
            };
            Console.WriteLine(pers.ToString());
        }
        static void Task2_1()
        {
            Student Sattar = new Student() 
            {
                firstName = "Sattar", lastName = "Salambayev", age = 20, gender = Genders.Males
            };
            //Создаём предметы
            SubjectScores s1 = new SubjectScores()
            {
                name = ".Net",
                credits = 3,
                score = 96
            };
            SubjectScores s2 = new SubjectScores()
            {
                name = "Java Programming",
                credits = 2,
                score = 82
            };
            SubjectScores s3 = new SubjectScores()
            {
                name = "Philosophy",
                credits = 3,
                score = 82
            };
            SubjectScores s4 = new SubjectScores()
            {
                name = "Discrete Math",
                credits = 4,
                score = 91
            };

            //Добавляем предметы
            Sattar.AddSubject(s1);
            Sattar.AddSubject(s2);
            Sattar.AddSubject(s3);
            Sattar.AddSubject(s4);

            Console.WriteLine("{0} and has GPA: {1}", Sattar.ToString(), Sattar.calculateGPA());
        }
        static void Task2_2()
        {
            Worker worker = new Worker("Ivan", "Ivanovich", 42, Genders.Males, 5000, 3);
            Console.WriteLine("{0} works has hourly salary: {1}, and monthly salary: {2}", worker.ToString(), worker.HourlySalary(), worker.MonthlySalary());
        }
        static void Task3()
        {
            Int16 i1 = new Int16();
            i1 = 20;
            Int32 i2 = new Int32();
            i2 = 28;
            Int32 i3 = new Int32();
            i3 = 1;
            double i4 = new double();
            i4 = 4.11;

            Console.WriteLine("{0} \t {1} \t {2} \t {3}", i1, i2, i3, i4);

            Int32 i32 = i1;
            //Int16 i16 = i2;
            //Int16 i16 = i4;
            double d = i1;
        }
    }
}
