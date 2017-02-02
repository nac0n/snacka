using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    public interface ICollideable
    {
        bool HasCollided { get; set; }

        bool IsKillable { get; }
        bool IsDestructable { get; }
        bool IsObtainable { get; }
        bool IsPassable { get; }
        bool IsMoveable { get; }
        
    }
}