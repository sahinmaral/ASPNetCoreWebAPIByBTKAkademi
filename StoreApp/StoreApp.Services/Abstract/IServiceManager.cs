﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services.Abstract
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
    }
}
