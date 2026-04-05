using System.Data;

partial class Program
{
    private static void DeferredExecution(string[] names)
    {
        //Deferred execution means that the query is not executed until we enumerate over the results.
        //This allows us to change the data source before we execute the query and see the changes reflected in the results.
        SectionTitle("Deferred execution");
        // Question: Which names end with an M?
        // (using a LINQ extension method)
        var query1 = names.Where(name => name.EndsWith("m")); //extension method or method syntax
        // Question: Which names end with an M?
        // (using LINQ query comprehension syntax)
        var query2 = from name in names where name.EndsWith("m") select name; //query syntax

        // Answer returned as an array of strings containing Pam and Jim.
        string[] result1 = query1.ToArray(); // ToArray() which forces the query to execute and return the results as an array of strings. (Materilize the query)
        // Answer returned as a list of strings containing Pam and Jim.
        List<string> result2 = query2.ToList(); // ToList() which forces the query to execute and return the results as a list of strings.((Materilize the query)
        // Answer returned as we enumerate over the results.
        foreach (string name in query1)
        {
            WriteLine(name); // outputs Pam
            names[2] = "Jimmy"; // Change Jim to Jimmy.
                                // On the second iteration Jimmy does not 
                                // end with an "m" so it does not get output.
        }
    }

    private static void FilteringUsingWhere(string[] names)
    {
        SectionTitle("Filtering using Where");
        // Explicitly creating the required delegate.
        //var query = names.Where(new Func<string, bool>(NameLongerThanFour)); // new func<string, bool> is a delegate that takes a string and returns a bool
        //delegate means we can pass a method as parameter to another method.

        // The compiler creates the delegate automatically.
        //var query = names.Where(NameLongerThanFour);

        // Using a lambda expression instead of a named method.
        // lambda expresion is an anonymous/nameless function.
        // - It uses => (lamda expression) symbol read as "goes to" or "maps to".
        //var query = names.Where(name => name.Length > 4);

        //orderby,orderbydescending,thenby, thenbydescending are explicit sorting extension methods
        var query1 = names
            .Where(name => name.Length > 4)
            .OrderBy(name => name.Length);

        var query2 = names
            .Where(name => name.Length > 4)
            .OrderByDescending(name => name.Length);

        var query3 = names
            .Where(name => name.Length > 4)
            .OrderBy(name => name.Length)
            .ThenBy(name => name);

        IOrderedEnumerable<string> query5 = names
          .Where(name => name.Length > 4)
          .OrderBy(name => name.Length)
          .ThenBy(name => name);

        //order,orderdescending, here names is array of string type so it implicitly implements the Icomparable interface
        // -if the type is complex i.e Person or Customer the we need to implement the Icomparable interface to use order or orderdescending extension method.
        var query6 = names
            .Order();



        foreach (string item in query5)
        {
            WriteLine(item);
        }
    }

    static bool NameLongerThanFour(string name)
    {
        return name.Length > 4;
    }

    static bool FirstNameStartWithD(string name)
    {
        return name.StartsWith("D");
    }


    static void FilteringByType()
    {

        //Filtering by where is best for values such as numbers,text etc but if the sequence contains multiple types then 
        // we can use the OfType<T>() extension method to filter by type.

        List<Exception> exceptions = new List<Exception>
        {
            new ArgumentException(),
            new InvalidOperationException(),
            new DivideByZeroException(),
            new ArgumentNullException(),
            new SystemException(),
            new IndexOutOfRangeException(),
            new OverflowException()
        };

        //filter by type using ofType<T>() extension method
        IEnumerable<ArithmeticException> arithmeticExceptionQuery = exceptions.OfType<ArithmeticException>();

        foreach (ArithmeticException ex in arithmeticExceptionQuery)
        {
            WriteLine(ex);
        }
    }

    static void Output(IEnumerable<string> cohort, string description = "")
    {
        if (!string.IsNullOrEmpty(description))
        {
            WriteLine(description);
        }
        Write(" ");
        WriteLine(string.Join(", ", cohort.ToArray()));
        WriteLine();
    }

    static void WorkingWithSets()
    {
        string[] cohort1 =
          { "Rachel", "Gareth", "Jonathan", "George" };
        string[] cohort2 =
          { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
        string[] cohort3 =
          { "Declan", "Jack", "Jack", "Jasmine", "Conor" };
        SectionTitle("The cohorts");
        Output(cohort1, "Cohort 1");
        Output(cohort2, "Cohort 2");
        Output(cohort3, "Cohort 3");
        SectionTitle("Set operations");
        Output(cohort2.Distinct(), "cohort2.Distinct()");
        Output(cohort2.DistinctBy(name => name.Substring(0, 2)),
          "cohort2.DistinctBy(name => name.Substring(0, 2)):");
        Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
        Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
        Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)");
        Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
        Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"),
          "cohort1.Zip(cohort2)");
    }
}