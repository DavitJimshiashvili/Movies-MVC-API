﻿using MovieManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerService
{
    public interface IScopedMovieService
    {
        Task UpadateMovieStatus();
    }
}
