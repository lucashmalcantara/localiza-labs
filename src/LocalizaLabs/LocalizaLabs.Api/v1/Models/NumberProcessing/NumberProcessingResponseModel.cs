using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLabs.Api.v1.Models.NumberProcessing
{
    public class NumberProcessingResponseModel
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public List<long> Divisors { get; private set; }
        public List<long> PrimeNumbers { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime? End { get; set; }
    }
}
