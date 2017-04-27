using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNamespace
{
    public class Matrix
    {
        List<List<double>> matrix;
        int m, n;
        int eps;

        /// <summary>
        /// Число строк матрицы.
        /// </summary>
        public int M { get { return m; } }
        /// <summary>
        /// Число столбцов матрицы.
        /// </summary>
        public int N { get { return n; } }
        /// <summary>
        /// Является ли матрица квадратной.
        /// </summary>
        public bool IsSquare { get { return m == n; } }

        public double this[int i, int j]
        {
            get { return matrix[i][j]; }
            set { matrix[i][j] = value; }
        }

        public double this[int i]
        {
            get { return matrix[0][i]; }
            set { matrix[0][i] = value; }
        }

        public Matrix(int m, int n, int e = 5)
        {
            eps = e;
            this.m = m;
            this.n = n;
            matrix = new List<List<double>>(m);
            for (int i = 0; i < m; ++i)
            {
                matrix.Add(new List<double>(n));
                for (int j = 0; j < n; ++j)
                    matrix[i].Add(default(double));
            }
        }

        public Matrix(int n, int e = 5)
        {
            eps = e;
            m = n;
            this.n = n;
            matrix = new List<List<double>>(m);
            for (int i = 0; i < m; ++i)
            {
                matrix.Add(new List<double>(n));
                for (int j = 0; j < n; ++j)
                    matrix[i].Add(default(double));
            }
        }

        public Matrix(List<List<double>> matr, int e = 5)
        {
            eps = e;
            m = matr.Count;
            n = matr[0].Count;
            matrix = new List<List<double>>();
            for (int i = 0; i < m; ++i)
            {
                matrix.Add(new List<double>(n));
                for (int j = 0; j < n; ++j)
                    matrix[i].Add(matr[i][j]);
            }
        }

        public Matrix(int m, int n, double[,] matr, int e = 5)
        {
            eps = e;
            this.m = m;
            this.n = n;
            matrix = new List<List<double>>();
            for (int i = 0; i < m; ++i)
            {
                matrix.Add(new List<double>(n));
                for (int j = 0; j < n; ++j)
                    matrix[i].Add(matr[i, j]);
            }
        }

        /// <summary>
        /// Сложение матриц <paramref name="A"/> и <paramref name="B"/>.
        /// </summary>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Если <typeparamref name="T"/> не поддерживает оператор +.</exception>
        /// <exception cref="ArgumentException">Если размерности матриц не совпадают.</exception>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
                throw new ArgumentException("Размерности матриц не совпадают!");
            Matrix result = new Matrix(A.M, A.N);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    result[i, j] = A[i, j] + B[i, j];

            return result;
        }

        /// <summary>
        /// Вычитание матриц <paramref name="A"/> и <paramref name="B"/>.
        /// </summary>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Если <typeparamref name="T"/> не поддерживает оператор +.</exception>
        /// <exception cref="ArgumentException">Если размерности матриц не совпадают.</exception>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
                throw new ArgumentException("Размерности матриц не совпадают!");
            Matrix result = new Matrix(A.M, A.N);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    result[i, j] = A[i, j] + B[i, j];

            return result;
        }

        /// <summary>
        /// Умножение матриц <paramref name="A"/> и <paramref name="B"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Если <paramref name="A.N"/> и <paramref name="B.M"/> не совпадают.</exception>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.N != B.M)
                throw new ArgumentException("Число столбцов первой матрицы не равно числу строк второй матрицы.");

            Matrix result = new Matrix(A.M, B.N);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    for (int k = 0; k < A.N; ++k)
                    {
                        double a = A[i, k],
                               b = B[k, j],
                               mult = a * b;
                        result[i, j] += Math.Round(mult, Math.Max(A.eps, B.eps));
                    }
            return result;
        }

        /// <summary>
        /// Умножение матрицы <paramref name="A"/> на число <paramref name="k"/>.
        /// </summary>
        public static Matrix operator *(Matrix A, double k)
        {
            Matrix result = new Matrix(A.M, A.N);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    result[i, j] = Math.Round(A[i, j] * k, A.eps);

            return result;
        }

        /// <summary>
        /// Умножение матрицы <paramref name="A"/> на число <paramref name="k"/>.
        /// </summary>
        public static Matrix operator *(double k, Matrix A)
        {
            return A * k;
        }

        /// <summary>
        /// Деление матрицы <paramref name="A"/> на число <paramref name="k"/>.
        /// </summary>
        public static Matrix operator /(Matrix A, double k)
        {
            Matrix result = new Matrix(A.M, A.N);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    result[i, j] = Math.Round(A[i, j] / k, A.eps);

            return result;
        }

        public static bool operator ==(Matrix A, Matrix B)
        {
            if (A.M != B.M || A.N != B.N)
                return false;

            for (int i = 0; i < A.M; ++i)
                for (int j = 0; j < A.N; ++j)
                    if (A[i, j] != B[i, j]) return false;

            return true;
        }

        public static bool operator !=(Matrix A, Matrix B)
        {
            return !(A == B);
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <returns>Транспонированная матрица.</returns>
        public Matrix Transpose()
        {
            Matrix result = new Matrix(N, M);
            for (int i = 0; i < result.M; ++i)
                for (int j = 0; j < result.N; ++j)
                    result[i, j] = matrix[j][i];

            return result;
        }

        /// <summary>
        /// Попытка вычисления обратной матрицы.
        /// Возможна только для квадратных матриц.
        /// </summary>
        /// <returns>Получилось ли вычислить обратную матрицу.</returns>
        public bool TryInvert(out Matrix inverse)
        {
            inverse = null;
            double d = det();
            if (!IsSquare || d == 0)
                return false;

            Matrix adj = new Matrix(N, M);
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    adj[i, j] = Math.Pow(-1, i + j) * GetMinor(i, j).det();

            inverse = adj.Transpose() / d;

            return true;
        }

        public Matrix GetMinor(int removedRow, int removedColumn)
        {
            List<List<double>> tmp = new List<List<double>>();
            for (int i = 0; i < m; ++i)
            {
                tmp.Add(new List<double>(n));
                for (int j = 0; j < n; ++j)
                {
                    if (j == removedColumn)
                        continue;
                    tmp[i].Add(matrix[i][j]);
                }
            }

            tmp.RemoveAt(removedRow);
            return new Matrix(tmp);
        }

        public double det()
        {
            if (m != n)
                throw new ArgumentException("Попытка вычислить определитель матрицы, не являющейся квадратной!");

            if (m == n && m == 1)
                return matrix[0][0];

            double det = 0;
            for (int i = 0; i < n; ++i)
            {
                double md = GetMinor(0, i).det();
                det += Math.Pow(-1, 2 + i) * matrix[0][i] * md;
            }

            return det;
        }

        public object Clone()
        {
            return new Matrix(matrix);
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                    str += $"{matrix[i][j]:f5} ";
                str = str.TrimEnd(new char[] { ' ' }) + Environment.NewLine;
            }

            return str;
        }
    }
}
