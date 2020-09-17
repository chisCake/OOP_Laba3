using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace Laba3 {
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

	class Abiturient {
		// Список всех абитуриентов
		public static List<Abiturient> AbiturientList { get; private set; }

		// Данные об абитуриенте
		[JsonProperty("id")]
		public string Id { get; private set; }
		[JsonProperty("surname")]
		public string Surname { get; private set; }
		[JsonProperty("name")]
		public string Name { get; private set; }
		[JsonProperty("patronymic")]
		public string Patronymic { get; private set; }
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
			AbiturientList = DB.Read();
		}

		// Конструктор без параметров
		public Abiturient() {
			Id = "Не указан";
			Surname = "Не указана";
			Name = "Не указано";
			Patronymic = "Не указано";
		}

		// Конструктор с параметрами
		public Abiturient(string id, string surname, string name, string patronymic, Address address, Dictionary<string, int?> marks) {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Address = address;
			Marks = new Dictionary<string, int?>(marks);
		}

		// Конструктор с параметрами по умолчанию
		public Abiturient(string id, string surname, string name, string patronymic = "Не указано") {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Address = new Address();
		}

		// Закрытый конструктор
		private Abiturient(string id) {
			Id = id;
			Surname = "Не указана";
			Name = "Не указано";
			Patronymic = "Не указано";
		}

		// Перезапись списка абитуриентов
		public static void ReWrite(List<Abiturient> abiturients) {
			if (AbiturientList == null)
				AbiturientList = new List<Abiturient>(abiturients);
			else
				AbiturientList = abiturients;
		}

		// Средний балл
		public double GetAvg() {
			var avg = Marks.Values.Average();
			if (avg.HasValue)
				return avg.Value;
			else
				return 0;
		}
	}
}
