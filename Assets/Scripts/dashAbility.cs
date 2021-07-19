using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashAbility : MonoBehaviour
{
    //private float nextSpawnTime;
    //private float spawnDelay = 5f;
    private SpriteRenderer SR;
    private CircleCollider2D CC;

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        CC = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            MovementController movementController = player.GetComponent<MovementController>();
            movementController.hasDash = true;
            SR.enabled = false;
            CC.enabled = false;
            //gameObject.SetActive(false);
            StartCoroutine(dashSpawnDelay());
            Debug.Log("pickup hit");
            //Destroy(gameObject);
        }
    }

    private IEnumerator dashSpawnDelay()
    {
        yield return new WaitForSeconds(5);
        SR.enabled = true;
        CC.enabled = true;
    }
}
