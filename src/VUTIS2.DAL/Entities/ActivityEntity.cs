using VUTIS2.Common.Enums;

namespace VUTIS2.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        public Guid Id { get; set; }

        public required DateTime StartTime { get; init; }
        public required DateTime EndTime { get; init; }

        public required string RoomName { get; init; }

        public required ActivityType ActivityType { get; init; }
        public required string Description { get; init; }

        public required Guid SubjectId { get; init; }
        public required SubjectEntity? Subject { get; init; }

        public required Guid EvaluationId { get; init; }
        public ICollection<EvaluationEntity>? Evaluation { get; init; } = new List<EvaluationEntity>();
    }
}
