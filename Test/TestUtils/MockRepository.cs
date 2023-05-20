using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Test.TestUtils.Utils;

namespace Test.TestUtils
{
    public class MockRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        private List<T> _mockDataList { get; set; } = new List<T>();
        public IQueryable<T> Table => _mockDataList.AsQueryable();
        public void ResetMockData()
        {
            _mockDataList.Clear();
        }
        public async Task<T> CreateAsync(T entity)
        {
            entity.Id = GenerateId();
            _mockDataList.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var data = _mockDataList.Find(x => x.Id == id);
            if (data is not null)
            {
                _mockDataList.Remove(data);
            }
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null)
        {
            var query = _mockDataList.AsQueryable();
            if (filter is not null)
            {
                query.Where(filter);
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = _mockDataList.AsQueryable();
            var ret = query.FirstOrDefault(e => e.Id == id);
            return ret;
        }

        public void Update(T entity)
        {
            var index = _mockDataList.FindIndex(x => x.Id == entity.Id);

            _mockDataList[index] = entity;
        }
    }
}
