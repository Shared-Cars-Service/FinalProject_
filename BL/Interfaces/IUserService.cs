using BL.DTO;
using Dal.DataObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserService : IService <UserDTO>
    {
        public Task<ActionResult<UserDTO>> ReadByPasswordAsync(string password, string name);
    }
}
