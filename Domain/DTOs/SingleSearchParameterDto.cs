using System.Security.Cryptography;

namespace Domain.DTOs
{
    public class SingleSearchParameterDto
    {
        public string? SearchParameter { get;  }

        public SingleSearchParameterDto(string? searchParameter)
        {
            SearchParameter = searchParameter;
        }
    }
}