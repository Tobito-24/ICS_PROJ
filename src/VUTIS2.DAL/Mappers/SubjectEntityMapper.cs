// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Mappers;

public class SubjectEntityMapper : IEntityMapper<SubjectEntity>
{
    public void MapToExistingEntity(SubjectEntity existingEntity, SubjectEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Abbreviation = newEntity.Abbreviation;
    }
}
