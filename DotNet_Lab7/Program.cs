using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab7
{
    // used in Ex1 
    class Cloth
    {
        public string category;
        public int price;

        public override string ToString()
        { 
            return string.Format("Category = {0}, Price = {1}", category, price);
        }
    }

    // used in Ex2
    class Employee
    {
        public int id;
        public string name;
        public int bonus;
        public DateTime date;
    }

    // used in Ex3
    class Author
    {
        public int id;
        public string name;
        public string surname;
    }

    class Book
    {
        public int authorId;
        public string title;
        public int year;
        
    }

    // used in Ex4
    public class Product
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
    class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Ex1();
            //Ex2();
            //Ex3();
            Ex4();
            //Ex5();
        }

        static void Ex1()
        {
            Random rnd = new Random();
            List<Cloth> clothes = new List<Cloth> { 
                new Cloth { category = "Shoes for Men", price = 20354},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},
                new Cloth { category = "Shoes for Men", price = rnd.Next(1200, 30000)},

                new Cloth { category = "T-shirts", price = rnd.Next(1200, 20000)},
                new Cloth { category = "T-shirts", price = rnd.Next(1200, 20000)},
                new Cloth { category = "T-shirts", price = rnd.Next(1200, 20000)},
                new Cloth { category = "T-shirts", price = rnd.Next(1200, 20000)},

                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)},
                new Cloth { category = "Shoes for Women", price = rnd.Next(1200, 40000)}
            };

            var expensiveShoes = from shoe in clothes 
                                 where shoe.category.Contains("Shoe")
                                 orderby shoe.price descending 
                                 select shoe;

            foreach (var cloth in expensiveShoes.Take(10))
            {
                Console.WriteLine(cloth);
            }
        }

        static void Ex2()
        {
            List<Employee> payments = new List<Employee> { 
                new Employee {id = 1, name = "Jane", bonus = 200, date = new DateTime(2014, 01, 01)}, 
                new Employee {id = 1, name = "Jane", bonus = 300, date = new DateTime(2014, 05, 01)}, 
                new Employee {id = 1, name = "Jane", bonus = 150, date = new DateTime(2015, 01, 01)}, 
                new Employee {id = 1, name = "Bob", bonus = 500, date = new DateTime(2014, 01, 01)}, 
            };

            // Task 1
            var bonusesByYear = from empl in payments
                                group empl by new {empl.name, empl.date.Year} into entry
                                select new { entry.Key, sum = entry.Sum(s => s.bonus) };
            foreach (var person in bonusesByYear)
            {
                Console.WriteLine("{0} - {1} in {2}", person.Key.name, person.sum, person.Key.Year);
            }

            // Task 2
            Console.WriteLine("\n***   Exercise A   ***");
            var query2 = from empl in payments
                         group empl by new { empl.name, empl.date.Year } into entry
                         select new { entry.Key, times = entry.Count() };
            foreach (var person in query2)  
            {
                Console.WriteLine("{0} - {1} times in {2}", person.Key.name, person.times, person.Key.Year);
            }
        }

        static void Ex3()
        {
            List<Author> authorList = new List<Author> { 
                new Author { id = 0, name = "Jane", surname = "Austen" }, 
                new Author { id = 1, name = "Theodore", surname = "Dreiser" }, 
                new Author { id = 2, name = "Douglas", surname = "Adams" }, 
                new Author { id = 3, name = "Abai", surname = "Kunanbaiuli" }
            };

            List<Book> bookList = new List<Book> { 
                new Book { authorId = 0, title = "Sense and Sensibility", year = 1811 }, 
                new Book { authorId = 0, title = "Lady Susan", year = 1794 }, 
                new Book { authorId = 0, title = "Pride and Prejudice", year = 1813 }, 
                new Book { authorId = 0, title = "Emma", year = 1815 }, 

                new Book { authorId = 1, title = "Sister Carrie", year = 1900 }, 
                new Book { authorId = 1, title = "Titan", year = 1914 }, 
                new Book { authorId = 1, title = "Genius", year = 1915 }, 

                new Book { authorId = 2, title = "The Hitchhiker's Guide to the Galaxy", year = 1978 }, 
                new Book { authorId = 2, title = "Dirk Gently", year = 1985 }, 
                new Book { authorId = 2, title = "Doctor Who", year = 1978 }, 

                new Book { authorId = 2, title = "The Book of Words", year = 1979 }, 
                new Book { authorId = 2, title = "Толык жинак", year = 1933 } 
            };

            var query = bookList.SelectMany(book => authorList.Where(a => a.id == book.authorId)
                                            .Select(a => new { name = a.name, lastname = a.surname, title = book.title, year = book.year}));

            foreach (var result in query)
            {
                Console.WriteLine("{0} {1} {2} {3}", result.name, result.lastname, result.title, result.year);
            }
        }

        static void Ex4()
        {
            List<Category> categories = new List<Category>()
            { 
                new Category(){Name="Beverages", ID=1},
                new Category(){ Name="Condiments", ID=2},
                new Category(){ Name="Vegetables", ID=3},
                new Category(){ Name="Grains", ID=4},
                new Category(){ Name="Fruit", ID=5}            
            };

            List<Product> products = new List<Product>() 
            {
              new Product{Name="Cola",  CategoryID=1},
              new Product{Name="Tea",  CategoryID=1},
              new Product{Name="Mustard", CategoryID=2},
              new Product{Name="Pickles", CategoryID=2},
              new Product{Name="Carrots", CategoryID=3},
              new Product{Name="Bok Choy", CategoryID=3},
              new Product{Name="Peaches", CategoryID=5},
              new Product{Name="Melons", CategoryID=5},
            };

            // Inner Join
            var query1 = from prod in products
                            join cat in categories
                            on prod.CategoryID equals cat.ID
                            select new { ProdName = prod.Name, CategoryName = cat.Name };
            foreach (var n in query1)
            {
                Console.WriteLine("{0} - {1}", n.ProdName, n.CategoryName);
            }
            Console.WriteLine();

            // Outer Join
            var query2 = from cat in categories
                         join prod in products on cat.ID equals prod.CategoryID into jn
                         from result in jn.DefaultIfEmpty()
                         select new { catName = cat.Name, prodName = result == null ? "(No products)" : result.Name };

            //foreach (var item in query2)
            //{
            //    Console.WriteLine("{0} - {1}", item.catName, item.prodName);
            //}

            var query3 = from s in query2
                         group s by s.catName into gr
                         select new { categName = gr.Key, 
                                      prodNames = from s2 in query2 
                                                  where s2.catName == gr.Key 
                                                  select s2
                         };
            foreach (var item in query3)
            {
                Console.WriteLine("Category: {0}", item.categName);

                foreach (var prodName in item.prodNames)
                {
                    Console.WriteLine(prodName.prodName);
                }
            }
        }

        static void Ex5()
        {
            List<Author> authorList = new List<Author> { 
                new Author { id = 0, name = "Jane", surname = "Austen" }, 
                new Author { id = 1, name = "Theodore", surname = "Dreiser" }, 
                new Author { id = 2, name = "Douglas", surname = "Adams" }, 
                new Author { id = 3, name = "Abai", surname = "Kunanbaiuli" }
            };

            // Deferred Execution
            var queryDeferred = from auth in authorList
                        where auth.name.Length > 4
                        select auth;

            // Names shorter than 4
            foreach (var i in queryDeferred)
            {
                Console.WriteLine("{0} {1}", i.name, i.surname);
            }

            authorList.Add(new Author { id = 4, name = "Stephen", surname = "King" });

            Console.WriteLine();
            foreach (var i in queryDeferred)
            {
                Console.WriteLine("{0} {1}", i.name, i.surname);
            }

            //Immediate execution
            var arr1 = (from auth in authorList
                         where auth.name.Length > 4
                         select auth).ToList();
            Console.WriteLine();
            foreach (var i in arr1)
            {
                Console.WriteLine("{0} {1}", i.name, i.surname);
            }

            authorList.Add(new Author { id = 5, name = "Charles", surname = "Dickens" });

            Console.WriteLine();
            foreach (var i in arr1)
            {
                Console.WriteLine("{0} {1}", i.name, i.surname);
            }
        }
    }
}
