using System.Collections.Generic;
using System.Linq;
using Something;

namespace utils
{
    public class Psychiatry
    {
        private Dictionary<string, EntityBehaviour> _behaviours = new();
        private EntityBehaviour _default = new()
        {
            Name = "Default",
            StepRange = 2,
            ViewRange = 15,
            WallViewRange = 10,
            DirectionAdaptationRate = 0.1f,
            GroupPull = 0.1f,
            WallRepulsiveness = 0.1f,
        };
        
        public EntityBehaviour Default => _default;

        public EntityBehaviour GetBehaviour(string name)
        {
            _behaviours.TryGetValue(name, out var behaviour);
            return behaviour ?? _default;
        }

        public List<string> GetBehaviourNames() => _behaviours.Keys.ToList();
        
        public void RegisterBehaviour(EntityBehaviour behaviour) => _behaviours.Add(behaviour.Name, behaviour);

        public void Initialize(IEnumerable<EntityBehaviour> behaviours)
        {
            _behaviours = new();
            
            if (_default is not null)
            {
                RegisterBehaviour(_default);
            }
            foreach (var entityBehaviour in behaviours)
            {
                RegisterBehaviour(entityBehaviour);
            }
        }
    }
}