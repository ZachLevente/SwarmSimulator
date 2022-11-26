using Something.Controllers;

namespace Something.UI
{
    public class BirdClickHandler
    {
        public static void BirdClicked(BirdObjectController entity)
        {
            GameManager.Instance.UiController.ShowEntityPopup(entity);
        }
    }
}