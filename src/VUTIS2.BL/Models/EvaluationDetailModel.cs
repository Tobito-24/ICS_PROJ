namespace VUTIS2.BL.Models;

public class EvaluationDetailModel : ModelBase
{
    public required int Points { get; init; }
    public string? Description { get; init; }

    public required ActivityDetailModel Activity { get; init; }

    public required StudentDetailModel? Student { get; init; }
    public static EvaluationDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Points = int.MinValue,
        Description = string.Empty,
        Activity = ActivityDetailModel.Empty,
        Student = StudentDetailModel.Empty
    };
}
