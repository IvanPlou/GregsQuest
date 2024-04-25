using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpThrough : MonoBehaviour
{
   public PlatformEffector2D effector;
   BoxCollider2D myBoxCollider;
   [SerializeField] float waitTime;
    
   void  Start()             
    {
        effector = GetComponent<PlatformEffector2D>();
        myBoxCollider = GetComponent<BoxCollider2D>(); 
    }

    void Update()
    {        
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) 
        {
            waitTime -= Time.deltaTime;
        }
        if(waitTime <= 0) 
        { 
            effector.rotationalOffset = 0;
            waitTime = 0.25f;
        }
    }
        
    void OnDownPlatform(InputValue value)
    {  
        if (value.isPressed && myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {         
            effector.rotationalOffset = 180f;
            waitTime = 0.25f;            
        }
    }

    void OnClimb(InputValue value)
    {
        if(value.isPressed) { effector.rotationalOffset = 0;}
    }

    
}
