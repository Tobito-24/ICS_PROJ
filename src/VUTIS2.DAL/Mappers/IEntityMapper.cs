using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Mappers;

public interface IEntityMapper<in TEntity> where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
