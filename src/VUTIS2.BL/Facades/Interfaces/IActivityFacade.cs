// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Models;

namespace VUTIS2.BL.Facades;

public interface IActivityFacade
{
    Task SaveAsync(ActivityDetailModel model, Guid subjectId);
    Task DeleteAsync(Guid id);
}
