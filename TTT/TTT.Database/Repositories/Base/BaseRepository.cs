using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TTT.Database.Contracts.Attributes;
using TTT.Database.Contracts.Interfaces.Base;
using TTT.Database.Contracts.Interfaces.Repositories.Base;

namespace TTT.Database.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseDatabaseItem
    {
        private readonly IMongoCollection<TEntity> _collection;

        protected BaseRepository()
        {
            IMongoDatabase database = new MongoClient("mongodb://localhost:27017").GetDatabase("ttt");
            this._collection = database.GetCollection<TEntity>(this.GetCollectionName(typeof(TEntity)));
            this._collection.InsertOne((TEntity)Activator.CreateInstance(typeof(TEntity)));
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return this._collection.AsQueryable();
        }

        public virtual IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            return this._collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return this._collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return this._collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => this._collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TEntity FindById(string id)
        {
            ObjectId objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
            return this._collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TEntity> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                ObjectId objectId = new ObjectId(id);
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                return this._collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual void InsertOne(TEntity document)
        {
            this._collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TEntity document)
        {
            return Task.Run(() => this._collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<TEntity> documents)
        {
            this._collection.InsertMany(documents);
        }
        
        public virtual async Task InsertManyAsync(ICollection<TEntity> documents)
        {
            await this._collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            this._collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TEntity document)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            await this._collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            this._collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => this._collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            ObjectId objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
            this._collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                ObjectId objectId = new ObjectId(id);
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                this._collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            this._collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => this._collection.DeleteManyAsync(filterExpression));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}