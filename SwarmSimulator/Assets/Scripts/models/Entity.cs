namespace Something
{
    abstract class Entity
    {
        int x, y, z;
        int dirX, dirY, dirZ; // -1 | 0 | 1
        internal abstract void selectDestination(Field[,,] env);
        internal abstract void stepIfAble();

    }
}