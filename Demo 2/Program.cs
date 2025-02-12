using Demo_2.Data;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using static Demo_2.Classes.ListGenerators;
using System.Text.RegularExpressions;
namespace Demo_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Casting [Conversion] Operators - Immediate Execution
            #region Tolist()

            List<Product> Result = productsList.Where(P => P.UnitsInStock == 0).ToList();

            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion

            #region ToArray()
            Product[] Result = productsList.Where(P => P.UnitsInStock == 0).ToArray();

            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion

            #region ToDictionary()
            Dictionary<long, Product> Result = productsList.Where(P => P.UnitsInStock == 0)
                                                           .ToDictionary(P => P.ProductID);


            Dictionary<long, string> Result = productsList.Where(P => P.UnitsInStock == 0)
                                                           .ToDictionary(P => P.ProductID, P => P.ProductName);
            foreach (var item in Result)
                Console.WriteLine($"key = {item.Key} , value = {item.Value}");
            #endregion

            #region ToHashSet()

            HashSet<Product> Result = productsList.Where(P => P.UnitsInStock == 0)
                                                  .ToHashSet();

            foreach (var item in Result)
                Console.WriteLine(item);

            #endregion

            #region OfType()

            ArrayList obj = new ArrayList()
            {
                "Ahmed",
                "Mohamed",
                "Mostafa",
                1,
                2,
                3
            };

            var Result = obj.OfType<long>();
            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion
            #endregion

            #region Generation Operators - Deferred Execution

            //Valid With Fluent Syntax Only
            //The Only Way To Call Them is As Static Methods from Enumerable Class

            #region Range()
            var Result = Enumerable.Range(0, 100); //0 .. 99

            foreach (var item in Result)
                Console.Write($"{item} ");
            #endregion

            #region Repeat()
            var Result = Enumerable.Repeat(2, 100);
            //It will Return IEnumerable of 100 Elements , each one = 2

            var Result = Enumerable.Repeat(new Product(), 100);
            //It will Return IEnumerable of 100 Products

            foreach (var item in Result)
                Console.Write($"{item} ");
            #endregion

            #region Empty()

            var Result = Enumerable.Empty<Product>().ToArray();
            Product[] Result = new Product[0];

            //Both will Generate an empty Array of products

            var Result = Enumerable.Empty<Product>().ToList();
            List<Product> Result = new List<Product>();

            //Both will Generate an empty list of products


            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion
            #endregion

            #region Set Operators [Union Family] - Deferred Execution

            var Seq1 = Enumerable.Range(0, 100); //0 .. 99
            var Seq2 = Enumerable.Range(50, 100); //50,149

            //Union

            var Result = Seq1.Union(Seq2); // 0.. 149 => Remove Duplication

            //Concat(UnionAll)
            var Result1 = Seq1.Concat(Seq2); // 0 .. 99 + 50 .. 149

            //Distinct
            var Result = Result1.Distinct(); // 0 .. 149 => Remove Duplication

            //Intersect
            var Result = Seq1.Intersect(Seq2);  // 50 .. 99

            //Except
            var Result = Seq1.Except(Seq2); // 0 .. 49


            Console.WriteLine("================SEQ1========================");
            foreach (var item in Seq1)
                Console.Write($"{item} ");

            Console.WriteLine("\n================SEQ2========================");

            foreach (var item in Seq2)
                Console.Write($"{item} ");

            Console.WriteLine("\n================Result========================");

            foreach (var item in Result)
                Console.Write($"{item} ");


            #endregion

            #region Quantifier Operator - Return boolean

            //Any
            var Result = productsList.Any(); // True
            //If Sequence contains at least one Element => it will Return True

           var Result = productsList.Any(P => P.UnitsInStock == 0); // True
            //If Sequence contains at least one Element that match condition => it will Return True

            //All
            var Result = productsList.All(P => P.UnitsInStock == 0); // False
            //If All Elements in Sequence match condition => it will Return True

           //SequenceEqual

            var Seq1 = Enumerable.Range(0, 100); //0 .. 99
            var Seq2 = Enumerable.Range(50, 100); //50,149

            var Result = Seq1.SequenceEqual(Seq2); // False
            Console.WriteLine(Result);

            #endregion

            #region Zipping Operator - ZIP

            string[] Names = { "Ahmed", "Mohamed", "Mosatfa", "Zeyad", "Amir" };
            int[] Numbers = Enumerable.Range(1, 10).ToArray();
            char[] Chars = { 'A', 'M', 'E', 'Z' };

            var Result = Names.Zip(Numbers);

            //Output:
            //(Ahmed, 1)
            //(Mohamed, 2)
            //(Mosatfa, 3)
            //(Zeyad, 4)

            var Result = Names.Zip(Numbers, (Names, Numbers) => new { index = Numbers, Names });


            var Result = Names.Zip(Numbers, Chars);

            foreach (var item in Result)
                Console.WriteLine(item);

            #endregion

            #region Grouping Operators


            #region Get Products Grouped by Category

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category);

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category;

            foreach (var Category in Result)
            {
                Console.WriteLine(Category.Key); //Name of Category

                foreach (var Product in Category)
                {
                    Console.WriteLine($"           {Product.ProductName}");
                }
            }

            #endregion


            #region Get Products in Stock Grouped by Category

            //Fluent Syntax

            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                     .GroupBy(P => P.Category);

            //Query Syntax

            var Result = from P in productsList
                         where P.UnitsInStock > 0
                         group P by P.Category;

            foreach (var Category in Result)
            {
                Console.WriteLine(Category.Key); //Name of Category

                foreach (var Product in Category)
                {
                    Console.WriteLine($"           {Product.ProductName}");
                }
            }

            #endregion


            #region Get Products in Stock Grouped by Category That Contains More Than 10 Products

            //Fluent Syntax

            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                     .GroupBy(P => P.Category)
                                     .Where(C => C.Count() > 10);

            //Query Syntax

            var Result = from P in productsList
                         where P.UnitsInStock > 0
                         group P by P.Category
                         into Category
                         where Category.Count() > 10
                         select Category;

            foreach (var Category in Result)
            {
                Console.WriteLine(Category.Key); //Name of Category

                foreach (var Product in Category)
                {
                    Console.WriteLine($"           {Product.ProductName}");
                }
            }

            #endregion


            #region Get Category Name of Products in Stock That Contains More Than 10 Products and Number of Product In Each Category

            //Fluent Syntax

            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                      .GroupBy(P => P.Category)
                                       .Where(C => C.Count() > 10)
                                       .Select(X => new
                                       {
                                           CategoryName = X.Key,
                                           Count = X.Count()
                                       });

            //Query Syntax
              var Result = from P in productsList
                           where P.UnitsInStock > 0
                           group P by P.Category
                           into Category
                           where Category.Count() > 10
                           select new
                           {
                               CategoryName = Category.Key,
                               Count = Category.Count()
                           };

            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion
            #endregion


            #region Partitioning Operators

            #region Take & TakeLast

            //Take

            var Result = productsList.Take(10);
            //Take Number of Elements From First Only

            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                     .Take(10);


            //TakeLast

            var Result = productsList.TakeLast(10);
            //Take Number of Elements From Last Only
            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                         .TakeLast(10);


            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion


            #region Skip & SkipLast

            //Skip

            var Result = productsList.Skip(10);
            //Skip Number of Elements From First And Get Rest Of Elements

           var Result = productsList.Where(P => P.UnitsInStock > 0)
                                    .Skip(10);


            //SkipLast

            var Result = productsList.SkipLast(10);

            //Skip Number of Elements From Last And Get Rest Of Elements
            var Result = productsList.Where(P => P.UnitsInStock > 0)
                                     .SkipLast(10);


            var Result = productsList.Skip(10).Take(10); //pagination 

            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion

            #region TakeWhile & SkipWhile

            int[] Numbers = { 5, 4, 1, 3, 9, 2, 8, 7 };

            //TakeWhile

            var Result = Numbers.TakeWhile(Num => Num < 9);
            //Take Elements Till Element That do not Match Condition

            var Result = Numbers.TakeWhile((Num, I) => Num > I);


            //SkipWhile

            var Result = Numbers.SkipWhile(Num => Num % 3 != 0);
            //Skip Elements Till Element That do not Match Condition
            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion
            #endregion

            #region Let & Into

            List<string> Names = new List<string>() { "Ahmed", "Ali", "Amr", "Sally", "Mohamed" };
            //A , O ,U ,I ,E

            //Into => Restart Query With Introducing A new Range
            var Result = from N in Names
                         select Regex.Replace(N, "[AOUIEaouie]", string.Empty)
                         into NoVowelNames
                         where NoVowelNames.Length > 3
                         select NoVowelNames;


            //let => Continue Query With Added A new Range
            var Result = from N in Names
                         let NoVowelNames = Regex.Replace(N, "[AOUIEaouie]", string.Empty)
                         where NoVowelNames.Length > 3
                         select NoVowelNames;



            foreach (var item in Result)
                Console.WriteLine(item);
            #endregion
        }
    }
}
