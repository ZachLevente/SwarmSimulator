using Something.Controllers;
using TMPro;
using UnityEngine;

namespace Something.UI
{
    public class BirdDataInputPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField fieldX;
        [SerializeField] private TMP_InputField fieldY;
        [SerializeField] private TMP_InputField fieldZ;
        [SerializeField] private TMP_Dropdown dropdown;

        public void DisplayValue(Vector3Int data, EntityBehaviour behaviour)
        {
            fieldX.text = data.x.ToString();
            fieldY.text = data.y.ToString();
            fieldZ.text = data.z.ToString();

            dropdown.ClearOptions();
            var options = GameManager.Instance.Psychiatry.GetBehaviourNames();
            dropdown.AddOptions(options);
            dropdown.value = options.IndexOf(behaviour.Name);
        }

        public Vector3Int GetIntegerData()
        {
            int.TryParse(fieldX.text, out int x);
            int.TryParse(fieldY.text, out int y);
            int.TryParse(fieldZ.text, out int z);
            
            return new Vector3Int(x, y, z);
        }

        public EntityBehaviour GetSelectedBehaviour()
        {
            return GameManager.Instance.Psychiatry.GetBehaviour(dropdown.options[dropdown.value].text);
        }
    }
}
