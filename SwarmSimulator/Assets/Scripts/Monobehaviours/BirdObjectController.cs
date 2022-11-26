using System;
using System.Collections.Generic;
using Something.UI;
using UnityEngine;

namespace Something.Controllers
{
    public class BirdObjectController : MonoBehaviour
    {
        public Entity Brain { get; private set; }
        
        private static readonly List<BirdObjectController> _birds = new();

        public BirdObjectController Spawn(Entity entity)
        {
            Brain = entity;
            Move();
            return this;
        }
        
        public void Move()
        {
            transform.position = GameManager.Instance.WorldSpaceGridController.GetGameWorldPosition(Brain.Position);
            transform.LookAt(transform.position + Brain.Direction);
        }
        
        public static void MoveAllBirds() => _birds.ForEach(b => b.Move());

        #region Callbacks

        private void OnEnable() => _birds.Add(this);

        private void OnDisable() => _birds.Remove(this);

        private void OnMouseDown()
        {
            BirdClickHandler.BirdClicked(Brain);
        }

        #endregion
    }
}