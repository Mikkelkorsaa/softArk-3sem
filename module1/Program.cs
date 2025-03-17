
Console.WriteLine(vendStreng("Hej"));
static int areal(int bredde)
{
    int result;
    if (bredde == 1)
    {
        result = 1;
    }
    else
    {
        result = bredde + areal(bredde - 1);
    }
    return result;
}
static int fakultet(int n)
{
    int result;
    if (n == 0)
    {
        result = 1;
    }
    else
    {
        result = n * fakultet(n - 1);
    }
    return result;
}
static int euclids(int a, int b)
{

    if (b <= a && a % b == 0)
    {
        return b;
    }
    else if (a < b)
    {
        return euclids(b, a);
    }
    else
    {
        return euclids(b, a % b);
    }


}
static int potens(int n, int p)
{

    if (p == 0)
    {
        return 1;
    }
    else
    {
        return n * potens(n, p - 1);
    }
}
static int times(int a, int b)
{
    if (a == 1)
    {
        return b;
    }
    else if (a == 0)
    {
        return 0;
    }
    else if (a > 1)
    {
        return b + times(a - 1, b);
    }
    return 0;
}
static string vendStreng(string s)
{

    if (s.Length == 0 || s.Length <= 1)
    {
        return s;
    }
    else
    {
        return vendStreng(s.Substring(1)) + s[0];
    }
}