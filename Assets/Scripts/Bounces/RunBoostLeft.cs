using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBoostLeft : MonoBehaviour
{
    //public bool isLeft;
    [SerializeField] float Boost = 20f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))

        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Boost, ForceMode2D.Impulse);
        }
    }
}
