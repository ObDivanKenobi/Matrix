using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixNamespace;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix;
            Console.WriteLine("Введите размерность: ");
            string[] dimentions = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (dimentions.Length == 0)
            {
                Console.WriteLine("SOMETHING'S WRONG, SHEPARD!");
                Console.ReadLine();
                return;
            }

            if (dimentions.Length == 1)
            {
                int n;
                if (!int.TryParse(dimentions[0], out n))
                {
                    Console.WriteLine("SOMETHING'S WRONG, SHEPARD!");
                    Console.ReadLine();
                    return;
                }

                matrix = new Matrix(n);
            }
            else
            {
                int m, n;
                if (!int.TryParse(dimentions[0], out m) || !int.TryParse(dimentions[1], out n))
                {
                    Console.WriteLine("SOMETHING'S WRONG, SHEPARD!");
                    Console.ReadLine();
                    return;
                }

                matrix = new Matrix(m, n);
            }

            bool FAIL;
            do
            {
                FAIL = false;
                Console.WriteLine($"Введите {matrix.M}x{matrix.N} матрицу:");
                for (int i = 0; i < matrix.M; ++i)
                {
                    string[] line = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < matrix.N; ++j)
                    {
                        double d;
                        if (!double.TryParse(line[j], out d))
                        {
                            Console.WriteLine("SOMETHING'S WRONG, SHEPARD!");
                            FAIL = true;
                            break;
                        }

                        matrix[i, j] = d;
                    }

                    if (FAIL)
                        break;
                }
            } while (FAIL);
            
            //Matrix m = new Matrix(3, 3, new double[,] { { 3, 2, 1 }, { 5, 2, 3 }, { 1, 1, 1 } });
            //Matrix m = new Matrix(2, 2, new double[,] { { 1, 2 }, { 3, 4 } });
            Console.WriteLine("M:");
            Console.WriteLine(matrix.ToString());

            Matrix tmp = matrix.Transpose();
            Console.WriteLine($"M^T: {Environment.NewLine}{tmp.ToString()}");
            
            if (!matrix.IsSquare)
            {
                Console.WriteLine("Вычислить определитель и найти обратную матрицу невозможно — исходная матрица не является квадратной!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"det(M) = {matrix.det()};");
            Console.WriteLine();

            if (!matrix.TryInvert(out tmp))
            {
                Console.WriteLine("Вычислить обратную матрицу невозможно - определитель равен нулю.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("M^(-1):");
            for (int i = 0; i < tmp.M; ++i)
            {
                for (int j = 0; j < tmp.N; ++j)
                    Console.Write($"{tmp[i, j]} ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Проверка:");
            Console.WriteLine("M*M(^-1) = ");
            Console.WriteLine($"{(matrix * tmp).ToString()}");

            Console.ReadLine();
        }
    }
}
