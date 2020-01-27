using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ContactManagerPoC.Domain.Core
{
    public static class Contract
    {
        public static void Require(Expression<Func<bool>> contract)
        {
            if (!contract.Compile()())
            {
                throw new InvalidOperationException(contract.ToString());
            }      
        }
    }
}
