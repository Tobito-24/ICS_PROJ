using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUTIS2.DAL.Entities
{
    public record StudentEntity : IEntity
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public string? PhotoUrl { get; set; }

        public ICollection<EnrollmentsEntity> EnrolledSubjects { get; init; } = new List<EnrollmentsEntity>();
    }
}
