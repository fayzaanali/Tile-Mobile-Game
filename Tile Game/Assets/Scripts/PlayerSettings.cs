using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    [Header("Menu and Interactables")]
    public Canvas ingameSettingsCanvas;
    public Button openSettingsMenu, closeSettingsMenu, exitToMainMenu;
    
    [Header("Audio Settings")]
    public Slider audioSlider;
    public TextMeshProUGUI audioPercentText;


    // Start is called before the first frame update
    void Start()
    {
        ingameSettingsCanvas.gameObject.SetActive(false);
        btnListen();
    }

    // Update is called once per frame
    void Update()
    {
        audioPercentText.text = (audioSlider.value * 10).ToString("F2");
        
    }

    void btnListen()
    {
        openSettingsMenu.GetComponent<Button>().onClick.AddListener(() =>
        {
            ingameSettingsCanvas.gameObject.SetActive(true);
            Debug.Log("open settings");
        });

        closeSettingsMenu.GetComponent<Button>().onClick.AddListener(() =>
        {
            ingameSettingsCanvas.gameObject.SetActive(false);
            Debug.Log("close settings");
        });

        exitToMainMenu.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            Debug.Log("exit to main menu");
        });
    }
}
