using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IRepository<T>
    {
        public Task<int> CreateAsync(T item);
        public Task<List<T>> ReadAllAsync();
        public Task<T> ReadByIdAsync(int code);
        public Task<bool> UpdateAsync(T newItem);
        public Task<bool> DeleteAsync(int code);
    }
}
