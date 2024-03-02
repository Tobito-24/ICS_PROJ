namespace VUTIS2.DAL.Entities
{
    public record EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public required int Points { get; set; }
        public string? Description { get; set; }

        public required Guid ActivityId { get; set; }
        public required ActivityEntity? Activity { get; set; }

        public required Guid StudentId { get; set; }
        public required StudentEntity? Student { get; set; }
    }
}
