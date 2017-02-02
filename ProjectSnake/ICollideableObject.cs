using System;

namespace ProjectSnake
{
    public interface ICollideableObject: IHasPosition, ICollideable, IHasProperties
    {
        void SetBackPosition();

    }
}
