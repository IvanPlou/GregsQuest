using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlienText : MonoBehaviour
{
    TextMeshPro alienText;
    
    void Start()
    {
        alienText.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {     
        if (other.tag == "Player")
       {
           alienText.gameObject.SetActive(true);
       }        
    }

    void OnTriggerExit2D(Collider2D other2)
    {
        if (other2.tag == "Player")
       {
           alienText.gameObject.SetActive(false);
       }  
    }
}
