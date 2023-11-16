using AutoMapper;
using Data.Models;
using Services.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostProject.Mapping
{
    public class GeneralProfile :Profile
    {
        public GeneralProfile()
        {
           

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            
            

            CreateMap<User, UserSetModel>();
            CreateMap<UserSetModel, User>();

         

        }
    }
}
