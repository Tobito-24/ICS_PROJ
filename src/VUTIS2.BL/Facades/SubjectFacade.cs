// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class SubjectFacade(IUnitOfWorkFactory unitOfWorkFactory, SubjectModelMapper modelMapper) : FacadeBase<SubjectEntity, SubjectListModel, SubjectDetailModel, SubjectEntityMapper>(unitOfWorkFactory, modelMapper), ISubjectFacade
{
    public IEnumerable<SubjectListModel> GetOrderedByNameAsc(List<SubjectListModel> subjects)
    {
        return subjects.OrderBy(s => s.Name);
    }

    public IEnumerable<SubjectListModel> GetOrderedByNameDesc(List<SubjectListModel> subjects)
    {
        return subjects.OrderByDescending(s => s.Name);
    }

    public IEnumerable<SubjectListModel> GetOrderedByAbbreviationAsc(List<SubjectListModel> subjects)
    {
        return subjects.OrderBy(s => s.Abbreviation);
    }

    public IEnumerable<SubjectListModel> GetOrderedByAbbreviationDesc(List<SubjectListModel> subjects)
    {
        return subjects.OrderByDescending(s => s.Abbreviation);
    }
}
