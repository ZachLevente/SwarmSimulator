using Something.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Something.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private BirdDataInputPanel vec3popup;
        [SerializeField] private Button OkButton;
        [SerializeField] private Button MenuButton;
        [SerializeField] private TMP_Text ContinueButtonText;
        
        private BirdObjectController modifiedEntity = null;
        private bool continous = false;
        

        public void Start()
        {
            OkButton.onClick.AddListener(SaveModifications);
            OkButton.onClick.AddListener(CloseEntityPopup);
            MenuButton.onClick.AddListener(BackToMenu);
        }

        #region Bird management popup

        public void BirdSelected(BirdObjectController entity)
        {
            modifiedEntity = entity;
            ShowEntityPositionPopup(modifiedEntity);
        }
        
        private void ShowEntityPositionPopup(BirdObjectController entity)
        {
            vec3popup.DisplayValue(entity.Brain.Position, entity.Brain.Behaviour);
            vec3popup.gameObject.SetActive(true);
        }

        private void SaveModifications()
        {
            if (modifiedEntity is not null)
            {
                modifiedEntity.Brain.Position = vec3popup.GetIntegerData();
                modifiedEntity.Brain.Behaviour = vec3popup.GetSelectedBehaviour();
                modifiedEntity.Move();
            }
        }
        
        private void CloseEntityPopup()
        {
            modifiedEntity = null;
            vec3popup.gameObject.SetActive(false);
        }

        #endregion

        #region Game continuity

        public void ToggleGameContinuity()
        {
            continous = !continous;
            ContinueButtonText.text = continous ? "Pause" : "Continue";
            GameManager.Instance.GameUpdateController.SetAutoStep(continous);
        }

        #endregion

        private void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}