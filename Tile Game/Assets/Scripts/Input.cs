using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour
{
    public Button btn;
    public Text btnText, debugText;
    int btnNum;

    // Start is called before the first frame update
    void Start()
    {
        btn.GetComponent<Button>().onClick.AddListener(TileNumber);
        for(int i = 0; i < 9; i++)
        {
            int randomNums = Random.Range(1, 9);   
            Debug.Log("RANDOM NUM: " + randomNums);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TileNumber()
    {
        debugText.text = btnText.text;
        int.Parse(btnText.text); // parse btn text to int.
        //Debug.Log(btnText.text);
    }
}
