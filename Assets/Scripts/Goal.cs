using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Vector2 nextLevelStartPos;
    private gameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameManager>();
            gm.lastCheckPointPos = nextLevelStartPos;

            //SceneManager.LoadScene(+1);
            if (sceneName == "SampleScene")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (sceneName == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }

        }
    }
}
