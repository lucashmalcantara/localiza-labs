using LocalizaLabs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLabs.Infrastructure.Persistence.Data
{
    public sealed class SimulatedPersistence
    {
        private static readonly SimulatedPersistence instance = new SimulatedPersistence();

        public Queue<ProcessingRequest> ProcessingRequestQueue { get; private set; }
        public List<NumberProcessingResult> NumberProcessingResults { get; set; }

        private SimulatedPersistence()
        {
            ProcessingRequestQueue = new Queue<ProcessingRequest>();
            NumberProcessingResults = new List<NumberProcessingResult>();
        }

        static SimulatedPersistence()
        {
        }

        public static SimulatedPersistence Instance
        {
            get { return instance; }
        }
    }
}
