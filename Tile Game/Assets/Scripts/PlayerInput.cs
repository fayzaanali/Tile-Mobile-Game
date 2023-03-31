using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public Text textCorrectDisplay, playerInputDisplay;
    public TextMeshProUGUI livesDisplay;
    public Button[] inputBtn;
    public Button[] displayBtn;
    public int lives;
    public GameObject livesPrefab;

    public Color buttonDefaultColor, buttonActiveColor, buttonInactiveColor;

    string localInput, localOutput;
    public int[] randomInt;

    public int turnLeft;

    float timeBetweenPatterns = 1.5f; 
    int[] pattern; 

    void Start()
    {
        pattern = new int[3];
        
        getBtns();
        randomNumbers(pattern.Length);
        instantiateLives(3);
        
        StartCoroutine(PlayPattern(pattern));
    }

    IEnumerator PlayPattern(int[] pattern)
    {
        foreach (int num in pattern)
        {
            yield return new WaitForSeconds(timeBetweenPatterns);

            displayBtn[num].GetComponent<Image>().color = buttonActiveColor;
            yield return new WaitForSeconds(timeBetweenPatterns);
            displayBtn[num].GetComponent<Image>().color = buttonDefaultColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        livesDisplay.text = lives.ToString();
    }

    void randomNumbers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int random = Random.Range(1, displayBtn.Length); // pick a random button to add to the pattern
            Debug.Log(i + " - Random Ints are: " + random);
            pattern[i] = random;
        }
    }

    void getBtns()
    {
        foreach (Button btn in inputBtn)
        {
            if (btn.onClick != null)
            {
                btn.GetComponent<Button>().onClick.AddListener(() => {
                    string playerInput = btn.GetComponentInChildren<Text>().text;
                    //Debug.Log("Player Input: " + playerInput + " btn: " + btn);
                    playerInputDisplay.text = playerInput;

                    foreach (int playerOutput in randomInt)
                    {
                        localInput = playerInput + 1;
                        localOutput = playerOutput.ToString();

                        if (playerInput == playerOutput.ToString())
                        {
                            textCorrectDisplay.text = playerInput + " == " + playerOutput.ToString();
                        }
                    }
                    checkLives(playerInput);
                });
            }
        }
    }

    void checkLives(string playerInput)
    {
        if (playerInput != localOutput)
        {
            lives = lives - 1;
        }

        if (lives == 0)
        {
            Debug.Log("game over!");
            textCorrectDisplay.text = "game over";
            foreach (Button setBtn in inputBtn)
            {
                setBtn.gameObject.SetActive(false); // sets all btns inactive is the game is over
            }
        }
    }

    void instantiateLives(int lives)
    {
        for(int i = 1; i < lives; i++)
        {
            Instantiate(livesPrefab, new Vector3(i * 10.0f, 0, 0), Quaternion.identity);
        }
    }

}