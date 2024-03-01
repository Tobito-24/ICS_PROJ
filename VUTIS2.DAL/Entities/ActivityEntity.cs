using VUTIS2.Common.Enums;

namespace VUTIS2.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        public Guid Id { get; set; }

        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required string RoomName { get; set; }
        public required ActivityType ActivityType { get; set; }
        public required string Description { get; set; }

        public required Guid SubjectId { get; set; }
        public required SubjectEntity? Subject { get; set; }

        public required Guid EvaluationId { get; set; }
        public required EvaluationEntity? Evaluation { get; init; }
    }
}
