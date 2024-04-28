namespace VUTIS2.DAL.Entities;

public class EnrollmentEntity : IEntity
{
    public Guid Id { get; set; }
    public required Guid StudentId { get; set; }
    public required Guid SubjectId { get; set; }
}
