using System;
using System.Linq;
using Microsoft.VisualBasic;

public class Opgave1
{
    public static Person[] people = new Person[]
    {
        new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
        new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
        new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
        new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
        new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
        new Person { Name = "Rosa Jensen", Age = 23, Phone = "+4543217846" },
    };

    public int GetTotalAge()
    {
        return people.Sum(person => person.Age);
    }
    public int CountNames(string name)
    {
        return people.Count(person => person.Name.Contains(name));
    }
    public int OldestPerson()
    {
        return people.Max(person => person.Age);
    }
    public bool HasPhoneNumber(string phoneNumber)
    {
        return people.Where(person => person.Phone == phoneNumber).Any();
    }
    public string OlderThan(int age)
    {
        IEnumerable<Person> people1 = people.Where(person => person.Age > age);
        return people1.Select(person => person.Name).Aggregate((a, b) => a + ", " + b);
    }
    public static Person[] BetterPhone(Person[] people)
    {
        return people.Select(p => new Person { Name = p.Name, Phone = p.Phone.Substring(2) }).ToArray();
    }
    public string YoungerThan30(Person[] person)
    {
        var people1 = people.Where(person => person.Age < 30);
        return string.Join(": ", people1.Select(person => $"{person.Name}, {person.Phone}"));
    }
}


public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; } = 0;
    public string Phone { get; set; } = "";
}

public class BubbleSort
{
    public static Func<Person, Person, int> ComparePersonAge = (p1, p2) => p1.Age - p2.Age;

    public static Func<Person, Person, int> ComparePersonName = (p1, p2) => p1.Name.CompareTo(p2.Name);

    public static Func<Person, Person, int> ComparePersonPhone = (p1, p2) => p1.Phone.CompareTo(p2.Phone);

    // Bytter om på to elementer i et array
    private static void Swap(Person[] array, int i, int j)
    {
        Person temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    // Laver sortering på array med Bubble Sort. 
    // compareFn bruges til at sammeligne to personer med.
    public static Person[] Sort(Person[] array, Func<Person, Person, int> compareFn)
    {
        for (int i = array.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j <= i - 1; j++)
            {
                // Laver en ombytning, hvis to personer står forkert sorteret
                if (compareFn(array[j], array[j + 1]) > 0)
                {
                    Swap(array, j, j + 1);
                }
            }
        }
        return array;
    }
}

class Program
{
    static void Main(string[] args)
    {

        Person[] ppl = BubbleSort.Sort(Opgave1.people, BubbleSort.ComparePersonAge);
        System.Console.WriteLine("Sorted by Age");
        foreach (Person p in ppl)
        {
            Console.WriteLine(p.Name + " " + p.Age);
        }
        Person[] ppl2 = BubbleSort.Sort(Opgave1.people, BubbleSort.ComparePersonName);
        System.Console.WriteLine("Sorted by Name");
        foreach (Person p in ppl2)
        {
            Console.WriteLine(p.Name + " " + p.Age);
        }
        Person[] ppl3 = BubbleSort.Sort(Opgave1.people, BubbleSort.ComparePersonPhone);
        System.Console.WriteLine("Sorted by Phone");
        foreach (Person p in ppl3)
        {
            Console.WriteLine(p.Name + " " + p.Phone);
        }
        Opgave1 opgave = new Opgave1();
        int totalAge = opgave.GetTotalAge();
        Console.WriteLine("Total Age: " + totalAge);
        int countNames = opgave.CountNames("Nielsen");
        Console.WriteLine("Count Names: " + countNames);
        int oldestPerson = opgave.OldestPerson();
        Console.WriteLine("Oldest Person: " + oldestPerson);
        bool hasPhoneNumber = opgave.HasPhoneNumber("+4543217846");
        Console.WriteLine("Has Phone Number: " + hasPhoneNumber);
        string olderThan = opgave.OlderThan(30);
        Console.WriteLine("Older Than: " + olderThan);
        Person[] p2 = Opgave1.BetterPhone(Opgave1.people);
        foreach (Person p in p2)
        {
            Console.WriteLine(p.Name + " " + p.Phone);
        }
        string youngerThan30 = opgave.YoungerThan30(Opgave1.people);
        Console.WriteLine("Younger Than 30: " + youngerThan30);

        var CreateWordFilterFn = (string[] badWords) =>
        {
            return (string text) =>
            {
                return text.Split(' ')
                .Where(word =>
                !badWords.Contains(word))
                .Aggregate((a, b) => a + " " + b);

            };
        };

        var CreateWordReplacerFn = (string[] badWords, string replacement) =>
        {
            return (string text) =>
            {
                return text.Split(' ')
                .Select(word => badWords.Contains(word) ? replacement : word)
                .Aggregate((a, b) => a + " " + b);
            };
        };

        var badWords = new string[] { "luder", "svin", "fuck", };
        var ReplaceBadWords = CreateWordFilterFn(badWords);
        Console.WriteLine(ReplaceBadWords("Du er en luder og et svin fuck dig "));




    }
};