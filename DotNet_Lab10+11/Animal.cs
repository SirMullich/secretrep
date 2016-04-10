using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DotNet_Lab10_11
{
    abstract class Animal
    {
        public int Legs { get; set; }
        public int Size { get; set; }
        
        abstract public string MakeSound();
    }

    class Lion : Animal
    {
        public string SkinColor { get; set; }
        
        public Lion()
        {
            Legs = 4;
            Size = 10;
            SkinColor = "Yellow";
        }
        public override string MakeSound()
        {
            return "Rghfhrhghgh!!! I'm a Lion";
        }
        public string Kill()
        {
            return "Killing!";
        }
    }

    class Elephant : Animal
    {
        public string Trunk { get; set; }
        public Elephant()
        {
            Legs = 4;
            Size = 50;
            Trunk = "I have a trunk";
        }
        public override string MakeSound()
        {
            return "Muahahauahahaa!!! I'm an elephant";
        }
    }

    class Monkey : Animal
    {
        public Monkey()
        {
            Legs = 2;
            Size = 5;
        }
        public override string MakeSound()
        {
            return "Wo-wo-oo-oa-aha-aaa! I'm a monkey";
        }
        public string Play()
        {
            return "I'm playing!";
        }
    }

    class Zoo
    {
        public void GetSound<T>()
        {
            Type t = typeof(T);
            MethodInfo[] mi = t.GetMethods();
            Console.WriteLine("The {0} has the following methods: ", t.FullName);
            foreach (var method in mi)
            {
                Console.WriteLine("{0} returns {1}", method.Name, method.ReturnType);
            }

            Console.WriteLine("{0}The {1} has the following properties: ", Environment.NewLine, t.FullName);

            PropertyInfo[] pi = t.GetProperties();
            foreach (var property in pi)
            {
                Console.WriteLine("Property {0} is of type {1}", property.Name, property.PropertyType);
            }

            Console.WriteLine("{0}Invoking methods of {1}: ", Environment.NewLine, t.FullName);

            foreach (MethodInfo method in mi)
            {
                if (!(method.Name.StartsWith("s") || method.Name.StartsWith("g") || method.Name.StartsWith("Get") || method.Name.StartsWith("Equal") || method.Name.StartsWith("ToStrin")))
                {
                    //Console.WriteLine(method.Name);
                    Console.WriteLine(method.Invoke(Activator.CreateInstance(t), null));
                }
            }
            Console.WriteLine();
        }
    }
}
