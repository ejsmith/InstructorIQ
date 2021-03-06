using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InstructorIQ.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="InstructorIQ.Core.Data.Entities.RefreshToken" />.
    /// </summary>
    public static partial class RefreshTokenExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> or null if not found.</returns>
        public static InstructorIQ.Core.Data.Entities.RefreshToken GetByKey(this IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> queryable, Guid id)
        {
            if (queryable is DbSet<InstructorIQ.Core.Data.Entities.RefreshToken> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> or null if not found.</returns>
        public static Task<InstructorIQ.Core.Data.Entities.RefreshToken> GetByKeyAsync(this IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> queryable, Guid id)
        {
            if (queryable is DbSet<InstructorIQ.Core.Data.Entities.RefreshToken> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="tokenHashed">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> or null if not found.</returns>
        public static InstructorIQ.Core.Data.Entities.RefreshToken GetByTokenHashed(this IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> queryable, string tokenHashed)
        {
            return queryable.FirstOrDefault(q => q.TokenHashed == tokenHashed);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="tokenHashed">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:InstructorIQ.Core.Data.Entities.RefreshToken"/> or null if not found.</returns>
        public static Task<InstructorIQ.Core.Data.Entities.RefreshToken> GetByTokenHashedAsync(this IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> queryable, string tokenHashed)
        {
            return queryable.FirstOrDefaultAsync(q => q.TokenHashed == tokenHashed);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userName">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> ByUserName(this IQueryable<InstructorIQ.Core.Data.Entities.RefreshToken> queryable, string userName)
        {
            return queryable.Where(q => q.UserName == userName);
        }

        #endregion

    }
}
