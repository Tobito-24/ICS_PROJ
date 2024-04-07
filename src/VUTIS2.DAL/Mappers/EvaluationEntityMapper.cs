// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Mappers;

public class EvaluationEntityMapper : IEntityMapper<EvaluationEntity>
{
    public void MapToExistingEntity(EvaluationEntity existingEntity, EvaluationEntity newEntity)
    {
        existingEntity.Description = newEntity.Description;
        existingEntity.ActivityId = newEntity.ActivityId;
        existingEntity.Points = newEntity.Points;
        existingEntity.StudentId = newEntity.StudentId;
    }
}
