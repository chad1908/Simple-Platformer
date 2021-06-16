using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signScript : MonoBehaviour
{
    public GameObject activateTextCanvas;
    public GameObject activateTutCanvas;

    private void Update()
    {
        if (activateTextCanvas.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.E))
            {
                activateTextCanvas.SetActive(false);
                activateTutCanvas.SetActive(true);          
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activateTextCanvas.SetActive(true);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activateTextCanvas.SetActive(false);
            activateTutCanvas.SetActive(false);
        }
    }
}
