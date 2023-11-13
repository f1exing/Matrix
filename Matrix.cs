using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Contest
{
    internal class Matrix
    {
        public double[,] matrix;

        public Matrix(double[,] a)
        {
            this.matrix = a;
        }

        public Matrix()
        {
            this.matrix = new double[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 234, 342, 342 } };
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix();
            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            return c;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix c = new Matrix();
            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
                }
            return c;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c = new Matrix();
            if (a.matrix.GetLength(0) != b.matrix.GetLength(1))
            {
                throw new Exception("Умножение невозможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }
            c.matrix = new double[a.matrix.GetLength(0), b.matrix.GetLength(1)];

            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] = 0;
                    for (int k = 0; k < a.matrix.GetLength(1); k++)
                    {
                        c.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                    };
                };
            return c;
        }

        public static Matrix operator *(Matrix a, double b)
        {
            Matrix c = new Matrix();
            c = a;
            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] *= b;
                };
            return c;
        }

        public static Matrix operator /(Matrix a, Matrix b)
        {
            Matrix c = new Matrix();

            c.matrix = new double[a.matrix.GetLength(0), b.matrix.GetLength(1)];

            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] = 0;
                    for (int k = 0; k < a.matrix.GetLength(1); k++)
                    {
                        c.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                    };
                };
            return c;
        }

        public static Matrix operator /(Matrix a, double b)
        {
            Matrix c = new Matrix();
            c = a;
            for (int i = 0; i < a.matrix.GetLength(0); i++)
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    c.matrix[i, j] /= b;
                };
            return c;
        }

        public double Determ()
        {
            if (matrix.GetLength(0) == 1)
                return matrix[0, 0]; 
            double det = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                det += matrix[i, 0] * this.GetMinor(i, 0).Determ();
            }

            return det;
        }

        public Matrix FormMinor()
        {
            if (this.matrix.GetLength(0) != this.matrix.GetLength(1))
            {
                throw new Exception(" Число строк в матрице не совпадает с числом столбцов");
            }
         Matrix s = new Matrix();
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(0); j++)
                {
                    s.matrix[i, j] = this.GetMinor(i, j).Determ();    
                }
            }
            return s;
        }

        public Matrix GetMinor(int n, int m)
        {
            Matrix c = new Matrix();
            c.matrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            for (int i = 0, k = 0; i < matrix.GetLength(0); i++)
            {
                if (i == n)
                    continue;

                for (int j = 0, u = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == m)
                        continue;

                    c.matrix[k, u] = matrix[i, j];
                    u++;
                }
                k++;
            }
            return c;  
        
        }

        public Matrix FormAlg()
        {
            if (this.matrix.GetLength(0) != this.matrix.GetLength(1))
            {
                throw new Exception(" Число строк в матрице не совпадает с числом столбцов");
            }

            Matrix s = new Matrix();
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(0); j++)
                {
                    s.matrix[i, j] = (int)(Math.Pow(-1, i + j) * this.matrix[i, j]);
                }
            }
            return s;
        }

        public Matrix Transpon()
        {
            if (this.matrix.GetLength(0) != this.matrix.GetLength(1))
            {
                throw new Exception(" Число строк в матрице не совпадает с числом столбцов");
            }

            Matrix s = new Matrix();
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(0); j++)
                {
                    s.matrix[i, j] = this.matrix[j, i];
                }
            }
            return s;   
        }


        public int GetLength(int a)
        {
            return this.matrix.GetLength(a);
        }


        public Matrix Invert()  
        {
            Matrix c = new Matrix();
            c = this.FormMinor().FormAlg().Transpon() / this.Determ();
            return c;
        }




        public static void print(Matrix a)
        {
            for (int i = 0; i < a.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    Console.Write($"{a.matrix[i, j]} \t");
                }
                Console.WriteLine("\n");
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix();
            Matrix m2 = new Matrix(new double[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 234, 342, 342 } });
            Console.WriteLine("Second matrix");
            Matrix.print(m1);
            Console.WriteLine("Second matrix");
            Matrix.print(m2);
            Console.WriteLine("Second matrix");

            Console.WriteLine();
            Matrix.print(m1 + m2);
            Console.WriteLine();
            Matrix.print(m1 - m2);
        }
    }

}
