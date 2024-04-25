using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBoostRight : MonoBehaviour
{    
    //public bool isRight;
    [SerializeField] float Boost = 20f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))

        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Boost, ForceMode2D.Impulse);
        }
    }
    
}
