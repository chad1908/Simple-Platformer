using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPos : MonoBehaviour
{
    private gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameManager>();
        transform.position = gm.lastCheckPointPos;
    }
}
