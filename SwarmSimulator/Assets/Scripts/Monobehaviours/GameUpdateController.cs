using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Something.Controllers
{
    public class GameUpdateController : MonoBehaviour
    {
        [SerializeField] private float updateTime = 1.0f;
        private Controller _controller;
        private Coroutine _lööp = null;

        public void SetGameState(WorldSpaceGrid grid)
        {
            _controller = new Controller(grid);
        }
        
        public void Step()
        {
            _controller.Step();
            BirdObjectController.MoveAllBirds();
        }

        public void SetAutoStep(bool continous)
        {
            if (continous)
            {
                _lööp = StartCoroutine(nameof(Gameloop));
            }
            else if (_lööp is not null)
            {
                StopCoroutine(_lööp);
                _lööp = null;
            }
        }

        private IEnumerator Gameloop()
        {
            while (true)
            {
                Step();
                yield return new WaitForSeconds(updateTime);
            }
        }
    }
}