namespace VUTIS2.DAL.Entities
{
    public record StudentEntity : IEntity
    {
        public Guid Id { get; set; }

        public required string FirstName { get; init; }
        public required string LastName { get; init; }

        public string? PhotoUrl { get; init; }

        public ICollection<SubjectEntity> Subjects { get; init; } = new List<SubjectEntity>();
    }
}
