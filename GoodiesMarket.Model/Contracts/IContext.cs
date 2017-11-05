﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GoodiesMarket.Model.Contracts
{
    public interface IContext : IDisposable
    {
        DbChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
