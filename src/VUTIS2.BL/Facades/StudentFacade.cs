// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class StudentFacade(IUnitOfWorkFactory unitOfWorkFactory, StudentModelMapper modelMapper) : FacadeBase<StudentEntity, StudentListModel, StudentDetailModel, StudentEntityMapper>(unitOfWorkFactory, modelMapper), IStudentFacade
{
    public IEnumerable<StudentListModel> GetOrderedByLastNameAsc(List<StudentListModel> students)
    {
        return students.OrderBy(s => s.LastName);
    }

    public IEnumerable<StudentListModel> GetOrderedByLastNameDesc(List<StudentListModel> students)
    {
        return students.OrderByDescending(s => s.LastName);
    }

    public IEnumerable<StudentListModel> GetOrderedByFirstNameAsc(List<StudentListModel> students)
    {
        return students.OrderBy(s => s.FirstName);
    }

    public IEnumerable<StudentListModel> GetOrderedByFirstNameDesc(List<StudentListModel> students)
    {
        return students.OrderByDescending(s => s.FirstName);
    }
}
