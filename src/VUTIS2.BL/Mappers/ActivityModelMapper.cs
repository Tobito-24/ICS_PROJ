// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Mappers;

public class ActivityModelMapper(EvaluationModelMapper evaluationModelMapper, SubjectModelMapper subjectModelMapper) : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id,
                Description = entity.Description,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RoomName = entity.RoomName,
                ActivityType = entity.ActivityType
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
    => entity is null ? ActivityDetailModel.Empty :  new ActivityDetailModel
    {
        Id = entity.Id,
        Description = entity.Description,
        StartTime = entity.StartTime,
        EndTime = entity.EndTime,
        RoomName = entity.RoomName,
        ActivityType = entity.ActivityType,
        SubjectId = entity.SubjectId,
        Evaluations = entity.Evaluations is null ? null : evaluationModelMapper.MapToListModel(entity.Evaluations).ToObservableCollection()
    };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => throw new NotImplementedException("This method is unsupported. Use the other overload.");
    public ActivityEntity MapToEntity(ActivityDetailModel model, Guid subjectId)=> new()
    {
        Id = model.Id,
        Description = model.Description,
        StartTime = model.StartTime,
        EndTime = model.EndTime,
        RoomName = model.RoomName,
        ActivityType = model.ActivityType,
        SubjectId = model.SubjectId,
        Subject = null!,
        Evaluations = null!
    };
}
