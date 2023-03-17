using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playBtn, levelSelectBtn, settingBtn, returnBtn;
    public Canvas playCanvas, levelCanvas, settingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        setDefaultCanvas();
        btnListen();
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
}
