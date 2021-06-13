using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 direction = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction;

        StartCoroutine(destroyArrow());
    }

    IEnumerator destroyArrow()
    {
        yield return new WaitForSeconds(5);
        Destroy(GameObject.FindWithTag("Arrow"));
    }
}
