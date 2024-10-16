// <copyright file="BsonCollectionAttribute.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Attributes;

/// <summary>
/// Attribute that indicates that an entity is a Bson collection.
/// In this way we can threat the entity as a mongodb collection.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BsonCollectionAttribute" /> class
/// with its required collection name.
/// </remarks>
/// <param name="collectionName">The name of the collection.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute(string collectionName)
    : Attribute
{
    private readonly string collectionName = collectionName;

    /// <summary>
    /// Gets the collection name.
    /// </summary>
    public string CollectionName => this.collectionName;
}