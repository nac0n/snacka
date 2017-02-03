using System;

namespace ProjectSnake
{
    abstract class ICollideableObject: IHasPosition, ICollideable, IHasProperties
    {

        void SetBackPosition();
        void GiveHP(ICollideableObject obj);

        int IHasPosition.posX
        {
            get { }

            set { }
        }

        int IHasPosition.posY
        {
            get { }

            set { }
        }

        int IHasPosition.prevPosX
        {
            get { }

            set { }
        }

        int IHasPosition.prevPosY
        {
            get { }

            set { }
        }

        bool ICollideable.HasCollided
        {
            get { }

            set { }
        }

        bool ICollideable.IsKillable
        {
            get { }
        }

        bool ICollideable.IsDestructable
        {
            get { }
        }

        bool ICollideable.IsObtainable
        {
            get { }
        }

        bool ICollideable.IsPassable
        {
            get { }
        }

        bool ICollideable.IsMoveable
        {
            get { }
        }

        float IHasProperties.expModifier
        {
            get { }
        }

        int IHasProperties.givenHP
        {
            get { }
        }
    }
}
