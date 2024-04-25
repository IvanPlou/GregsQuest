using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1.5f;
    Animator myAnimator;
    [SerializeField] float runTime = 10f;
    [SerializeField] float idleTime = 0f;
    [SerializeField] AudioClip destroySFX;

    [SerializeField] float delayRespawn = 7f;
    [SerializeField] float delayDestroy = 0.5F;

    void Start()
    {
       myRigidBody = GetComponent<Rigidbody2D>();
       myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(runTime >= Mathf.Epsilon)
        {
            Run();
        }

        if(idleTime >= Mathf.Epsilon)
        {
            Idle();
        }

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;       
    }

    void Run()
    {
        myRigidBody.velocity = new Vector2 (moveSpeed, 0f);    
        runTime -= Time.deltaTime;

         if(runTime <= 0) {idleTime = 7f;}
    }

    void Idle()
    {
        myRigidBody.velocity = new Vector2 (0f, 0f);
        idleTime -= Time.deltaTime;

        if(idleTime <= 0) {runTime = 10f;}
    }    

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player") 
        && collision.gameObject.transform.localPosition.y >= GetComponent<Transform>().position.y)
        {        
        GetComponent<AudioSource>().PlayOneShot(destroySFX);
        GetComponent<SpriteRenderer>().flipY = true;
        Vector2 offSet = new Vector2 (Random.Range(40, 70), Random.Range(-40, 40));
        myRigidBody.velocity += offSet * Time.deltaTime;
        
        Invoke("Destroy", delayDestroy);
        Invoke("Respawn", delayRespawn);
        }        
    }    
 
    void Destroy()
    {     
        GetComponent<SpriteRenderer>().flipY = false;   
        gameObject.SetActive(false);         
        
    }    

    void Respawn()
    {
        gameObject.SetActive(true);
        runTime = 10f;
        idleTime = 0f;
    }    
}
