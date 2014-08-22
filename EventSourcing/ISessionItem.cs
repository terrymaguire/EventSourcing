using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    internal interface ISessionItem
    {
        void SubmitChanges();
    }
}
