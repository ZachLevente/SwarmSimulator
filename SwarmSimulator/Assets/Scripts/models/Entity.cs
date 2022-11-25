namespace Something
{
    abstract class Entity
    {
        int x, y, z;
        internal abstract void selectDestination(Field[,,] env);
        internal abstract void stepIfAble();

    }
}