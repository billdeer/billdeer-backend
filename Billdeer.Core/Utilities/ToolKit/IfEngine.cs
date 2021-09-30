using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Utilities.ToolKit
{
    public static class IfEngine
    {
        public static bool Engine(params bool[] funcs)
        {
            foreach (var func in funcs)
            {
                if (func)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
