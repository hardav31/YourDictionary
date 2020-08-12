using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Config
{
    public class AutoMapperProfileBL : Profile
    {
        public AutoMapperProfileBL()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();
        }
    }
}
