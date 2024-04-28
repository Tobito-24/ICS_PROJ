namespace VUTIS2.BL.Models;

public class EnrollmentListModel : ModelBase
{
    public Guid SubjectId { get; init; }
    public SubjectListModel? Subject { get; init; }
    public Guid StudentId { get; init; }
    public StudentListModel? Student { get; init; }
    public static EnrollmentListModel Empty => new()
    {
        Id = Guid.Empty,
    };
}
