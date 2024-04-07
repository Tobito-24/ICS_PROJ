// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Mappers;

public class SubjectModelMapper(StudentModelMapper studentModelMapper, ActivityModelMapper activityModelMapper) : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    public override SubjectListModel MapToListModel(SubjectEntity? entity)
        => entity is null
            ? SubjectListModel.Empty
            : new SubjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Abbreviation = entity.Abbreviation,
            };
    public override SubjectDetailModel MapToDetailModel(SubjectEntity? entity)
        => entity is null
            ? SubjectDetailModel.Empty
            : new SubjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Abbreviation = entity.Abbreviation,
                Students = studentModelMapper.MapToListModel(entity.Students).ToObservableCollection(),
                Activities = activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection()
            };
    public override SubjectEntity MapToEntity(SubjectDetailModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Abbreviation = model.Abbreviation,
    };
}
