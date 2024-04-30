using VUTIS2.BL.Models;
using VUTIS2.DAL.Entities;

namespace VUTIS2.BL.Facades;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityListModel>> GetAsyncFromSubject(Guid Id);
}
