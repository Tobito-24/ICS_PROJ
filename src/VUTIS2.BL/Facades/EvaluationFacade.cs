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
    protected override List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(EvaluationEntity.Student)}"
    };

    public IEnumerable<EvaluationListModel> GetOrderedByPointsAsc(List<EvaluationListModel> evaluations)
    {
        return evaluations.OrderBy(e => e.Points);
    }

    public IEnumerable<EvaluationListModel> GetOrderedByPointsDesc(List<EvaluationListModel> evaluations)
    {
        return evaluations.OrderByDescending(e => e.Points);
    }
}
