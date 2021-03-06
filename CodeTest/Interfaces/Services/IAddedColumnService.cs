﻿using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface IAddedColumnService
    {
        Task<ResultModel<AddedColumnModel>> Create(AddedColumnCreateModel model);
    }
}
