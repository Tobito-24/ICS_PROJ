using Microsoft.Extensions.DependencyInjection;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Mappers;

public class SubjectModelMapper : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>, ISubjectModelMapper
{
    private IEnrollmentModelMapper _enrollmentModelMapper;
    private IActivityModelMapper _activityModelMapper;
    public SubjectModelMapper(IEnrollmentModelMapper enrollmentModelMapper, IActivityModelMapper activityModelMapper)
    {
        _enrollmentModelMapper = enrollmentModelMapper;
        _activityModelMapper = activityModelMapper;
    }

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
                Enrollments = _enrollmentModelMapper.MapToListModel(entity.Enrollments).ToObservableCollection(),
                Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection()
            };
    public override SubjectEntity MapToEntity(SubjectDetailModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Abbreviation = model.Abbreviation,
    };
}
