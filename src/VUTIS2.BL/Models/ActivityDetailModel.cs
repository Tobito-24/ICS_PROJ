using System.Collections.ObjectModel;
using VUTIS2.Common.Enums;

namespace VUTIS2.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }

    public required string RoomName { get; init; }

    public required ActivityType ActivityType { get; init; }
    public required string Description { get; init; }
    public Guid SubjectId { get; set; }
    public SubjectDetailModel? Subject { get; init; }
    public ObservableCollection<EvaluationListModel>? Evaluations { get; init; } = new();

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        StartTime = DateTime.MinValue,
        EndTime = DateTime.MinValue,
        RoomName = string.Empty,
        ActivityType = ActivityType.Empty,
        Description = string.Empty,
    };
}
