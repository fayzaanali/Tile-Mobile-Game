using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playBtn, levelSelectBtn, settingBtn;
    public Canvas playCanvas, levelCanvas, settingCanvas;
    public Button[] exitButtonArray;
    // Start is called before the first frame update
    void Start()
    {
        setDefaultCanvas();
        btnListen();
        exitToMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setDefaultCanvas()
    {
        playCanvas.gameObject.SetActive(true);

        levelCanvas.gameObject.SetActive(false);
        settingCanvas.gameObject.SetActive(false);
    }

    void btnListen()
    {
        playBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            playCanvas.gameObject.SetActive(true);

            levelCanvas.gameObject.SetActive(false);
            settingCanvas.gameObject.SetActive(false);
        });

        levelSelectBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            levelCanvas.gameObject.SetActive(true);

            playCanvas.gameObject.SetActive(false);
            settingCanvas.gameObject.SetActive(false);
        });

        settingBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            settingCanvas.gameObject.SetActive(true);

            levelCanvas.gameObject.SetActive(false);
            playCanvas.gameObject.SetActive(false);
        });
    }

    void exitToMainMenu()
    {
        foreach (Button btn in exitButtonArray)
        {
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("exiting");
                setDefaultCanvas();
            });
        }
    }
}
