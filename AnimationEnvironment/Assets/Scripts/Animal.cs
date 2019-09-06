using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float moveSpeed;
    public int jumpForce;
    Vector3 currentDirection;
    Vector3[] directions = new Vector3[16];
    Rigidbody rb;

    float viewDistance = 1.5f;

    bool canMove;
    private void Start()
    {
        directions[0] = new Vector3(0, 0, 1);
        directions[1] = new Vector3(0.5f, 0, 1);
        directions[2] = new Vector3(1, 0, 1);
        directions[3] = new Vector3(1, 0, 0.5f);
        directions[4] = new Vector3(1, 0, 0);
        directions[5] = new Vector3(1, 0, -0.5f);
        directions[6] = new Vector3(1, 0, -1);
        directions[7] = new Vector3(0.5f, 0, -1);
        directions[8] = new Vector3(0, 0, -1);
        directions[9] = new Vector3(-0.5f, 0, -1);
        directions[10] = new Vector3(-1, 0, -1);
        directions[11] = new Vector3(-1, 0, -0.5f);
        directions[12] = new Vector3(-1, 0, 0);
        directions[13] = new Vector3(-1, 0, 0.5f);
        directions[14] = new Vector3(-1, 0, 1);
        directions[15] = new Vector3(-0.5f, 0, 1);


        currentDirection = directions[0];
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        for (int i = 0; i < 16; i++)
        {
            //Debug.DrawRay(transform.position, directions[i].normalized * viewDistance, Color.blue);
        }
        //Quaternion targetRotation = Quaternion.LookRotation(currentDirection * 90);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 20 * Time.deltaTime);

        transform.LookAt(transform.position + currentDirection);

        currentDirection = currentDirection.normalized;



        if (OnGround())
        {
            //Jump();
        }
        if (canMove)
        {

            Move();
        }
        else
            Stop();

        CheckRaycast(currentDirection);

        //Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

    void CheckRaycast(Vector3 direction)
    {
        if (Physics.Raycast(transform.position + new Vector3(0.25f, 0, 0), direction, viewDistance) || Physics.Raycast(transform.position + new Vector3(-0.25f, 0, 0), direction, viewDistance))
        {
            //int random =Random.Range(0, 7);
            canMove = false;
            currentDirection = directions[Random.Range(0, 16)];
        }
        else
        {
            canMove = true;
        }
        //Debug.DrawRay(transform.position + new Vector3(0.25f, 0, 0), direction * viewDistance, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(-0.25f, 0, 0), direction * viewDistance, Color.red);

    }

    bool OnGround()
    {
        //rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
        return Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z), -Vector3.up, 0.3f);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    void Stop()
    {
        rb.velocity = Vector3.zero;
    }
    void Move()
    {

        rb.velocity = new Vector3(currentDirection.x * moveSpeed, rb.velocity.y, currentDirection.z * moveSpeed);
        // transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
}
