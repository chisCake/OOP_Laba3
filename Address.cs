using Newtonsoft.Json;

namespace OOP_Laba3 {
	class Address {
		[JsonProperty("city")]
		public string City { get; private set; }
		[JsonProperty("street")]
		public string Street { get; private set; }
		[JsonProperty("house")]
		public string House { get; private set; }
		[JsonProperty("flat")]
		public string Flat { get; private set; }

		public Address() {
			City = "Не указан";
			Street = "Не указана";
			House  = "Не указан";
			Flat = "Не указана";
		}

		public Address(string city, string street, string house, string flat) {
			City = city;
			Street = street;
			House = house;
			Flat = flat;
		}
	}
}
