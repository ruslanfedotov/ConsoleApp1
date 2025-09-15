using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyExpenses
{
    public class Expense
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public Expense(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Name}; {Amount} рубле";
        }
    }

    class Program
    {
        static void Main()
        {
            List<Expense> expenses = new List<Expense>();

            Console.WriteLine("Введите количество операций (от 2 до 40):");
            int n;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out n) && n >= 2 && n <= 40)
                {
                    break;
                }
                Console.WriteLine("Неверное значение. Введите число от 2 до 40:");
            }

            Console.WriteLine("Введите траты по шаблону: (Название; Сумма)");
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    string line = Console.ReadLine().Trim();

                    if (line.StartsWith("(") && line.EndsWith(")"))
                    {
                        string content = line.Substring(1, line.Length - 2).Trim();
                        string[] parts = content.Split(';');

                        if (parts.Length == 2)
                        {
                            string name = parts[0].Trim();
                            string amountStr = parts[1].Trim();

                            if (double.TryParse(amountStr, out double amount) && amount > 0)
                            {
                                expenses.Add(new Expense(name, amount));
                                break;
                            }
                        }
                    }

                    Console.WriteLine("Неверный формат. Введите по шаблону: (Название; Сумма)");
                }
            }

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Сортировка по цене");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\nДанные:");
                    foreach (var expense in expenses)
                    {
                        Console.WriteLine(expense);
                    }
                }
                else if (choice == "2")
                {
                    if (expenses.Count == 0)
                    {
                        Console.WriteLine("Нет данных.");
                    }
                    else
                    {
                        double sum = expenses.Sum(e => e.Amount);
                        double average = sum / expenses.Count;
                        double max = expenses.Max(e => e.Amount);
                        double min = expenses.Min(e => e.Amount);

                        Console.WriteLine("\nСтатистика:");
                        Console.WriteLine($"Сумма: {sum} рубле");
                        Console.WriteLine($"Среднее: {average:F2} рубле");
                        Console.WriteLine($"Максимум: {max} рубле");
                        Console.WriteLine($"Минимум: {min} рубле");
                    }
                }
                else if (choice == "3")
                {
                    BubbleSort(expenses);
                    Console.WriteLine("\nДанные отсортированы по цене:");
                    foreach (var expense in expenses)
                    {
                        Console.WriteLine(expense);
                    }
                }
                else if (choice == "4")
                {
                    Console.WriteLine("\nВыберите валюту для конвертации:");
                    Console.WriteLine("1. доллар (курс: 90 рубле за 1 доллар)");
                    Console.WriteLine("2. евро (курс: 100 рубле за 1 евро)");
                    Console.WriteLine("3. Ввести свой курс");
                    Console.Write("Выбор: ");

                    string currencyChoice = Console.ReadLine();
                    double rate = 1.0;
                    string currencyName = "";

                    if (currencyChoice == "1")
                    {
                        rate = 90.0;
                        currencyName = "доллар";
                    }
                    else if (currencyChoice == "2")
                    {
                        rate = 100.0;
                        currencyName = "евро";
                    }
                    else if (currencyChoice == "3")
                    {
                        Console.Write("Введите курс: ");
                        string rateInput = Console.ReadLine();
                        if (double.TryParse(rateInput, out rate) && rate > 0)
                        {
                            Console.Write("Введите название валюты: ");
                            currencyName = Console.ReadLine().Trim();
                        }
                        else
                        {
                            Console.WriteLine("Неверный курс.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор.");
                        continue;
                    }

                    Console.WriteLine($"\nКонвертированные данные в {currencyName}:");
                    foreach (var expense in expenses)
                    {
                        double convertedAmount = expense.Amount / rate;
                        Console.WriteLine($"{expense.Name}; {convertedAmount:F2} {currencyName}");
                    }
                }
                else if (choice == "5")
                {
                    Console.Write("Введите название для поиска: ");
                    string searchTerm = Console.ReadLine().Trim();

                    List<Expense> foundExpenses = new List<Expense>();
                    foreach (var expense in expenses)
                    {
                        if (expense.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            foundExpenses.Add(expense);
                        }
                    }

                    if (foundExpenses.Count > 0)
                    {
                        Console.WriteLine("\nНайденные траты:");
                        foreach (var expense in foundExpenses)
                        {
                            Console.WriteLine(expense);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ничего не найдено.");
                    }
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор.");
                }
            }
        }

        static void BubbleSort(List<Expense> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].Amount > list[j + 1].Amount)
                    {
                        Expense temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }
}
