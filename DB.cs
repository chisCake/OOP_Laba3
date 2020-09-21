using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace Laba3 {
	class DB {
		public static void Write(List<Abiturient> abiturients) {
			using var sw = new StreamWriter("abiturients.json");
			string json = JsonConvert.SerializeObject(abiturients);
			sw.WriteLine(json);
		}

		public static List<Abiturient> Read() {
			if (!File.Exists("abiturients.json")) return null;
			using var sr = new StreamReader("abiturients.json");
			string json = sr.ReadToEnd();
			var abiturients = JsonConvert.DeserializeObject<List<Abiturient>>(json);
			return abiturients;
		} 
	}
}
