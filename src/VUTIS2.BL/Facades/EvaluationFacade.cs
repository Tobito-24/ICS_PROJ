// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.Repositories;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class EvaluationFacade(IUnitOfWorkFactory unitOfWorkFactory, EvaluationModelMapper evaluationModelMapper) : FacadeBase<EvaluationEntity, EvaluationListModel, EvaluationDetailModel, EvaluationEntityMapper>(unitOfWorkFactory, evaluationModelMapper), IEvaluationFacade
{
    public async Task SaveAsync(EvaluationDetailModel model, Guid activityId)
    {
        EvaluationEntity entity = evaluationModelMapper.MapToEntity(model, activityId);
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<EvaluationEntity> repository =
            uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }
}
