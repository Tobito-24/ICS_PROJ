using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class StudentFacade(IUnitOfWorkFactory unitOfWorkFactory, IStudentModelMapper modelMapper) : FacadeBase<StudentEntity, StudentListModel, StudentDetailModel, StudentEntityMapper>(unitOfWorkFactory, modelMapper), IStudentFacade
{
    protected override List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(StudentEntity.Enrollments)}"
    };
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
