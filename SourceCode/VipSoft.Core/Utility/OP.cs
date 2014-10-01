using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VipSoft.Core.Utility
{

    public enum LOP
    {
        AND, OR
    }

    /// <summary>
    /// =,<>,>=,<=,>,<,LIKE %{0}%,LIKE {0}%,LIKE %{0}
    /// </summary>
    public enum OP
    {
        EQ, NE, IN, NOTIN, GE, LE, GT, LT, BETWEEN, LIKE, FLIKE, ELIKE, IS, UEMPTY
    } 
}
