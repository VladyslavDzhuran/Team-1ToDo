using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo
{
    interface IToDoService
    {
        void Create();
        void Read();
        void Update(); 
        void Delete();
    }
}
