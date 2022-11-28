using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Text errorMessgage;

    private void Start()
    {
        dropdown.options.Clear();
        dropdown.AddOptions(GetJsonFiles());

        if (dropdown.options.Count == 0)
        {
            startButton.interactable = false;
            errorMessgage.text = "There are no environment descriptors available";
        }
        else
        {
            startButton.interactable = true;
            errorMessgage.text = "";
        }
        
        startButton.onClick.AddListener(StartSimulation);
    }

    private void StartSimulation()
    {
        Debug.Log(dropdown.options[dropdown.value].text);
    }

    private List<string> GetJsonFiles()
    {
        return new DirectoryInfo("environment")
            .GetFiles()
            .Where(f => f.Extension == ".json")
            .Select(f =>f.Name)
            .ToList();
    }
}
