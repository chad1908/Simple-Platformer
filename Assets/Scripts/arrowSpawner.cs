using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowSpawner : MonoBehaviour
{
    public arrowScript arrowPrefab;
    public float arrowSpeed;
    private float time = 0;
    public float arrowDelay;

    public bool shootLeft = false;
    public bool shoot;

    private Vector2 dir2;

    // Start is called before the first frame update
    void Start()
    {
        dir2 = new Vector2(arrowSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot == true)
        {
            if (time < Time.time)
            {
                arrowScript arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as arrowScript;
                arrow.direction = dir2;
                if (shootLeft == true)
                {
                    arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                time = Time.time + arrowDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shoot = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shoot = false;
        }
    }
}
