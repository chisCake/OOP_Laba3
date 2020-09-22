using System;
using System.Security.Cryptography.X509Certificates;

<<<<<<< HEAD
namespace OOP_Laba3 {
=======
namespace Laba3 {
>>>>>>> 0f2594825fde0a58ac71941cc0caeec6336c9514
	partial class Abiturient {
		// Не нашли применения

		// Закрытый конструктор
		private Abiturient(string id) {
			Id = id;
			Surname = "Не указана";
			Name = "Не указано";
			Patronymic = "Не указано";

			hashId = GetHashId();
			Counter++;
		}

		// Поле только для чтения
		private readonly int hashId = 0;

		private int GetHashId() {
			int hash1 = Id.GetHashCode();
			int hash2 = Surname.GetHashCode();
			int hash = (hash1 * hash2).GetHashCode();
			return hash;
		}

		// Поле константа
		const int constantin = 1;

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return GetHashId();
		}

		public override string ToString() {
			return $"{Surname} {Name}";
		}
	}
}
