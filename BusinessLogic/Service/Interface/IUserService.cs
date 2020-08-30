﻿using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public interface IUserService
    {
        Task<UserModelBL> GetUsers();
    }
}
