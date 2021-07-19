using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashRespawn : MonoBehaviour
{
    public GameObject dashPrefab;
    public bool shouldSpawn = true;
    //private float spawnDelay = 5f;   
    //private float nextSpawnTime;



    private void Update()
    {
        spawnDash();
    }

    private void spawnDash()
    {       
        if (shouldSpawn == true)
        {
            //StartCoroutine(dashSpawnDelay());           
            //nextSpawnTime = Time.time + spawnDelay;
            Instantiate(dashPrefab, transform.position, transform.rotation);
            //Debug.Log("has spawned");
            shouldSpawn = false;
        }
    }

    //private IEnumerator dashSpawnDelay (float spawnDelay = 5f)
    //{
    //    yield return new WaitForSeconds(spawnDelay);
    //}
}
