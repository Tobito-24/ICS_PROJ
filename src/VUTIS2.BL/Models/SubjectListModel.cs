namespace VUTIS2.BL.Models;

public class SubjectListModel : ModelBase
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

    public static SubjectListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Abbreviation = string.Empty,
    };
}
