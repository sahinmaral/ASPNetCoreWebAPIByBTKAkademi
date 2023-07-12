using System.Runtime.Serialization;

namespace StoreApp.Entities.Models.RequestFeatures
{
	public class BookParameters : RequestParameters
	{
		public uint MinPrice { get; init; } = 10;
		public uint MaxPrice { get; init; } = 1000;
		public string? SearchTerm { get; init; }
	}
}
