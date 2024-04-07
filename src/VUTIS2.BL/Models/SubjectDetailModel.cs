﻿using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VUTIS2.BL.Models;

public class SubjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();

    public ObservableCollection<StudentListModel> Students { get; init; } = new();
    public static SubjectDetailModel Empty => new()
    {
        Name = string.Empty,
        Abbreviation = string.Empty,
    };
}
