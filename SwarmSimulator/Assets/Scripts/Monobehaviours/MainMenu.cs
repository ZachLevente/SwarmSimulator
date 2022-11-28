using System.Collections.Generic;
using System.IO;
using System.Linq;
using Something;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Text errorMessgage;

    private void Start()
    {
        startButton.onClick.AddListener(StartSimulation);
        exitButton.onClick.AddListener(Exit);
        
        dropdown.options.Clear();
        dropdown.AddOptions(GetJsonFiles());

        if (dropdown.options.Count == 0)
        {
            startButton.interactable = false;
            errorMessgage.text = "There are no environment descriptors available\nAdd a file to /Environments/*.json";
        }
        else
        {
            startButton.interactable = true;
            errorMessgage.text = "";
        }
    }

    private void StartSimulation()
    {
        Environment.Selected = dropdown.options[dropdown.value].text;
        SceneManager.LoadScene("SwarmScene");
    }

    private List<string> GetJsonFiles()
    {
        return new DirectoryInfo("Environments")
            .GetFiles()
            .Where(f => f.Extension == ".json")
            .Select(f =>f.Name)
            .ToList();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
