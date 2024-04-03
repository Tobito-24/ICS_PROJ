// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
