// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace VUTIS2.BL.Mappers;

public interface IModelMapper<TEntity, out TListModel, TDetailModel>
{
    TListModel MapToListModel(TEntity? entity);

    IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
        => entities.Select(MapToListModel);

    TDetailModel MapToDetailModel(TEntity entity);
    TEntity MapToEntity(TDetailModel model);
}
