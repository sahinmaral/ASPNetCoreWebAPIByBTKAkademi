namespace StoreApp.Entities.Models.Exceptions
{
    public class PriceOutOfRangeBadRequestException : BadRequestException
    {
        public PriceOutOfRangeBadRequestException() : base("Maximum price should be between 10 and 1000 and greater than minumum price")
        {
        }
    }
}
