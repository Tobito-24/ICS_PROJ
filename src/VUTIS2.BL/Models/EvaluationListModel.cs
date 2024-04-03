// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace VUTIS2.BL.Models;

public class EvaluationListModel : ModelBase
{
    public required int Points { get; init; }
    public string? Description { get; init; }

    public required StudentListModel? Student { get; init; }
    public static EvaluationListModel Empty => new()
    {
        Id = Guid.Empty,
        Points = 0,
        Description = string.Empty,
        Student = StudentListModel.Empty
    };
}
