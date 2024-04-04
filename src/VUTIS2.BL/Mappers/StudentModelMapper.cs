// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailModel>
{
    public override StudentListModel MapToListModel(StudentEntity? entity)
    => entity is null ? StudentListModel.Empty : new StudentListModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhotoUrl = entity.PhotoUrl,
        };

    public override StudentDetailModel MapToDetailModel(StudentEntity? entity)
        => entity is null
            ? StudentDetailModel.Empty
            : new StudentDetailModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl,
            };
    public override StudentEntity MapToEntity(StudentDetailModel model)
        => new()
    {
        Id = model.Id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        PhotoUrl = model.PhotoUrl,
    };
}
