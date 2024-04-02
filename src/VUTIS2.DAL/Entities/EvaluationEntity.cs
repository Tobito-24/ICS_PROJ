namespace VUTIS2.DAL.Entities
{
    public record EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public required int Points { get; init; }
        public string? Description { get; init; }

        public required Guid ActivityId { get; init; }
        public required ActivityEntity? Activity { get; init; }

        public required Guid StudentId { get; init; }
        public required StudentEntity? Student { get; init; }
    }
}
