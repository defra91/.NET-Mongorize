// <copyright file="BaseEntity.cs" company="Luca De Franceschi">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Entities
{
    /// <summary>
    /// Represents the base entity model class.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the id of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the entity creation.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the entity last update.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}