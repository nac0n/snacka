using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    public interface IHasCollision
    {
        bool IsDestructable { get; }
        bool IsObtainable { get; }
        bool IsPassable { get; }
        bool IsMoveable { get; }
    }
}