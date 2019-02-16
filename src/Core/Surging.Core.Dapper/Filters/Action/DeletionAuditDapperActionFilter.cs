using Surging.Core.Domain.Entities;
using Surging.Core.Domain.Entities.Auditing;
using System;

namespace Surging.Core.Dapper.Filters.Action
{
    public class DeletionAuditDapperActionFilter<TEntity, TPrimaryKey> : DapperActionFilterBase, IAuditActionFilter<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public void ExecuteFilter(TEntity entity)
        {
            if (entity is ISoftDelete)
            {
                ((ISoftDelete)entity).IsDeleted = IsDeleted;
                if (typeof(IHasDeletionTime).IsAssignableFrom(entity.GetType()))
                {
                    ((IHasDeletionTime)entity).DeletionTime = DateTime.Now;
                }
            }
        }
    }
}
