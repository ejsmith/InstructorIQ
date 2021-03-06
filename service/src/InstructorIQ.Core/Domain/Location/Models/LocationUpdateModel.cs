using System;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Models;

// ReSharper disable once CheckNamespace
namespace InstructorIQ.Core.Domain.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public class LocationUpdateModel
        : EntityUpdateModel, IHaveTenant<Guid>
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'Name'.
        /// </summary>
        /// <value>
        /// The property value for 'Name'.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Description'.
        /// </summary>
        /// <value>
        /// The property value for 'Description'.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'AddressLine1'.
        /// </summary>
        /// <value>
        /// The property value for 'AddressLine1'.
        /// </value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'AddressLine2'.
        /// </summary>
        /// <value>
        /// The property value for 'AddressLine2'.
        /// </value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'AddressLine3'.
        /// </summary>
        /// <value>
        /// The property value for 'AddressLine3'.
        /// </value>
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'City'.
        /// </summary>
        /// <value>
        /// The property value for 'City'.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'StateProvince'.
        /// </summary>
        /// <value>
        /// The property value for 'StateProvince'.
        /// </value>
        public string StateProvince { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'PostalCode'.
        /// </summary>
        /// <value>
        /// The property value for 'PostalCode'.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Latitude'.
        /// </summary>
        /// <value>
        /// The property value for 'Latitude'.
        /// </value>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Longitude'.
        /// </summary>
        /// <value>
        /// The property value for 'Longitude'.
        /// </value>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'TenantId'.
        /// </summary>
        /// <value>
        /// The property value for 'TenantId'.
        /// </value>
        public Guid TenantId { get; set; }

        #endregion

    }
}
