using System.Collections.ObjectModel;

namespace VUTIS2.BL.Models;

public class StudentDetailModel : ModelBase
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public string? PhotoUrl { get; init; }

    public ObservableCollection<SubjectListModel> Subjects { get; init; } = new ();

    public static StudentDetailModel Empty => new()
    {
        Id = Guid.Empty,
        FirstName = string.Empty,
        LastName = string.Empty,
    };
}
