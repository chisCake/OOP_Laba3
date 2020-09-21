using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OOP_Laba3 {
	class Generator {
		private static Random rndm = new Random();
		private static List<Abiturient> abiturients = new List<Abiturient>();

		// Сырые данные
		private static List<string> surnames = new List<string>();
		private static List<string> names = new List<string>();
		private static List<string> patronymics = new List<string>();
		private static List<string> cities = new List<string>();
		private static List<string> streets = new List<string>();

		// Статус загруженности сырых данных
		public static bool Loaded { get; private set; }

		static Generator() {
			Loaded = false;
		}

		// Загрузка сырых данных из файлов
		private static void LoadSurnames() {
			using var sr = new StreamReader("surnames.txt");
			string surname;
			while ((surname = sr.ReadLine()) != null)
				surnames.Add(surname);
		}

		private static void LoadNames() {
			using var sr = new StreamReader("names.txt");
			string name;
			while ((name = sr.ReadLine()) != null)
				names.Add(name);
		}

		private static void LoadPatronymics() {
			using var sr = new StreamReader("patronymics.txt");
			string patronymic;
			while ((patronymic = sr.ReadLine()) != null)
				patronymics.Add(patronymic);
		}

		private static void LoadCities() {
			using var sr = new StreamReader("citiesBel.txt");
			string city;
			while ((city = sr.ReadLine()) != null)
				cities.Add(city);
		}

		private static void LoadStreets() {
			using var sr = new StreamReader("streetsBel.txt");
			string street;
			while ((street = sr.ReadLine()) != null)
				streets.Add(street);
		}

		private static void LoadBlanks() {
			LoadSurnames();
			LoadNames();
			LoadPatronymics();
			LoadCities();
			LoadStreets();
			Loaded = true;
		}

		// Генерация абитуриентов
		public static void Generate(int amt) {
			if (!Loaded)
				LoadBlanks();
			abiturients.Clear();

			string id, surname, name, patronymic, phone;
			Address address;

			for (int i = 0; i < amt; i++) {
				bool pass;
				// Генерация id
				do {
					pass = true;
					id = rndm.Next(100000, 1000000).ToString();
					// Проверка на повторяемость
					foreach (var item in abiturients) {
						if (item.Id == id) {
							pass = false;
							break;
						}
					}
				} while (!pass);

				// Генерация ФИО
				surname = surnames[rndm.Next(surnames.Count)];
				name = names[rndm.Next(names.Count)];
				patronymic = patronymics[rndm.Next(patronymics.Count)];

				// Генерация номера
				int code = rndm.Next(4);
				phone = $"+375{(code == 0 ? 25 : code == 1 ? 29 : code == 2 ? 33 : 44)}" +
					$"{(code == 0 ? rndm.Next(10000000) : rndm.Next(2) == 0 ? rndm.Next(1000000, 4000000) : rndm.Next(5000000, 10000000))}";

				// Генерация адреса
				string city = cities[rndm.Next(cities.Count)];
				string street = streets[rndm.Next(cities.Count)];
				int house = rndm.Next(51);
				int flat = rndm.Next(37);
				address = new Address(city, street, house.ToString(), flat.ToString());

				// Генерация оценок
				int[] minVals = { 1, 4, 7 }, maxVals = { 6, 8, 10 };
				// Определение уровня интеллекта
				int iqBorder = rndm.Next(1, 6),
					iqLvl = iqBorder == 1 ? 0 : iqBorder == 5 ? 2 : 1;
				// Создание словаря
				var marks = new Dictionary<string, int?> {
					{"ДиЮПИ", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Печатные процессы", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Математика", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"КС", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"ООП", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Дискретная математика", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"ИГиГ", rndm.Next(minVals[iqLvl], maxVals[iqLvl])}
				};

				var abiturient = new Abiturient(id, surname, name, patronymic, phone, address, marks);
				abiturients.Add(abiturient);
			}

			DB.Write(abiturients);
			Abiturient.ReWrite(abiturients);
		}

	}
}