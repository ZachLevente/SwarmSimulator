using Something.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Something.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private Vector3InputPanel vec3popup;
        [SerializeField] private Button OkButton;
        [SerializeField] private TMP_Text ContinueButtonText;
        
        private BirdObjectController modifiedEntity = null;
        private bool continous = false;
        

        public void Start()
        {
            OkButton.onClick.AddListener(SaveModifications);
            OkButton.onClick.AddListener(CloseEntityPopup);
        }

        public void CloseEntityPopup()
        {
            modifiedEntity = null;
            vec3popup.gameObject.SetActive(false);
        }
        
        public void ShowEntityPopup(BirdObjectController entity)
        {
            modifiedEntity = entity;
            vec3popup.SetAllowDecimals(false);
            vec3popup.DisplayValue(entity.Brain.Position, "Position:");
            vec3popup.gameObject.SetActive(true);
        }

        public void SaveModifications()
        {
            if (modifiedEntity is not null)
            {
                modifiedEntity.Brain.Position = vec3popup.GetIntegerData();
                modifiedEntity.Move();
            }
        }

        public void ToggleGameContinuity()
        {
            continous = !continous;
            ContinueButtonText.text = continous ? "Pause" : "Continue";
            GameManager.Instance.GameUpdateController.SetAutoStep(continous);
        }
    }
}