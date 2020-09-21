using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace OOP_Laba3 {
	/*

		Создать класс Abiturient: 
		id, Фамилия, Имя, Отчество, Адрес, Телефон, массив оценок.
		Свойства и конструкторы должны обеспечивать проверку корректности.
		Добавить методы:
			расчёта среднего балла,
			поиска максимального и минимального балла
		Создать массив объектов. Вывести:
		a) список абитуриентов, имеющих неудовлетворительные оценки;
		b) список абитуриентов, у которых сумма баллов выше
		заданной;

	*/

	partial class Abiturient {
		// Список всех абитуриентов
		public static List<Abiturient> List { get; private set; }
		// Кол-во созданных абитуриентов
		public static int Counter { get; private set; }

		// Данные об абитуриенте
		[JsonProperty("id")]
		public string Id { get; private set; }
		[JsonProperty("surname")]
		public string Surname { get; private set; }
		[JsonProperty("name")]
		public string Name { get; private set; }
		[JsonProperty("patronymic")]
		public string Patronymic { get; private set; }
		[JsonProperty("phone")]
		public string Phone { get; private set; }
		[JsonProperty("address")]
		public Address Address { get; private set; }
		// Словарь (он же по заданию массив) с оценками
		[JsonProperty("Marks")]
		public Dictionary<string, int?> Marks { get; private set; }

		public void InputMarks(Dictionary<string, int?> marks) {
			if (Marks == null)
				Marks = new Dictionary<string, int?>(marks);
			else
				foreach (var item in marks) {
					if (Marks.ContainsKey(item.Key))
						Marks[item.Key] = item.Value;
					else
						Marks.Add(item.Key, item.Value);
				}
		}

		//Статический конструктор
		static Abiturient() {
			// Загрузка сохранённых студентов из json
			List = DB.Read();
			if (List == null)
				List = new List<Abiturient>();
		}

		// Конструктор без параметров
		public Abiturient() {
			Id = "Не указан";
			Surname = "Не указана";
			Name = "Не указано";
			Patronymic = "Не указано";
			Counter++;
		}

		// Конструктор с параметрами
		public Abiturient(string id, string surname, string name, string patronymic, string phone,Address address, Dictionary<string, int?> marks) {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Phone = phone;
			Address = address;
			Marks = new Dictionary<string, int?>(marks);
			Counter++;
		}

		// Конструктор с параметрами по умолчанию
		public Abiturient(ref string id, ref string surname, ref string name, string patronymic = "Не указано") {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Phone = "Не указан";
			Address = new Address();
			Marks = new Dictionary<string, int?>();
			Counter++;
		}

		// Перезапись списка абитуриентов
		public static void ReWrite(List<Abiturient> abiturients) {
			if (List == null)
				List = new List<Abiturient>(abiturients);
			else
				List = abiturients;
			DB.Write(List);
		}

		// Средний балл
		public double GetAvg() {
			if (Marks == null)
				return 0;
			var avg = Marks.Values.Average();
			if (avg.HasValue)
				return avg.Value;
			else
				return 0;
		}

		// Проверка ID
		public static void IsIdFree(string id, out bool isFree ) {
			isFree = true;
			foreach (var item in List) {
				if (item.Id == id) {
					isFree = false;
					break;
				}
			}
		}

		// Добавление абитуриента
		public static void Add(Abiturient abiturient) {
			List.Add(abiturient);
			ReWrite(List);
		}

		// Удаление абитуриента
		public static void Delete(Abiturient abiturient) {
			List.Remove(abiturient);
			ReWrite(List);
		}

		// Поиск студента по id или фамилии
		public static List<Abiturient> GetAbiturient(string data) {
			var result = new List<Abiturient>();
			foreach (var item in List) {
				if (item.Id == data) {
					result.Add(item);
					break;
				}
				if (item.Surname == data)
					result.Add(item);
			}
			return result;
		}

		// Список с неудовлетворительными оценками
		public static List<Abiturient> GetUnderperforming() {
			var list = new List<Abiturient>();
			foreach (var item in List) {
				if (item.Marks.Values.Any(v => v < 4))
					list.Add(item);
			}
			return list;
		}

		// Список с суммой оценок выше заданной
		public static List<Abiturient> GetMoreThan(int userSum) {
			var list = new List<Abiturient>();
			foreach (var item in List) {
				if (item.Marks.Values.Sum() > userSum)
					list.Add(item);
			}
			return list;
		}

		public enum PrintType {
			Short,
			Full
		}

		public static void PrintList(List<Abiturient> list, PrintType type) {
			int counter = 0;
			switch (type) {
				case PrintType.Short:
					Console.WriteLine("  №      ID           Фамилия             Имя          Средний балл\n");
					foreach (var item in list) {
						Console.WriteLine($"{++counter,4})  {item.Id,6}      {item.Surname,-13}      {item.Name,-11}      {item.GetAvg(),8:F2}");
					}
					break;
				case PrintType.Full:
					foreach (var item in list) {
						Console.WriteLine(
						$"\n{++counter,4})    ID: {item.Id}" +
						$"\nФИО: {item.Surname} {item.Name} {item.Patronymic}" +
						$"\nТелефон: {item.Phone}" +
						$"\nСредний балл: {item.GetAvg(),3:F2}" +
						$"\nМинимальный/максимальный балл: {item.Marks.Values.Min()}/{item.Marks.Values.Max()}" +
						$"\nАдрес: г. {item.Address.City}, ул. {item.Address.Street}, д. {item.Address.House}, кв. {item.Address.Flat}" +
						$"\nСписок оценок:"
						);
						foreach (var mark in item.Marks) {
							Console.WriteLine(
								$"{mark.Key,-24}{mark.Value}");
						}
					}
					break;
				default:
					break;
			}
		}
	}
}
