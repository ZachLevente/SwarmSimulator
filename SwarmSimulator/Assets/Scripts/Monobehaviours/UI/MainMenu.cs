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
    [SerializeField] private TMP_Text errorMessage;

    private const string FolderName = "Environments";
    private const string NoEnvironmentsError = "There are no environment descriptors available\nAdd a file to /Environments/*.json";

    private string Selected => dropdown.options.Count == 0 ? "" : FolderName + "/" + dropdown.options[dropdown.value].text;

    private void Start()
    {
        startButton.onClick.AddListener(StartSimulation);
        exitButton.onClick.AddListener(Exit);
        
        dropdown.options.Clear();
        
        UpdateDropdown(GetJsonFiles());
        StartCoroutine(nameof(RefreshDropdown));
    }

    private void StartSimulation()
    {
        StopCoroutine(nameof(RefreshDropdown));
        Environment.Selected = Selected;
        SceneManager.LoadScene("SwarmScene");
    }

    private IEnumerable<WaitForSeconds> RefreshDropdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            UpdateDropdown(GetJsonFiles());
        }
    }

    private List<string> GetJsonFiles()
    {
        return new DirectoryInfo(FolderName)
            .GetFiles()
            .Where(f => f.Extension == ".json")
            .Select(f =>f.Name)
            .ToList();
    }

    private void UpdateDropdown(List<string> existingFiles)
    {
        var lastselected = Selected;

        // If the file is not available anymore, remove it
        foreach (var entry in dropdown.options)
        {
            if (!existingFiles.Contains(entry.text))
            {
                dropdown.options.Remove(entry);
            }
        }
        
        // If a file is not listed, add it
        foreach (var currentFile in existingFiles)
        {
            if (dropdown.options.All(o => o.text != currentFile))
            {
                dropdown.AddOptions(new List<string> {currentFile} );
            }
        }

        if (dropdown.options.Count == 0)
        {
            startButton.interactable = false;
            errorMessage.text = NoEnvironmentsError;
        }
        else
        {
            startButton.interactable = true;
            errorMessage.text = "";
        }
    }

    private void Exit()
    {
        Application.Quit();
    }
}
