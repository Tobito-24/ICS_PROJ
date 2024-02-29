using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUTIS2.DAL.Entities
{
    public record EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public required int Points { get; set; }

        public string? Description { get; set; }

        public required ActivityEntity Activity { get; set; }

        public required StudentEntity Student { get; set; }
    }
}
