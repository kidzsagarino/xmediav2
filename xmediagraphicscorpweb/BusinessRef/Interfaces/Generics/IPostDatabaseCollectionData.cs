﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.Interfaces.Generics
{
    public interface IPostDatabaseCollectionData<T>
    {
        ICollection<T> PostDatabaseCollectionData();
    }
}