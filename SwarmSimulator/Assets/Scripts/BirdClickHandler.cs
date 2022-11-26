using Something.Controllers;
using UnityEngine;

namespace Something.UI
{
    public class BirdClickHandler
    {
        public static void BirdClicked(Entity entity)
        {
            GameManager.Instance.UiController.ShowPositionPopup(entity.Position);
        }
    }
}