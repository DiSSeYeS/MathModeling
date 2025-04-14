using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
namespace MathModeling2 
{ 
class SimplexMethod 
{ 
static void Main(string[] args) 
{ 
Console.WriteLine("Добро пожаловать в программу для решения 
задачи линейного программирования методом симплекс!"); 
            Console.WriteLine("Введите количество переменных:"); 
            int numVariables = int.Parse(Console.ReadLine()); 
 
            Console.WriteLine("Введите количество ограничений:"); 
            int numConstraints = int.Parse(Console.ReadLine()); 
 
            double[,] tableau = new double[numConstraints + 1, numVariables 
+ numConstraints + 1]; 
 
            Console.WriteLine("Введите коэффициенты целевой функции (через 
пробел):"); 
            string[] objectiveInput = Console.ReadLine().Split(' '); 
            for (int j = 0; j < numVariables; j++) 
            { 
                tableau[numConstraints, j] = 
double.Parse(objectiveInput[j]); 
            } 
 
            Console.WriteLine("Введите коэффициенты ограничений (с правыми 
частями через пробел):"); 
            for (int i = 0; i < numConstraints; i++) 
            { 
                string[] constraintInput = Console.ReadLine().Split(' '); 
                for (int j = 0; j < numVariables; j++) 
                { 
                    tableau[i, j] = double.Parse(constraintInput[j]); 
                } 
                tableau[i, numVariables + i] = 1; // Добавление базисных 
переменных 
                tableau[i, tableau.GetLength(1) - 1] = 
double.Parse(constraintInput[constraintInput.Length - 1]); // Свободный член 
            } 
 
            SolveSimplex(tableau, numVariables, numConstraints); 
        } 
 
        static void SolveSimplex(double[,] tableau, int numVariables, int 
numConstraints) 
        { 
            int rows = tableau.GetLength(0); 
            int cols = tableau.GetLength(1); 
 
            while (true) 
            { 
                int pivotCol = FindPivotColumn(tableau, cols); 
                if (pivotCol == -1) 
                { 
                    Console.WriteLine("Оптимальное решение найдено."); 
                    PrintSolution(tableau, numVariables, numConstraints); 
                    return; 
                } 
 
                int pivotRow = FindPivotRow(tableau, pivotCol, rows); 
                if (pivotRow == -1) 
                { 
                    Console.WriteLine("Задача не имеет ограничений 
(неограниченное решение)."); 
                    return; 
                } 
 
                PerformPivot(tableau, pivotRow, pivotCol, rows, cols); 
            } 
        } 
 
        static int FindPivotColumn(double[,] tableau, int cols) 
        { 
            int pivotCol = -1; 
            double minValue = 0; 
 
            for (int j = 0; j < cols - 1; j++) 
            { 
                if (tableau[tableau.GetLength(0) - 1, j] < minValue) 
                { 
                    minValue = tableau[tableau.GetLength(0) - 1, j]; 
                    pivotCol = j; 
                } 
            } 
 
            return pivotCol; 
        } 
 
        static int FindPivotRow(double[,] tableau, int pivotCol, int rows) 
        { 
            int pivotRow = -1; 
            double minRatio = double.MaxValue; 
 
            for (int i = 0; i < rows - 1; i++) 
            { 
                if (tableau[i, pivotCol] > 0) 
                { 
                    double ratio = tableau[i, tableau.GetLength(1) - 1] / 
tableau[i, pivotCol]; 
                    if (ratio < minRatio) 
                    { 
                        minRatio = ratio; 
                        pivotRow = i; 
                    } 
                } 
            } 
 
            return pivotRow; 
        } 
 
        static void PerformPivot(double[,] tableau, int pivotRow, int 
pivotCol, int rows, int cols) 
        { 
            double pivotValue = tableau[pivotRow, pivotCol]; 
 
            for (int j = 0; j < cols; j++) 
            { 
                tableau[pivotRow, j] /= pivotValue; 
            }

for (int i = 0; i < rows; i++) 
            { 
                if (i != pivotRow) 
                { 
                    double factor = tableau[i, pivotCol]; 
                    for (int j = 0; j < cols; j++) 
                    { 
                        tableau[i, j] -= factor * tableau[pivotRow, j]; 
                    } 
                } 
            } 
        } 
 
        static void PrintSolution(double[,] tableau, int numVariables, int 
numConstraints) 
        { 
            double[] solution = new double[numVariables]; 
 
            for (int i = 0; i < numConstraints; i++) 
            { 
                for (int j = 0; j < numVariables; j++) 
                { 
                    if (tableau[i, j] == 1) 
                    { 
                        solution[j] = tableau[i, tableau.GetLength(1) - 1]; 
                        break; 
                    } 
                } 
            } 
 
            Console.WriteLine("Оптимальное решение:"); 
            for (int i = 0; i < numVariables; i++) 
            { 
                Console.WriteLine($"x{i + 1} = {solution[i]}"); 
            } 
 
            Console.WriteLine("Оптимальное значение целевой функции: " + 
tableau[tableau.GetLength(0) - 1, tableau.GetLength(1) - 1]); 
        } 
    } 
 
 
}


