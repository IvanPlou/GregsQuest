using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostDown : MonoBehaviour
{
    [SerializeField] float Boost = 30f;

    void OnColliderEnter2D(Collider2D collision)

    {
        if (collision.gameObject.CompareTag("Player"))

        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * Boost, ForceMode2D.Impulse);
        }
    }

}
