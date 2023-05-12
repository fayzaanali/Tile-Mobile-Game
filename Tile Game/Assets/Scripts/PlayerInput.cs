using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public Text textCorrectDisplay, playerInputDisplay;
    public TextMeshProUGUI livesDisplay, scoreDisplay;
    public Button[] inputBtn;
    public Button[] displayBtn;
    public int lives;
    public GameObject livesPrefab;
    public GameObject tickGO, crossGO;
    public Button continueLevelBtn, playAgainBtn;

    public Color buttonDefaultColor, buttonActiveColor, buttonInactiveColor;

    float timeBetweenPatterns = 1.5f; 
    int[] pattern;
    int ptrnIndex = 0;
    static int playerScore;
    static int triesLeft = 3;

    void Start()
    {
        pattern = new int[3];
        
        getBtns();
        randomNumbers(pattern.Length);
        instantiateLives(3);
        continueLevelBtn.gameObject.SetActive(false);

        StartCoroutine(PlayPattern(pattern));

        resetLevel();
        playPatternAgain();

        tickGO.SetActive(false);
        crossGO.SetActive(false);
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

    IEnumerator isCorrect()
    {
        tickGO.SetActive(true);
        yield return new WaitForSeconds(1);
        tickGO.SetActive(false);
    }

    IEnumerator isIncorrect()
    {
        crossGO.SetActive(true);
        yield return new WaitForSeconds(1);
        crossGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        livesDisplay.text = lives.ToString();
        scoreDisplay.text = playerScore.ToString();
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
                        playerScore += 10;
                        ptrnIndex++;
                        //textCorrectDisplay.text = "correct";
                        StartCoroutine(isCorrect());
                    } else if (playerInput != pattern[ptrnIndex].ToString())
                    {
                        lives = lives - 1;
                        Destroy(livesPrefab.transform.GetChild(livesPrefab.transform.childCount - 1).gameObject);
                        
                        playerScore -= 5;
                        Debug.Log("input did not match pattern " + pattern[ptrnIndex].ToString() + "lives left: " + lives + " index at: " + ptrnIndex);
                        //textCorrectDisplay.text = "incorrect";
                        StartCoroutine(isIncorrect());
                    }

                    if (lives > 0 && ptrnIndex > 2)
                    {
                        Debug.Log("can keep going");
                        continueLevelBtn.gameObject.SetActive(true);
                    }

                    if (lives == 0)
                    {
                        Debug.Log("game over!");
                        textCorrectDisplay.text = "Game Over";
                        foreach (Button setBtn in inputBtn)
                        {
                            setBtn.gameObject.SetActive(false);
                            // todo: save score data when gameover.
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
            Vector3 position = livesPrefab.transform.position + Vector3.right * (i * 150.0f);
            GameObject livesInstanceGO = Instantiate(livesPrefab, position, Quaternion.identity);
            livesInstanceGO.transform.parent = GameObject.Find("Canvas_Main").transform;
        }
    }

    void resetLevel()
    {
        continueLevelBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            continueLevelBtn.gameObject.SetActive(false);
        });
    }

    void playPatternAgain()
    {
        if (triesLeft > 0)
        {
            playAgainBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                StartCoroutine(PlayPattern(pattern));
                triesLeft = triesLeft - 1;
                Debug.Log(triesLeft + " tries left");
            });
        } else if (triesLeft == 0)
        {
            playAgainBtn.gameObject.SetActive(false);
            Debug.Log(triesLeft + " tries left");
        }
    }
}