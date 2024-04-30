using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class SubjectFacade(IUnitOfWorkFactory unitOfWorkFactory, ISubjectModelMapper modelMapper, IEnrollmentFacade enrollmentFacade, IActivityFacade activityFacade) : FacadeBase<SubjectEntity, SubjectListModel, SubjectDetailModel, SubjectEntityMapper>(unitOfWorkFactory, modelMapper), ISubjectFacade
{
    protected override List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(SubjectEntity.Enrollments)}",
        $"{nameof(SubjectEntity.Activities)}"
    };

    public async Task DeleteSubjectAsync(Guid id)
    {
        var Subject = await GetAsync(id);
        if (Subject is not null)
        {
            foreach (var enrollment in Subject.Enrollments)
            {
                await enrollmentFacade.DeleteAsync(enrollment.Id);
            }
            foreach (var activity in Subject.Activities)
            {
                await activityFacade.DeleteActivityAsync(activity.Id);
            }
        }
    }

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
