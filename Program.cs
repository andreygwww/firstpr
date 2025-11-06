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

            if (result != "" && coef > 0) {result += " + "; }
            if (result != "" && coef < 0) {result += " - "; }
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
}
       
class Programm
{
    static void Main(string[] args)
    {
        double[] coeffs = { 1.0, 0.0, 2.0 };
        Polynomial p = new Polynomial(coeffs); // 1 + 2x^2

        Console.WriteLine(p);
    }
}