using System.Collections.Generic;
using System.IO;
using System.Linq;
using Something;

namespace utils
{
    public class Psychiatry
    {
        private Dictionary<string, EntityBehaviour> _behaviours = new();
        private EntityBehaviour _default;
        
        public EntityBehaviour Default => _default;

        
        public EntityBehaviour GetBehaviour(string name)
        {
            _behaviours.TryGetValue(name, out var behaviour);
            return behaviour ?? _default;
        }

        public List<string> GetBehaviourNames() => _behaviours.Keys.ToList();

        public void Initialize(IEnumerable<EntityBehaviour> behaviours)
        {
            _behaviours = new();
            foreach (var entityBehaviour in behaviours)
            {
                RegisterBehaviour(entityBehaviour);
            }

            if (_default is not null)
            {
                RegisterBehaviour(_default);
            }
        }
        
        public bool RegisterBehaviour(EntityBehaviour behaviour)
        {
            if (_behaviours.ContainsKey(behaviour.Name))
            {
                return false;
            }
            
            _behaviours.Add(behaviour.Name, behaviour);
            return true;
        }

        public void SetAsDefault(EntityBehaviour behaviour)
        {
            if (!_behaviours.ContainsKey(behaviour.Name))
            {
                RegisterBehaviour(behaviour);
            }
            
            _default = behaviour;
        }

        public void SetAsDefault(string file)
        {
            string jsonString = File.ReadAllText (file);
            var defaultBehaviour = EntityBehaviour.CreateFromJSON(jsonString);
            defaultBehaviour.validate();
            SetAsDefault(defaultBehaviour);
        }
    }
}