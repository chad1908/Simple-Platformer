using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public GameObject platformReveal;
    public GameObject section2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        platformReveal.SetActive(false);
    }
}
