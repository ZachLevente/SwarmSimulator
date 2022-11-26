using UnityEngine;

namespace Something.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private Vector3InputPanel vec3popup;

        public void ShowPositionPopup(Vector3Int position)
        {
            vec3popup.SetAllowDecimals(false);
            vec3popup.DisplayValue(position, "Position:");
            vec3popup.gameObject.SetActive(true);
        }
    }
}