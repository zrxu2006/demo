using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    interface IUtility
    {
        string UtilityName { get; }
        string SetName(string name);
    }

}
