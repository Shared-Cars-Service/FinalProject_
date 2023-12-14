using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BL.Interfaces
{
    public interface IService<T>
    {
        public Task<int> CreateAsync(T item);
        public Task<List<T>> ReadAllAsync();
        public Task<T> ReadByIdAsync(int id);
        public Task<bool> UpdateAsync(T newItem);
        public Task<bool> DeleteAsync(int code);
    }
}
