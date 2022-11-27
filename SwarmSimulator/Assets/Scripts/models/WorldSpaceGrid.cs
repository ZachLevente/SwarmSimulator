using System.Collections.Generic;

namespace Something
{
    public class WorldSpaceGrid
    {
        private readonly Entity[,,] _field;
        private readonly List<Entity> _entities = new();

        public Entity[,,] Fields => _field;

        public WorldSpaceGrid(int x, int y, int z)
        {
            _field = new Entity[x,y,z];
        }

        internal void Step()
        {
            foreach (var entity in _entities)
                entity.SelectDestination(_field, _entities);
            foreach (var entity in _entities)
                entity.StepIfAble(_field);
        }

        internal void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}