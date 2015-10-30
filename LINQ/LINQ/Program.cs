using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace LINQ
{
    internal class Program
    {
        /* Practice your LINQ!
         * You can use the methods in Data Loader to load products, customers, and some sample numbers
         * 
         * NumbersA, NumbersB, and NumbersC contain some ints
         * 
         * The product data is flat, with just product information
         * 
         * The customer data is hierarchical as customers have zero to many orders
         */

        private static void Main()
        {
            //problem1();
            //problem2();
            //problem3();
            //problem4();
            //problem5();
            //problem6();
            //problem7();
            //problem8();
            //problem9();
            //problem10();
            //problem11();
            //problem12();
            //problem13();
            //problem14();
            //problem15();
            //problem16();
            //problem17();
            //problem18();
            //problem19();
            //problem20();
            //problem21();
            //problem22();
            //problem23();
            //problem24();
            //problem25();
            //problem26();
            //problem27();
            //problem28();
            //problem29();
            //problem30();
            //problem31();
            //problem32();
            //problem33();
            //problem34();
            //problem35();
            //problem36();
            //problem37();
            //problem38();
            problem39();
            //problem40();

            Console.ReadLine();
        }
        //1. Find all products that are out of stock.
        private static void problem1()
        {
            var products = DataLoader.LoadProducts();
            
            var results = from p in products
                where p.UnitsInStock == 0
                select p;

            foreach (var r in results)
            {
                Console.WriteLine("{0} has {1} units in stock.", r.ProductName, r.UnitsInStock);
            }
        }

        //2. Find all products that are in stock and cost more than 3.00 per unit.
        private static void problem2()
        {
            var products = DataLoader.LoadProducts();
            
            var results = from p in products
                where p.UnitsInStock > 0 && p.UnitPrice > 3
                select p;

            foreach (var r in results)
            {
                Console.WriteLine("{0} has {1} units in stock with unit price {2:c}", r.ProductName,
                    r.UnitsInStock, r.UnitPrice);
            }
        }

        //3. Find all customers in Washington, print their name then their orders. (Region == "WA")
        private static void problem3()
        {
            var customers = DataLoader.LoadCustomers();

            var results = from c in customers
                where c.Region == "WA"
                select new
                {
                    Name = c.CustomerID,
                    c.Region,
                    Orders = from o in c.Orders
                        orderby o.OrderID
                        select new {ID = o.OrderID, Date = o.OrderDate}

                };

            foreach (var r in results)
            {
                Console.WriteLine("{0} from {1} has the following orders:", r.Name, r.Region);

                foreach (var o in r.Orders)
                {
                    Console.WriteLine("\t{0} -- {1:d}", o.ID, o.Date);
                }
            }
        }

        //4. Create a new sequence with just the names of the products.
        private static void problem4()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.ProductName
                select new {p.ProductName};

            foreach (var r in results)
            {
                Console.WriteLine(r.ProductName);
            }
        }

        //5. Create a new sequence of products and unit prices where the unit prices are increased by 25%.
        private static void problem5()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                select new {NewPrice = p.UnitPrice*1.25m, p.ProductName, OldPrice = p.UnitPrice};

            foreach (var r in results)
            {
                Console.WriteLine("{0} - Old price: {1:C}. New price: {2:C}", r.ProductName, r.OldPrice,
                    r.NewPrice);

            }
        }

        //6. Create a new sequence of just product names in all upper case.
        private static void problem6()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                select new {Name = p.ProductName.ToUpper()};

            foreach (var r in results)
            {
                Console.WriteLine("{0}", r.Name);
            }
        }

        //7. Create a new sequence with products with even numbers of units in stock.
        private static void problem7()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                where p.UnitsInStock%2 == 0
                select p;

            foreach (var r in results)
            {
                Console.WriteLine("{0} has {1} units in stock", r.ProductName, r.UnitsInStock);
            }
        }

        //8. Create a new sequence of products with ProductName, Category, and rename UnitPrice to Price.
        //private static void 
        private static void problem8()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                select new {p.ProductName, p.Category, Price = p.UnitPrice};

            foreach (var r in results)
            {
                Console.WriteLine("{0}({1}) costs {2:C} per unit.", r.ProductName, r.Category,
                    r.Price);
            }
        }

        //9. Make a query that returns all pairs of numbers from both arrays such that the number from numbersB is less than the number from numbersC.
        private static void problem9()
        {
            var arrayB = DataLoader.NumbersB;
            var arrayC = DataLoader.NumbersC;

            var results = from b in arrayB
                from c in arrayC
                where b < c
                select new {b, c};

            foreach (var r in results)
            {
                Console.WriteLine("From B: {0} < From C: {1}", r.b, r.c);
            }

        }

        //10. Select CustomerID, OrderID, and Total where the order total is less than 500.00.
        private static void problem10()
        {
            var customers = DataLoader.LoadCustomers();

            var results = from c in customers
                from o in c.Orders
                where o.Total < 500.00m
                select new {c.CustomerID, o.OrderID, o.Total};

            foreach (var r in results)
            {
                Console.WriteLine("Customer ID: {0} - Order ID: {1} - Order Tota: {2}", r.CustomerID,
                    r.OrderID, r.Total);
            }

        }

        //11. Write a query to take only the first 3 elements from NumbersA.
        private static void problem11()
        {
            var arrayA = DataLoader.NumbersA;

            var results = arrayA.Take(3);

            Console.WriteLine("The original array:");
            foreach (var n in arrayA)
            {
                Console.Write("{0} ", n);
            }

            Console.WriteLine("\nArray with only the first three elements:");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }

        }

        //12. Get only the first 3 orders from customers in Washington.
        private static void problem12()
        {
            var customers = DataLoader.LoadCustomers();


            var results = (from c in customers
                from o in c.Orders
                where c.Region == "WA"
                orderby o.OrderDate
                select new {c, o}).Take(3);

            foreach (var r in results)
            {
                Console.WriteLine("{0} -- {1} -- {2}", r.c.CustomerID, r.o.OrderID, r.o.OrderDate);
            }
        }

        //13. Skip the first 3 elements of NumbersA.
        private static void problem13()
        {
            var arrayA = DataLoader.NumbersA;

            var results = arrayA.Skip(3);
            Console.WriteLine("Original array:");
            foreach (var n in arrayA)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine("\nArray without the first three elements:");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }
        }

        //14. Get all except the first two orders from customers in Washington.
        private static void problem14()
        {
            var customers = DataLoader.LoadCustomers();

            var results = (from c in customers
                from o in c.Orders
                where c.Region == "WA"
                orderby o.OrderDate
                select new {c, o}).Skip(2);

            foreach (var r in results)
            {
                Console.WriteLine("{0} -- {1} -- {2}", r.c.CustomerID, r.o.OrderID, r.o.OrderDate);
            }

        }

        //15. Get all the elements in NumbersC from the beginning until an element is greater or equal to 6.
        private static void problem15()
        {
            var arrayC = DataLoader.NumbersC;

            var results = arrayC.TakeWhile(x => x <= 6);

            Console.WriteLine("The original array:");
            foreach (var n in arrayC)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine("\tArray which ends at the element that is less 7:");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }
        }

        //16. Return elements starting from the beginning of NumbersC until a number is hit that is less than its position in the array.
        private static void problem16()
        {
            int[] arrayC = DataLoader.NumbersC;

            var results = arrayC.TakeWhile((x, index) => x >= index);

            Console.WriteLine("The original array:");
            foreach (var n in arrayC)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine("\nArray that ends at the element that is less than its index:");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }
        }

        //17. Return elements from NumbersC starting from the first element divisible by 3.
        private static void problem17()
        {
            var arrayC = DataLoader.NumbersC;

            var results = arrayC.SkipWhile(x => x%3 != 0);

            Console.WriteLine("The original array: ");
            foreach (var n in arrayC)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine("\nArray beginning at the first element divisible by 3: ");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }

        }

        //18. Order products alphabetically by name.
        private static void problem18()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.ProductName
                select p;

            foreach (var r in results)
            {
                Console.WriteLine(r.ProductName);
            }
        }
        //19. Order products by UnitsInStock descending.
        private static void problem19()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.UnitsInStock descending
                select p;

            foreach (var r in results)
            {
                Console.WriteLine("Product: {0}, Inventory: {1}", r.ProductName, r.UnitsInStock);
            }
        }

        //20. Sort the list of products, first by category, and then by unit price, from highest to lowest.
        private static void problem20()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.Category, p.UnitPrice descending
                select p;

            foreach (var r in results)
            {
                Console.WriteLine("\tProduct Name: {0}, Category: {1}, Price: {2:C}", r.ProductName,
                        r.Category, r.UnitPrice);
            }
        }

        //21. Reverse NumbersC.
        private static void problem21()
        {
            var numbers = DataLoader.NumbersC;

            var results = numbers.Reverse();

            Console.WriteLine("Original array:");
            foreach (var n in numbers)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine("\nReversed array:");
            foreach (var r in results)
            {
                Console.Write("{0} ", r);
            }

        }

        //22. Display the elements of NumbersC grouped by their remainder when divided by 5.
        private static void problem22()
        {
            var numbersC = DataLoader.NumbersC;

            var results = from c in numbersC
                group c by c%5
                into g
                orderby g.Key
                select g;

            foreach (var r in results)
            {
                Console.WriteLine("Numbers who's remainder equals {0} when divided by 5: ", r.Key);
                foreach (var n in r)
                {
                    Console.WriteLine("\t{0}", n);
                }
            }
        }

        //23. Display products by Category.
        private static void problem23()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                group p by p.Category
                into category
                select category;

            foreach (var r in results)
            {
                Console.WriteLine(r.Key);

                foreach (var p in r)
                {
                    Console.WriteLine("\t{0}", p.ProductName);
                }
            }
        }

        //24. Group customer orders by year, then by month.
        private static void problem24()
        {
            var customers = DataLoader.LoadCustomers();

            var results = from c in customers
                select new
                {
                    CustomerName = c.CustomerID,
                    Years =
                        from o in c.Orders
                        group o by o.OrderDate.Year
                        into year
                        select new
                        {
                            Year = year.Key,
                            Months = from o in year
                                group o by o.OrderDate.Month
                                into month
                                select new {Month = month.Key, month}
                        }
                };

            foreach (var r in results)
            {
                Console.WriteLine(r.CustomerName);

                foreach (var y in r.Years)
                {
                    Console.WriteLine("\t" + y.Year);

                    foreach (var m in y.Months)
                    {
                        Console.WriteLine("\t\t{0}", m.Month);

                        foreach (var o in m.month)
                        {
                            Console.WriteLine("\t\t\t{0:D} -- Order ID: {1}", o.OrderDate, o.OrderID);
                        }
                    }
                }
            }
        }

        //25. Create a list of unique product category names.
        private static void problem25()
        {
            var products = DataLoader.LoadProducts();

            var results = (from p in products
                select p.Category).Distinct();

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
        }
        //26. Get a list of unique values from NumbersA and NumbersB.
        private static void problem26()
        {
            var numbersA = DataLoader.NumbersA;
            var numbersB = DataLoader.NumbersB;

            var results = numbersA.Union(numbersB);

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
        }
        //27. Get a list of the shared values from NumbersA and NumbersB.
        private static void problem27()
        {
            var numbersA = DataLoader.NumbersA;
            var numbersB = DataLoader.NumbersB;

            var results = numbersA.Intersect(numbersB);

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
        }
        //28. Get a list of values in NumbersA that are not also in NumbersB.
        private static void problem28()
        {
            var numbersA = DataLoader.NumbersA;
            var numbersB = DataLoader.NumbersB;

            var results = numbersA.Except(numbersB);

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
        }
        //29. Select only the first product with ProductID = 12(not a list).
        private static void problem29()
        {
            var products = DataLoader.LoadProducts();

            var result = products.First(p => p.ProductID == 12);

            Console.WriteLine("Product Name: {0}, Product ID: {1}", result.ProductName, result.ProductID);
        }
        //30. Write code to check if ProductID 789 exists.
        private static void problem30()
        {
            var products = DataLoader.LoadProducts();

            var result = products.FirstOrDefault(p => p.ProductID == 789);

            if (result == null)
            {
                Console.Write("No such product");
            }
            else
            {
                Console.Write("Product Name: {0}, Product ID: {1}", result.ProductName, result.ProductID);
            }

        }
        //31. Get a list of categories that have at least one product out of stock.
        private static void problem31()
        {
            var products = DataLoader.LoadProducts();

            var results = products.Where(p => p.UnitsInStock == 0).GroupBy(p => p.Category);

            foreach (var group in results)
            {
                Console.WriteLine(group.Key);
            }
        }
        //32. Determine if NumbersB contains only numbers less than 9.
        private static void problem32()
        {
            var numbersB = DataLoader.NumbersB;

            var result = numbersB.All(n=> n < 9);

            if (result)
            {
                Console.Write("All are less than 9");
            }
            else
            {
                Console.Write("There was at least one greater than or equal to 9");
            }
        }
        //33. Get a grouped a list of products only for categories that have all of their products in stock.
        private static void problem33()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.UnitsInStock
                group p by p.Category
                into category
                where category.All(p=>p.UnitsInStock > 0)
                select category;

            foreach (var r in results)
            {
                Console.WriteLine("{0} has all of it's products in stock.", r.Key);
                Console.WriteLine("Here are its products:");

                foreach (var p in r)
                {
                    Console.WriteLine(p.ProductName);
                }

                Console.WriteLine();
            }
        }
        //34. Count the number of odds in NumbersA.
        private static void problem34()
        {
            var numbersA = DataLoader.NumbersA;

            var result = numbersA.Where(n => n%2 == 1).Count();

            Console.WriteLine("There are {0} odd numbers in Array A", result);
        }
        //35. Display a list of CustomerIDs and only the count of their orders.
        private static void problem35()
        {
            var customers = DataLoader.LoadCustomers();

            var result = from c in customers
                select new {CustID = c.CustomerID, NumerOfOrders = c.Orders.Length};

            foreach (var r in result)
            {
                Console.WriteLine("Customer ID: {0}", r.CustID);
                Console.WriteLine("Order count: {0}\n", r.NumerOfOrders);
            }
        }
        //36. Display a list of categories and the count of their products.
        private static void problem36()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                group p by p.Category
                into category
                select new {Category = category.Key, TotalProducts = category.Count()};

            foreach (var r in results)
            {
                Console.WriteLine("Category: {0}", r.Category);
                Console.WriteLine("Total Number of Products: {0}\n", r.TotalProducts);
            }

        }
        //37. Display the total units in stock for each category.
        private static void problem37()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                group p by p.Category
                into category
                select new {Category = category.Key, TotalInStock = category.Sum(p => p.UnitsInStock)};

            foreach (var r in results)
            {
                Console.WriteLine("Category: {0}", r.Category);
                Console.WriteLine("Total units in stock: {0}\n", r.TotalInStock);
            }
        }
        
        
        //38. Display the lowest priced product in each category.
        private static void problem38()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                orderby p.UnitPrice ascending
                group p by p.Category
                into category
                select
                    new
                    {
                        Category = category.Key,
                        ProductNam = category.First().ProductName,
                        ProductPrice = category.First().UnitPrice
                    };

            foreach (var r in results)
            {
                Console.WriteLine("Category: {0}", r.Category);
                Console.WriteLine("Cheapest Product: {0}", r.ProductNam);
                Console.WriteLine("Product Price: {0:c}\n", r.ProductPrice);
            }
        }

        //39. Display the highest priced product in each category.
        private static void problem39()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                          orderby p.UnitPrice descending
                          group p by p.Category
                into category
                
                select
                    new
                    {
                        Category = category.Key,
                        ProductNam = category.First().ProductName,
                        ProductPrice = category.First().UnitPrice
                    };

            foreach (var r in results)
            {
                Console.WriteLine("Category: {0}", r.Category);
                Console.WriteLine("Most Expensive Product: {0}", r.ProductNam);
                Console.WriteLine("Product Price: {0:c}\n", r.ProductPrice);
            }
        }

        //40. Show the average price of product for each category.
        private static void problem40()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                group p by p.Category
                into category
                select new {Category = category.Key, AveragePrice = category.Average(product => product.UnitPrice)};

            foreach (var r in results)
            {
                Console.WriteLine("Category: {0}", r.Category);
                Console.WriteLine("Average Price: {0:c}\n", r.AveragePrice);
            }
        }
    }
}
