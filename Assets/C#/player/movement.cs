using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;
    private Vector2 moveDirection;
    public Animator ancaanim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if(myRigidbody.velocity.sqrMagnitude==0)
            ancaanim.SetFloat("Speed",0);
        else
            ancaanim.SetFloat("Speed",1);
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        ancaanim.SetFloat("Horizontal",moveX);
        ancaanim.SetFloat("Vertical",moveY);
        Move();
    }

    void Move()
    {
        myRigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

    }
}
