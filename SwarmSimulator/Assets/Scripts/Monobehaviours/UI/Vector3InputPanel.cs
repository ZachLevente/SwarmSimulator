using TMPro;
using UnityEngine;
using static TMPro.TMP_InputField;
using static TMPro.TMP_InputField.CharacterValidation;

namespace Something.UI
{
    public class Vector3InputPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private TMP_InputField fieldX;
        [SerializeField] private TMP_InputField fieldY;
        [SerializeField] private TMP_InputField fieldZ;

        [SerializeField] private bool _allowDecimals = true;
        public string Label = "Values:";

        private void Start()
        {
            SetAllowDecimals(_allowDecimals);
            if (string.IsNullOrEmpty(label.text))
            {
                label.text = Label;
            }
        }

        public void SetAllowDecimals(bool value)
        {
            _allowDecimals = value;
            UpdateValidation();
        }

        private void UpdateValidation()
        {
            fieldX.characterValidation = _allowDecimals ? CharacterValidation.Decimal : Integer;
            fieldY.characterValidation = _allowDecimals ? CharacterValidation.Decimal : Integer;
            fieldZ.characterValidation = _allowDecimals ? CharacterValidation.Decimal : Integer;
        }

        public Vector3Int GetIntegerData()
        {
            int.TryParse(fieldX.text, out int x);
            int.TryParse(fieldY.text, out int y);
            int.TryParse(fieldZ.text, out int z);
            return new Vector3Int(x, y, z);
        }

        public Vector3 GetFloatData()
        {
            float.TryParse(fieldX.text, out float x);
            float.TryParse(fieldY.text, out float y);
            float.TryParse(fieldZ.text, out float z);
            return new Vector3(x, y, z);
        }

        public void DisplayValue(Vector3 data, string valueName = null)
        {
            if (!_allowDecimals)
            {
                DisplayValue(new Vector3Int(Mathf.RoundToInt(data.x), Mathf.RoundToInt(data.y), Mathf.RoundToInt(data.z)), valueName);
                return;
            }
            
            fieldX.text = data.x.ToString();
            fieldY.text = data.y.ToString();
            fieldZ.text = data.z.ToString();

            label.text = string.IsNullOrEmpty(valueName) ? Label : valueName;
        }

        public void DisplayValue(Vector3Int data, string valueName = null)
        {
            fieldX.text = data.x.ToString();
            fieldY.text = data.y.ToString();
            fieldZ.text = data.z.ToString();

            label.text = string.IsNullOrEmpty(valueName) ? Label : valueName;
        }
    }
}
