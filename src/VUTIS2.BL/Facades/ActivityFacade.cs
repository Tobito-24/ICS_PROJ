// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.Repositories;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory, ActivityModelMapper activityModelMapper) : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>(unitOfWorkFactory, activityModelMapper), IActivityFacade
{
    public async Task SaveAsync(ActivityDetailModel model, Guid subjectId)
    {
        ActivityEntity entity = activityModelMapper.MapToEntity(model, subjectId);
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository =
            uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }

    protected override List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(ActivityEntity.Evaluations)}.{nameof(EvaluationEntity.Student)}"
    };
}
