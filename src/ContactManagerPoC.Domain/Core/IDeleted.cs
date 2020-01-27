using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Domain.Core
{
    interface IDeleted
    {
        bool IsDeleted { get; }
    }
}
