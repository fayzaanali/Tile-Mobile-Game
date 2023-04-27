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

    public int turnLeft;

    float timeBetweenPatterns = 1.5f; 
    int[] pattern;
    int ptrnIndex = 0;

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
            int random = Random.Range(1, displayBtn.Length);
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
                    playerInputDisplay.text = playerInput;

                    Debug.Log("input " + playerInput);
                    Debug.Log("pattern " + pattern[ptrnIndex]);
                    if (playerInput == pattern[ptrnIndex].ToString())
                    {
                        Debug.Log("input matched pattern " + pattern[ptrnIndex].ToString() + " lives left: " + lives + " index at: " + ptrnIndex);
                        // add score here later
                        ptrnIndex++;
                        textCorrectDisplay.text = "correct";
                    } else if (playerInput != pattern[ptrnIndex].ToString())
                    {
                        lives = lives - 1;
                        Debug.Log("input did not match pattern " + pattern[ptrnIndex].ToString() + "lives left: " + lives + " index at: " + ptrnIndex);
                        textCorrectDisplay.text = "incorrect";
                    }

                    if (lives == 0)
                    {
                        Debug.Log("game over!");
                        textCorrectDisplay.text = "game over";
                        foreach (Button setBtn in inputBtn)
                        {
                            setBtn.gameObject.SetActive(false);
                        }
                    }
                });
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