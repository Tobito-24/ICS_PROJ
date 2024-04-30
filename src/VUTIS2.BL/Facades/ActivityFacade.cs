using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.Common.Enums;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Mappers;
using VUTIS2.DAL.Repositories;
using VUTIS2.DAL.UnitOfWork;

namespace VUTIS2.BL.Facades;

public class ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory, IActivityModelMapper activityModelMapper, IEvaluationFacade evaluationFacade)
    : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>(unitOfWorkFactory,
        activityModelMapper), IActivityFacade
{
    /*public async Task SaveAsync(ActivityDetailModel model, Guid subjectId)
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
    }*/

    public async Task DeleteActivityAsync(Guid Id)
    {
        var Activity = await GetAsync(Id);
        if (Activity is not null)
        {
            if (Activity.Evaluations is not null)
            {
                foreach (var evaluation in Activity.Evaluations)
                {
                    await evaluationFacade.DeleteAsync(evaluation.Id);

                }
            }
        }
    }
    public async Task<IEnumerable<ActivityListModel>> GetAsyncFromSubject(Guid Id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        List<ActivityEntity> activities = await repository.Get().Where(a => a.SubjectId == Id).ToListAsync();
        return ModelMapper.MapToListModel(activities);
    }
    protected override List<string> IncludesNavigationPathDetail => new()
    {
        $"{nameof(ActivityEntity.Evaluations)}"
    };

    public async Task<IEnumerable<ActivityListModel>> GetActivitiesStartTime(DateTime startTime, bool from,
        Guid SubjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        // Filter and sort the activities
        List<ActivityEntity> activities = await repository
            .Get()
            .Where(a => from ? a.StartTime <= startTime : a.StartTime >= startTime)
            .Where(a => a.SubjectId == SubjectId)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
        return ModelMapper.MapToListModel(activities);
    }

    public async Task<IEnumerable<ActivityListModel>> GetActivitiesEndTime(DateTime endTime, bool from, Guid SubjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        // Filter and sort the activities
        List<ActivityEntity> activities = await repository
            .Get()
            .Where(a => from ? a.StartTime <= endTime : a.StartTime >= endTime)
            .Where(a => a.SubjectId == SubjectId)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
        return ModelMapper.MapToListModel(activities);
    }

    public async Task<IEnumerable<ActivityListModel>> GetActivitiesByBoth(DateTime startTime, DateTime endTime,
        Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        // Filter and sort the activities
        List<ActivityEntity> activities = await repository
            .Get()
            .Where(a => a.SubjectId == subjectId)
            .Where(a => a.StartTime >= startTime && a.EndTime <= endTime)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
        return ModelMapper.MapToListModel(activities);
    }

    public IEnumerable<ActivityListModel> GetOrderedByStartTimeAsc(IEnumerable<ActivityListModel> activities)
    {
        return activities.OrderBy(a => a.StartTime);
    }

    public IEnumerable<ActivityListModel> GetOrderedByStartTimeDesc(IEnumerable<ActivityListModel> activities)
    {
        return activities.OrderByDescending(a => a.StartTime);
    }

    public IEnumerable<ActivityListModel> GetOrderedByEndTimeAsc(IEnumerable<ActivityListModel> activities)
    {
        return activities.OrderBy(a => a.EndTime);
    }

    public IEnumerable<ActivityListModel> GetOrderedByEndTimeDesc(IEnumerable<ActivityListModel> activities)
    {
        return activities.OrderByDescending(a => a.EndTime);
    }

}
