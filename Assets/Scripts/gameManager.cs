using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager GMinstance;

    private void Awake()
    {
        if (GMinstance == null)
        {
            GMinstance = this;
        }
        else if (GMinstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void restartLevel(float delay)
    {
        StartCoroutine(restartLevelDelay(delay));
    }

    private IEnumerator restartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (sceneName == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }

    }
}
