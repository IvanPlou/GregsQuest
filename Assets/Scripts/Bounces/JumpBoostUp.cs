using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostUp : MonoBehaviour
{
    [SerializeField] float Boost = 30f;    
    
    void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Boost, ForceMode2D.Impulse);
        }
    }

}

