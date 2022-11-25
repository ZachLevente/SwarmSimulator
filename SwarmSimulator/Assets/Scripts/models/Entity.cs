using UnityEngine;

namespace Something
{
    abstract class Entity
    {
    
        public int x, y, z;
        Vector3 dir;
        internal abstract void selectDestination(Field[,,] env);
        internal abstract void stepIfAble();

    }
}