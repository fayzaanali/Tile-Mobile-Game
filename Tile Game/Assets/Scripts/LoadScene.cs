using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public int sceneToLoad;

    IEnumerator LoadingAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneToLoad);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            // add progress bar here

            if (loadAsync.progress >= 0.9f)
            {
                loadAsync.allowSceneActivation = true;
            }

            yield return null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
