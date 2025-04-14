
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace MathModeling1 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            Console.WriteLine("Добро пожаловать в программу для решения 
транспортной задачи!"); 
            Console.WriteLine("Выберите метод построения опорного плана:"); 
            Console.WriteLine("1 - Метод северо-западного угла"); 
            Console.WriteLine("2 - Метод минимальных элементов"); 
 
            int choice; 
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice 
!= 1 && choice != 2)) 
            { 
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 
или 2."); 
            } 
 
            Console.WriteLine("Введите количество поставщиков:"); 
            int m = int.Parse(Console.ReadLine()); 
            Console.WriteLine("Введите количество потребителей:"); 
            int n = int.Parse(Console.ReadLine()); 
 
            Console.WriteLine("Введите запасы поставщиков (через пробел):"); 
            int[] supplies = Array.ConvertAll(Console.ReadLine().Split(' '), 
int.Parse); 
 
            Console.WriteLine("Введите потребности потребителей (через 
пробел):"); 
            int[] demands = Array.ConvertAll(Console.ReadLine().Split(' '), 
int.Parse); 
 
            Console.WriteLine("Введите матрицу стоимостей (через пробелы и 
переносы строк):"); 
            int[,] costs = new int[m, n]; 
            for (int i = 0; i < m; i++) 
            { 
                int[] row = Array.ConvertAll(Console.ReadLine().Split(' '), 
int.Parse); 
                for (int j = 0; j < n; j++) 
                { 
                    costs[i, j] = row[j]; 
                } 
            } 
 
            int[,] result; 
            if (choice == result = NorthWestCornerMethod(supplies, demands, m, n); 
            } 
            else 
            { 
                result = LeastCostMethod(supplies, demands, costs, m, n); 
            } 
 
            Console.WriteLine("Результирующий опорный план:"); 
            for (int i = 0; i < m; i++) 
            { 
                for (int j = 0; j < n; j++) 
                { 
                    Console.Write(result[i, j] + " "); 
                } 
                Console.WriteLine(); 
            } 
 
            int totalCost = CalculateTotalCost(result, costs, m, n); 
            Console.WriteLine($"Общие затраты: {totalCost}"); 
        } 
 
        static int[,] NorthWestCornerMethod(int[] supplies, int[] demands, 
int m, int n) 
        { 
            int[,] plan = new int[m, n]; 
            int i = 0, j = 0; 
 
            while (i < m && j < n) 
            { 
                int allocation = Math.Min(supplies[i], demands[j]); 
                plan[i, j] = allocation; 
                supplies[i] -= allocation; 
                demands[j] -= allocation; 
 
                if (supplies[i] == 0) i++; 
                else j++; 
            } 
 
            return plan; 
        } 
 
        static int[,] LeastCostMethod(int[] supplies, int[] demands, int[,] 
costs, int m, int n) 
        { 
            int[,] plan = new int[m, n]; 
            bool[,] used = new bool[m, n]; 
 
            while (true) 
            { 
                int minCost = int.MaxValue; 
                int minI = -1, minJ = -1; 
 
                for (int i = 0; i < m; i++) 
                { 
                    for (int j = 0; j < n; j++) 
                    { 
                        if (!used[i, j] && costs[i, j] < minCost) 
                        { 
                            minCost = costs[i, j]; 
                            minI = i; 
                            minJ = j; 
                        } 
                    } 
                } 
 
                if (minI == -1) break; 
 
                int allocation = Math.Min(supplies[minI], demands[minJ]); 
                plan[minI, minJ] = allocation; 
                supplies[minI] -= allocation; 
                demands[minJ] -= allocation; 
                used[minI, minJ] = true; 
 
                if (supplies[minI] == 0) 
                { 
                    for (int j = 0; j < n; j++) 
                    { 
                        used[minI, j] = true; 
                    } 
                } 
 
                if (demands[minJ] == 0) 
                { 
                    for (int i = 0; i < m; i++) 
                    { 
                        used[i, minJ] = true; 
                    } 
                } 
            } 
 
            return plan; 
        } 
 
        static int CalculateTotalCost(int[,] plan, int[,] costs, int m, int 
n) 
        { 
            int totalCost = 0; 
            for (int i = 0; i < m; i++) 
            { 
                for (int j = 0; j < n; j++) 
                { 
                    totalCost += plan[i, j] * costs[i, j]; 
                } 
            } 
            return totalCost;
}}}
