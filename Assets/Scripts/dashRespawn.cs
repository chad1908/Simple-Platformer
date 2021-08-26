using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashRespawn : MonoBehaviour
{
    public GameObject dashPrefab;
    public bool shouldSpawn = true;

    private void Update()
    {
        spawnDash();
    }

    private void spawnDash()
    {       
        if (shouldSpawn == true)
        {
            Instantiate(dashPrefab, transform.position, transform.rotation);
            //Debug.Log("has spawned");
            shouldSpawn = false;
        }
    }
}
