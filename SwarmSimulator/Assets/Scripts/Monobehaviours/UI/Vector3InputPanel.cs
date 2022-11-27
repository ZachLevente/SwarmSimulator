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

        public string Label = "Values:";

        private void Start()
        {
            if (string.IsNullOrEmpty(label.text))
            {
                label.text = Label;
            }
        }

        public Vector3Int GetIntegerData()
        {
            int.TryParse(fieldX.text, out int x);
            int.TryParse(fieldY.text, out int y);
            int.TryParse(fieldZ.text, out int z);
            return new Vector3Int(x, y, z);
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
