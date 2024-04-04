

using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Description = newEntity.Description;
        existingEntity.SubjectId = newEntity.SubjectId;
        existingEntity.ActivityType = newEntity.ActivityType;
        existingEntity.StartTime = newEntity.StartTime;
        existingEntity.EndTime = newEntity.EndTime;
        existingEntity.RoomName = newEntity.RoomName;
    }
}
