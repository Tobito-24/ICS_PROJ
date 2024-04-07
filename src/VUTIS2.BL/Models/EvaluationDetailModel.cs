namespace VUTIS2.BL.Models;

public class EvaluationDetailModel : ModelBase
{
    public required int Points { get; init; }
    public string? Description { get; init; }
    public Guid ActivityId { get; set; }

    public ActivityDetailModel? Activity { get; init; }
    public Guid StudentId { get; set; }
    public StudentListModel? Student { get; init; }
    public static EvaluationDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Points = int.MinValue,
        Description = string.Empty,
        Activity = ActivityDetailModel.Empty,
        Student = StudentListModel.Empty
    };
}
