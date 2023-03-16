using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Text textCorrectDisplay, playerInputDisplay;
    public Button[] inputBtn;
    public int lives;

    public Color buttonDefaultColor, buttonActiveColor, buttonInactiveColor;

    string localInput, localOutput;
    int[] randomInt = new int[3];


    public int turnTime;
    private IEnumerator coroutine;

    float startIndex = 0;
    float endIndex = 2; // needs to 1 less than randomInt[]
    int index;

    void Start()
    {
        randomNumbers(3);
        getBtns();
    }


    IEnumerator waiter(int randomInt, Button[] randomBtn)
    {
            randomBtn[randomInt].GetComponent<Image>().color = buttonActiveColor;
            yield return new WaitForSeconds(1.5f);
            randomBtn[randomInt].GetComponent<Image>().color = buttonDefaultColor;
    }

    // Update is called once per frame
    void Update()
    {

        if (startIndex < endIndex)
        {
            startIndex += 1 * Time.deltaTime;
            
            index = Mathf.RoundToInt(startIndex);
            StartCoroutine(waiter(randomInt[index], inputBtn));
        }

    }

    void randomNumbers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int random = Random.Range(1, amount);
            Debug.Log(i + " - Random Ints are: " + random);
            randomInt[i] = random;
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
                    //Debug.Log("Player Input: " + playerInput + " /// btn: " + btn);
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
}
