using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3 {
	class Program {
		private static List<Abiturient> abiturients = Abiturient.AbiturientList;

		static void Main() {
			// Генерация новых данных, если сохранённых нет
			if (abiturients == null) {
				Generator.Generate(10);
				abiturients = Abiturient.AbiturientList;
			}

			while (true) {
				Console.Clear();
				Console.Write(
					"1 - список абитуриентов" +
					"\n2 - добавить абитуриента" +
					"\n3 - удалить абитуриента" +
					"\n4 - вывести подробную информацию об абитуриенте" +
					"\n5 - средний балл абитуриента" +
					"\n6 - минимальный и максимальный балл студента" +
					"\n7 - список абитуриентов, имеющих неудовлетворительные оценки" +
					"\n8 - список абитуриентов, у которых сумма баллов выше заданной" +
					"\n9 - сгенерировать новый список" +
					"\n0 - выход" +
					"\nВыберите действие: ");

				if (!int.TryParse(Console.ReadLine(), out int choice)) {
					Console.WriteLine("Нет такого варианта");
					Console.ReadKey();
					continue;
				}

				switch (choice) {
					case 1:
						PrintList();
						break;
					case 2:
						break;
					case 3:
						break;
					case 4:
						break;
					case 5:
						break;
					case 6:
						break;
					case 7:
						break;
					case 9:
						Console.Write("Введите кол-во абитуриентов: ");
						if (int.TryParse(Console.ReadLine(), out int amt)) {
							Generator.Generate(amt);
							abiturients = Abiturient.AbiturientList;
							Console.WriteLine("Новый список абитуриентов сгенерирован");
						}
						else
							Console.WriteLine("Значение должно быть целочисленным");
						break;
					case 0:
						return;
				}
				Console.ReadKey();
			}
		}

		// Вывод списка
		private static void PrintList() {
			Console.WriteLine("  №      ID           Фамилия             Имя             Отчество          Средний балл\n");
			int counter = 0;
			foreach (var item in abiturients) {
				Console.WriteLine($"{++counter,4})  {item.Id,6}|    |{item.Surname,-13}|    |{item.Name,-11}|    |{item.Patronymic,-16}|    |{item.GetAvg(),8:F2}");
			}
		}
	}
}
