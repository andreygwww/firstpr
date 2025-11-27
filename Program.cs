using System;

class Polynomial
{
    private int degree;
    private double[] coeffs;

    public Polynomial()
    {
        degree = 0;
        coeffs = new double[1] { 0.0 };
    }

    public Polynomial(double[] new_coeffs)
    {
        degree = new_coeffs.Length - 1;
        coeffs = (double[])new_coeffs.Clone();
    }

    public int Degree
    {
        get { return degree; }
    }

    public double[] Coeffs
    {
        get { return (double[])coeffs.Clone(); }
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < coeffs.Length; i++)
        {
            double coef = coeffs[i];
            if (coef == 0) { continue; }

            if (result != "" && coef > 0) { result += " + "; }
            if (result != "" && coef < 0) { result += " - "; }
            if (result == "" && coef < 0) { result += "-"; }

            double absCoef = Math.Abs(coef);
            if (i == 0)
            {
                result += absCoef;
            }
            else if (i == 1)
            {
                if (absCoef == 1) { result += "x"; }
                else { result += absCoef + "x"; }
            }
            else
            {
                if (absCoef == 1) { result += "x^" + i; }
                else { result += absCoef + "x^" + i; }
            }
        }
        if (result == "") { return "0"; }
        return result;
    }

    public static Polynomial operator *(Polynomial obj1, double k)
    {
        int len = obj1.coeffs.Length;
        double[] newCoeffs = new double[len];
        for (int i = 0; i < len; i++)
        {
            newCoeffs[i] = obj1.coeffs[i] * k;
        }

        return new Polynomial(newCoeffs);
    }

    public static Polynomial operator +(Polynomial p1, Polynomial p2)
    {
        int len1 = p1.coeffs.Length;
        int len2 = p2.coeffs.Length;
        int maxLen = Math.Max(len1, len2);
        double[] newCoeffs = new double[maxLen];
        for (int i = 0; i < maxLen; i++)
        {
            double c1 = (i < len1) ? p1.coeffs[i] : 0;
            double c2 = (i < len2) ? p2.coeffs[i] : 0;
            newCoeffs[i] = c1 + c2;
        }
        return new Polynomial(newCoeffs);
    }
}

class Programm
{
    static void Main(string[] args)
    {
        double[] coeffs1 = { 1.0, 0.0, 2.0 };
        Polynomial p1 = new Polynomial(coeffs1);
        double[] coeffs2 = { 3.0, 4.0 };
        Polynomial p2 = new Polynomial(coeffs2);
        Console.WriteLine($"Полином 1: {p1}");
        Console.WriteLine($"Полином 2:{p2}");
        Polynomial sum = p1 + p2;
        Console.WriteLine(sum);
        Polynomial mult = p1 * 5;
        Console.WriteLine(mult);
        Console.ReadKey();
    }
}