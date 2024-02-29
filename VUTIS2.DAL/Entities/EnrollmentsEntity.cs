using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUTIS2.DAL.Entities
{
    public record EnrollmentsEntity : IEntity
    {
        public Guid Id { get; set; }

        public required Guid StudentId { get; set; }
        public required Guid SubjectId { get; set; }

        public StudentEntity? Student { get; init; }
        public SubjectEntity? Subject { get; init; }
    }
}
