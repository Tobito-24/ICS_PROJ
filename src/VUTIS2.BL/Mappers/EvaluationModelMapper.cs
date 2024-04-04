// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Mappers;

public class EvaluationModelMapper(StudentModelMapper studentModelMapper) : ModelMapperBase<EvaluationEntity, EvaluationListModel, EvaluationDetailModel>
{
    public override EvaluationListModel MapToListModel(EvaluationEntity? entity)
        => entity is null
            ? EvaluationListModel.Empty
            : new EvaluationListModel
            {
                Id = entity.Id,
                Description = entity.Description,
                Points = entity.Points,
                StudentId = entity.StudentId,
                Student = studentModelMapper.MapToListModel(entity.Student),
            };
    public override EvaluationDetailModel MapToDetailModel(EvaluationEntity? entity)
    => entity is null ? EvaluationDetailModel.Empty : new EvaluationDetailModel
    {
        Id = entity.Id,
        Description = entity.Description,
        Points = entity.Points,
        StudentId = entity.StudentId,
        ActivityId = entity.ActivityId,
        Activity = null!,
        Student = null!
    };

    public EvaluationListModel MapToListModel(EvaluationDetailModel evaluationDetailModel)
        => new()
        {
            Id = evaluationDetailModel.Id,
            Description = evaluationDetailModel.Description,
            Points = evaluationDetailModel.Points,
            StudentId = evaluationDetailModel.StudentId,
            Student = evaluationDetailModel.Student,
        };
    public override EvaluationEntity MapToEntity(EvaluationDetailModel model)
        => throw new NotImplementedException("This method is unsupported. Use the other overload.");
    public EvaluationEntity MapToEntity(EvaluationDetailModel model, Guid activityId) => new()
    {
        Id = model.Id,
        Description = model.Description,
        Points = model.Points,
        StudentId = model.StudentId,
        ActivityId = model.ActivityId,
        Student = null!,
        Activity = null!
    };
}
