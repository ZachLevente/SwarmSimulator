using System.Collections.Generic;
using UnityEngine;

namespace Something.Controllers
{
    public class GameUpdateController : MonoBehaviour
    {
        private Controller _controller;

        public void SetGameState(WorldSpaceGrid grid)
        {
            _controller = new Controller(grid);
        }
        
        public void Step()
        {
            _controller.Step();
            BirdObjectController.MoveAllBirds();
        }
    }
}