using System.Data;

partial class Program
{
    private static void DeferredExecution(string[] names)
    {
        SectionTitle("Deferred execution");
        // Question: Which names end with an M?
        // (using a LINQ extension method)
        var query1 = names.Where(name => name.EndsWith("m")); //extension method or method syntax
        // Question: Which names end with an M?
        // (using LINQ query comprehension syntax)
        var query2 = from name in names where name.EndsWith("m") select name; //query syntax

        // Answer returned as an array of strings containing Pam and Jim.
        string[] result1 = query1.ToArray();
        // Answer returned as a list of strings containing Pam and Jim.
        List<string> result2 = query2.ToList();
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
        var query = names.Where(name => name.Length > 4);


        foreach (string item in query)
        {
            WriteLine(item);
        }

        SectionTitle("Filtering using First");
        //explicit delegate creation of func<string,bool> which takes a string and returns a bool. if bool is true then result included else excluded.
        //string test = names.First(new Func<string, bool>(FirstNameStartWithD));

        //implicit delegate creation by compiler i.e support direct method name as parameter. no need to use of new func<string, bool>.
        //string test = names.First(FirstNameStartWithD);

        //using of lambda expression to reduce the code and increase readability.
        string test = names.First(name => name.StartsWith("D"));

        WriteLine(test);
    }

    static bool NameLongerThanFour(string name)
    {
        return name.Length > 4;
    }

    static bool FirstNameStartWithD(string name)
    {
        return name.StartsWith("D");
    }
}