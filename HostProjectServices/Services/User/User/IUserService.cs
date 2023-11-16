using Services.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IUserService
    {
        
        Task<List<UserModel>> GetUsers();

        Task<string> InsertUser(UserSetModel input);


    }
}
