using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            // // Comment the line below and write your code here
            // throw new NotImplementedException();
            int size = (int)MATRIX_SIZE;
            double[,] a = new double[size, size]; // zarayeb
            double[] b = new double[size]; // adad sabet
            for (int raw = 0; raw < size; ++raw)
            {
                for (int column = 0; column < size; ++column)
                    a[raw, column] = matrix[raw, column];
                b[raw] = matrix[raw, size];
            }
            Equation equation = new Equation(a, b);
            return SolveEquation(equation);
        }

        // static Position SelectPivotElement(double[,] a, bool[] used_raws, Position pivot)
        // {
        //     // This algorithm selects the first free element.
        //     // You'll need to improve it to pass the problem.
        //     while (pivot.raw < used_raws.Length && (used_raws[pivot.raw] || a[pivot.raw, pivot.column] == 0))
        //         ++pivot.raw;
        //     return pivot;
        // }

        // static Position SelectPivotElement(double[,] a, bool[] used_raws, bool[] used_columns)
        // {
        //     // This algorithm selects the first free element.
        //     // You'll need to improve it to pass the problem.
        //     Position pivot_element = new Position(0, 0);
        //     while (used_raws[pivot_element.raw])
        //         ++pivot_element.raw;
        //     while (used_columns[pivot_element.column])
        //         ++pivot_element.column;
        //     return pivot_element;
        // }

        static Position SelectPivotElement(double[,] a, bool[] used_raws, Position pivot)
        {
            // This algorithm selects the first free element.
            // You'll need to improve it to pass the problem.
            while (used_raws[pivot.raw] || a[pivot.raw, pivot.column] == 0)
                ++pivot.raw;
            if (pivot.raw == used_raws.Length)
            {
                return new Position(0, 0);
            }
            else
                return pivot;
        }

        static void SwapLines(double[,] a, double[] b, bool[] used_raws, Position pivot_element)
        {
            int size = b.Length;

            for (int column = 0; column < size; ++column)
            {
                double tmpa = a[pivot_element.column, column];
                a[pivot_element.column, column] = a[pivot_element.raw, column];
                a[pivot_element.raw, column] = tmpa;
            }

            double tmpb = b[pivot_element.column];
            b[pivot_element.column] = b[pivot_element.raw];
            b[pivot_element.raw] = tmpb;

            bool tmpu = used_raws[pivot_element.column];
            used_raws[pivot_element.column] = used_raws[pivot_element.raw];
            used_raws[pivot_element.raw] = tmpu;

            pivot_element.raw = pivot_element.column;
        }

        static void ProcessPivotElement(double[,] a, double[] b, Position pivot_element)
        {
            // Write your code here
            double scale = a[pivot_element.raw, pivot_element.column];
            // if (scale != 0)
            for (int i = 0; i < b.Length; i++) // column count
            {
                a[pivot_element.raw, i] /= scale;
            }
            b[pivot_element.raw] /= scale;
            for (int i = 0; i < b.Length; i++) // raw count
            {
                if (i != pivot_element.raw)
                {
                    double negScale = a[i, pivot_element.column];
                    for (int c = 0; c < b.Length; c++) // column count
                    {
                        a[i, c] -= negScale * a[pivot_element.raw, c];
                    }
                    b[i] -= negScale * b[pivot_element.raw];
                }
            }
            // double scale = a[pivot_element.raw, pivot_element.column];
            // // if (scale != 0)
            // for (int i = 0; i < b.Length; i++) // column count
            // {
            //     a[pivot_element.raw, i] /= scale;
            // }
            // for (int i = pivot_element.raw + 1; i < b.Length; i++) // raw count
            // {
            //     double negScale = a[i, pivot_element.column];
            //     for (int c = 0; c < b.Length; c++) // column count
            //     {
            //         a[i, c] -= negScale * a[pivot_element.raw, c];
            //     }
            // }
        }

        static void MarkPivotElementUsed(Position pivot_element, bool[] used_raws, bool[] used_columns)
        {
            used_raws[pivot_element.raw] = true;
            used_columns[pivot_element.column] = true;
        }

        // static double[] SolveEquation(Equation equation)
        // {
        //     double[,] a = equation.a;
        //     double[] b = equation.b;
        //     int size = b.Length;

        //     bool[] used_columns = new bool[size];
        //     bool[] used_raws = new bool[size];
        //     for (int step = 0; step < size; ++step)
        //     {
        //         Position pivot_element = SelectPivotElement(a, used_raws, used_columns);
        //         SwapLines(a, b, used_raws, pivot_element); // ????
        //         ProcessPivotElement(a, b, pivot_element); // 
        //         MarkPivotElementUsed(pivot_element, used_raws, used_columns);
        //     }

        //     return b;
        // }

        static double[] SolveEquation(Equation equation)
        {
            double[,] a = equation.a;
            double[] b = equation.b;
            int size = b.Length;

            bool[] used_columns = new bool[size];
            bool[] used_raws = new bool[size];
            for (int step = 0; step < size; ++step)
            {
                Position pivot_element = new Position(step, 0);
                pivot_element = SelectPivotElement(a, used_raws, pivot_element);
                SwapLines(a, b, used_raws, pivot_element); // ????
                ProcessPivotElement(a, b, pivot_element); // 
                MarkPivotElementUsed(pivot_element, used_raws, used_columns);
            }

            // roundation
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] >= 0)
                {
                    double x = b[i] - (int)b[i];
                    if (x < 0.25)
                        b[i] = b[i] - x;
                    else if (x >= 0.75)
                        b[i] = b[i] - x + 1;
                    else
                        b[i] = b[i] - x + 0.5;
                }
                else
                {
                    double x = (b[i] - (int)b[i])*-1;
                    if (x < 0.25)
                        b[i] = b[i] + x;
                    else if (x >= 0.75)
                        b[i] = b[i] + x - 1;
                    else
                        b[i] = b[i] + x - 0.5;
                }

            }
            return b;
        }

    }

    public class Equation
    {
        public Equation(double[,] a, double[] b)
        {
            this.a = a;
            this.b = b;
        }

        public double[,] a;
        public double[] b;
    }

    public class Position
    {
        public Position(int column, int raw)
        {
            this.column = column;
            this.raw = raw;
        }

        public int column;
        public int raw;
    }

}
