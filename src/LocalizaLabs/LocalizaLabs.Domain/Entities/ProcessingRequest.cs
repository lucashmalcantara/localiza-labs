using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLabs.Domain.Entities
{
    public class ProcessingRequest
    {
        public Guid NumberProcessingId { get; set; }
        public bool Ready { get; set; }

        public ProcessingRequest(Guid numberProcessingId)
        {
            NumberProcessingId = numberProcessingId;
            Ready = false;
        }
    }
}
