using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
       {           
           AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
           gameObject.SetActive(false);
           Destroy(gameObject);
       }
    }
}
