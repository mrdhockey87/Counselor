﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class QueryFailedException : Exception
    {
        public QueryFailedException(string error) : base(error)
        { }

    }
}
