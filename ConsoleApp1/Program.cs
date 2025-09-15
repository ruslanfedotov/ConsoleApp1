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

        public override string ToString() => $"{Name}; {Amount} рублей";
    }

    class Program
    {
        static void Main()
        {
            List<Expense> expenses = new List<Expense>();

            Console.WriteLine("Введите количество операций (2-40):");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 2 || n > 40)
                Console.WriteLine("Неверное значение. Введите число от 2 до 40:");

            Console.WriteLine("Введите траты по шаблону: (Название; Сумма)");
            for (int i = 0; i < n; i++)
            {
                string line;
                do
                {
                    line = Console.ReadLine().Trim();
                    if (line.StartsWith("(") && line.EndsWith(")"))
                    {
                        line = line.Substring(1, line.Length - 2).Trim();
                        string[] parts = line.Split(';');
                        if (parts.Length == 2 && double.TryParse(parts[1].Trim(), out double amount) && amount > 0)
                        {
                            expenses.Add(new Expense(parts[0].Trim(), amount));
                            break;
                        }
                    }
                    Console.WriteLine("Неверный формат. Введите по шаблону: (Название; Сумма)");
                } while (true);
            }

            while (true)
            {
                Console.WriteLine("\nМеню:\n1. Вывод данных\n2. Статистика\n3. Сортировка по цене\n4. Конвертация валюты\n5. Поиск по названию\n0. Выход");
                Console.Write("Выберите пункт: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        expenses.ForEach(e => Console.WriteLine(e));
                        break;
                    case "2":
                        if (expenses.Count == 0) Console.WriteLine("Нет данных.");
                        else Console.WriteLine($"Сумма: {expenses.Sum(e => e.Amount)} рубле\nСреднее: {expenses.Average(e => e.Amount):F2} рубле\nМаксимум: {expenses.Max(e => e.Amount)} рубле\nМинимум: {expenses.Min(e => e.Amount)} рубле");
                        break;
                    case "3":
                        BubbleSort(expenses);
                        Console.WriteLine("Данные отсортированы по цене:");
                        expenses.ForEach(e => Console.WriteLine(e));
                        break;
                    case "4":
                        Console.WriteLine("Выберите валюту:\n1. доллар (90 руб)\n2. евро (100 руб)\n3. Свой курс");
                        string choice = Console.ReadLine();
                        double rate = choice == "1" ? 90 : choice == "2" ? 100 : 0;
                        string currency = choice == "1" ? "доллар" : choice == "2" ? "евро" : "";

                        if (choice == "3")
                        {
                            Console.Write("Введите курс: ");
                            if (!double.TryParse(Console.ReadLine(), out rate) || rate <= 0)
                            {
                                Console.WriteLine("Неверный курс.");
                                continue;
                            }
                            Console.Write("Введите название валюты: ");
                            currency = Console.ReadLine().Trim();
                        }
                        else if (rate == 0)
                        {
                            Console.WriteLine("Неверный выбор.");
                            continue;
                        }

                        foreach (var e in expenses)
                            Console.WriteLine($"{e.Name}; {e.Amount / rate:F2} {currency}");
                        break;
                    case "5":
                        Console.Write("Введите название для поиска: ");
                        string keyword = Console.ReadLine().Trim();
                        var found = expenses.Where(e => e.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                        Console.WriteLine(found.Count > 0 ? string.Join("\n", found) : "Ничего не найдено.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void BubbleSort(List<Expense> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
                for (int j = 0; j < list.Count - i - 1; j++)
                    if (list[j].Amount > list[j + 1].Amount)
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
        }
    }
}
