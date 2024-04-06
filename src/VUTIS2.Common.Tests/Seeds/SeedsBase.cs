// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Reflection;
using VUTIS2.DAL.Entities;

namespace VUTIS2.Common.Tests.Seeds;

public class SeedsBase
{
    public static IEntity ClearJoined(IEntity entity)
    {
        // Get the type of the object
        Type entityType = entity.GetType();

        // Get all properties of the object
        PropertyInfo[] properties = entityType.GetProperties();

        // Dictionary to store property values
        Dictionary<PropertyInfo, object> propertyValues = new Dictionary<PropertyInfo, object>();

        // Iterate over each property
        foreach (var property in properties)
        {
            // Check if the property type is IEntity
            if (typeof(IEntity).IsAssignableFrom(property.PropertyType))
            {
                // Store the current value
                propertyValues[property] = property.GetValue(entity);

                // Set the property value to null
                property.SetValue(entity, null);
            }
            // Check if the property type is ICollection or ICollection<T>
            else if (IsCollection(property.PropertyType))
            {
                // Store the current value
                propertyValues[property] = property.GetValue(entity);

                // Create an empty collection of the property's type
                var collectionType = property.PropertyType.GetGenericTypeDefinition();
                var itemType = property.PropertyType.GetGenericArguments()[0];
                var emptyCollection = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                // Set the property value to the empty collection
                property.SetValue(entity, emptyCollection);
            }
        }

        return entity;
    }

    // Helper method to check if the type is ICollection or ICollection<T>
    private static bool IsCollection(Type type)
    {
        return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(ICollection<>) || type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)));
    }
}
