using System;
using System.Collections.Generic;

namespace OOP_Laba3 {
	class Program {
		private static List<Abiturient> abiturients = Abiturient.List;

		static void Main() {
			// Генерация новых данных, если сохранённых нет
			if (abiturients == null) {
				Generator.Generate(10);
				abiturients = Abiturient.List;
			}

			while (true) {
				Console.Clear();
				Console.Write(
					"1 - список абитуриентов" +
					"\n2 - добавить абитуриента" +
					"\n3 - удалить абитуриента" +
					"\n4 - вывести подробную информацию об абитуриенте" +
					"\n5 - список абитуриентов, имеющих неудовлетворительные оценки" +
					"\n6 - список абитуриентов, у которых сумма баллов выше заданной" +
					"\n7 - сгенерировать новый список" +
					"\n0 - выход" +
					"\nВыберите действие: ");

				if (!int.TryParse(Console.ReadLine(), out int choice)) {
					Console.WriteLine("Нет такого варианта");
					Console.ReadKey();
					continue;
				}

				switch (choice) {
					case 1:
						Abiturient.PrintList(Abiturient.List, Abiturient.PrintType.Short);
						break;
					case 2:
						AddAbiturien();
						break;
					case 3:
						DeleteAbiturien();
						break;
					case 4:
						AbiturientInfo();
						break;
					case 5:
						FindUnderperforming();
						break;
					case 6:
						FindWithSum();
						break;
					case 7:
						Console.Write("Введите кол-во абитуриентов: ");
						if (int.TryParse(Console.ReadLine(), out int amt)) {
							Generator.Generate(amt);
							abiturients = Abiturient.List;
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

		// Добавление абитуриента
		private static void AddAbiturien() {
			Abiturient abiturient;
			string id, surname, name;
			Console.Write(
				"\n1 - ввести полную информацию об абитуриенте" +
				"\n2 - ввести краткие сведение об абитуриенте" +
				"\nВыберите действие: ");
			string opt = Console.ReadLine();
			if (opt != "1" && opt != "2") {
				Console.WriteLine("Нет такого варианта");
				return;
			}

			Console.Write(
				"\n1 - сгенерировать ID автоматически" +
				"\n2 - ввести ID вручную" +
				"\nВыберите действие: ");
			switch (Console.ReadLine()) {
				case "1":
					var rndm = new Random();
					do {
						string tid = rndm.Next(100000, 1000000).ToString();
						Abiturient.IsIdFree(tid, out bool status);
						if (status) {
							id = tid;
							break;
						}
					} while (true);
					Console.WriteLine($"Случайно сгенерированный ID: {id}");
					break;
				case "2":
					do {
						Console.Write("Введите ID: ");
						id = Console.ReadLine();
						Abiturient.IsIdFree(id, out bool status);
						if (status)
							break;
						else
							Console.WriteLine("Абитуриент с таким ID уже зарегестрирован");
					} while (true);
					break;
				default:
					Console.WriteLine("Нет такого варианта");
					return;
			}

			Console.Write("\nВведите фамилию: ");
			surname = Console.ReadLine();
			Console.Write("Введите имя: ");
			name = Console.ReadLine();

			switch (opt) {
				case "1":
					string patronymic, phone, city, street, house, flat;
					Console.Write("Введите отчество: ");
					patronymic = Console.ReadLine();
					Console.Write("Введите телефон: +375");
					phone = "+375" + Console.ReadLine();
					Console.Write("Введите город: ");
					city = Console.ReadLine();
					Console.Write("Введите улицу: ");
					street = Console.ReadLine();
					Console.Write("Введите номер дома: ");
					house = Console.ReadLine();
					Console.Write("Введите номер квартиры: ");
					flat = Console.ReadLine();
					Address address = new Address(city, street, house, flat);
					var marks = new Dictionary<string, int?>();
					abiturient = new Abiturient(id, surname, name, patronymic, phone, address, marks);
					Abiturient.Add(abiturient);
					Console.WriteLine("Абитуриент добавлен");
					break;
				case "2":
					abiturient = new Abiturient(ref id, ref surname, ref name);
					Abiturient.Add(abiturient);
					Console.WriteLine("Абитуриент добавлен");
					break;
				default:
					Console.WriteLine("Нет такого варианта");
					break;
			}

		}

		// Удаление абитуриента
		private static void DeleteAbiturien() {
			Abiturient.PrintList(Abiturient.List, Abiturient.PrintType.Short);
			Console.Write("Введите ID абитуриента для удаления: ");
			string id = Console.ReadLine();
			Abiturient.IsIdFree(id, out bool status);
			if (status)
				Console.WriteLine("Абитуриент с данным ID не найден");
			else {
				foreach (var item in Abiturient.List) {
					if (item.Id == id) {
						Abiturient.Delete(item);
						Console.WriteLine("Абитуриент удалён");
						break;
					}
				}
			}
		}

		// Подробная информация об абитуриенте
		// Минимальный, средний, максимальный балл
		private static void AbiturientInfo() {
			Abiturient.PrintList(Abiturient.List, Abiturient.PrintType.Short);
			Console.Write("\nВведите ID или фамилию абитуриента для поиска: ");
			string data = Console.ReadLine();
			List<Abiturient> result = Abiturient.GetAbiturient(data);
			if (result.Count == 0) {
				Console.WriteLine("Абитуриент с заданным ID/фамилией не найдено");
			}
			else if (result.Count == 1) {
				Console.WriteLine("Подходящий абитуриент найден: ");
				Abiturient.PrintList(result, Abiturient.PrintType.Full);
			}
			else {
				Console.WriteLine("Найдено несколько совпадений: ");
				Abiturient.PrintList(result, Abiturient.PrintType.Full);
			}
		}

		// Абитуриенты с неуд оценками
		private static void FindUnderperforming() {
			var list = Abiturient.GetUnderperforming();
			if (list.Count == 0)
				Console.WriteLine($"Абитуриентов с неудовлетворяющими оценками не найдено");
			else {
				Console.WriteLine($"Кол-во найденных абитуриентов с неудовлетворяющими оценками: {list.Count}");
				Abiturient.PrintList(list, Abiturient.PrintType.Short);
			}
		}

		// Абитуриенты с оценками выше заданной
		private static void FindWithSum() {
			int sum;
			do {
				Console.Write("Введите сумму всех баллов: ");
				if (int.TryParse(Console.ReadLine(), out sum))
					break;
			} while (true);
			var list = Abiturient.GetMoreThan(sum);
			if (list.Count == 0)
				Console.WriteLine($"Абитуриентов с суммой оценок выше {sum} не найдено");
			else {
				Console.WriteLine($"Кол-во найденных абитуриентов у которых сумма всех оценок выше {sum}: {list.Count}");
				Abiturient.PrintList(list, Abiturient.PrintType.Short);
			}
		}
	}
}
