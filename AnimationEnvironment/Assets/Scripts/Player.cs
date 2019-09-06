using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed;

    public int jumpForce;

    public float mouseSensitivity;
    bool canJump = true;
    Vector3 screenPoint;
    Vector3 offset;
    GameObject draggedObject;

    Color lastObjectColor;

    Rigidbody rigidBody;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        Move();
        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
            Jump();





        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 10, Color.red);

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 10))
            {

                if (hit.collider.gameObject.layer == 8)
                    draggedObject = hit.collider.gameObject;

                if (draggedObject)
                {
                    PickObject();

                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (draggedObject)
            {
                DropObject();
            }

        }


        if (Input.GetMouseButton(1) && draggedObject)
        {
            HoldObject();


        }

    }

    void RunObjectSpinAnimation(GameObject go)
    {
        go.transform.Rotate(Vector3.up * 200 * Time.deltaTime, Space.World);
        go.transform.eulerAngles = new Vector3(0, go.transform.eulerAngles.y, 0);


    }

    void PickObject()
    {
        lastObjectColor = draggedObject.GetComponent<MeshRenderer>().material.color;
        draggedObject.GetComponent<MeshRenderer>().material.color = Color.red;
        screenPoint = Camera.main.WorldToScreenPoint(draggedObject.transform.position);
        offset = draggedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void HoldObject()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if (draggedObject)
        {
            RunObjectSpinAnimation(draggedObject);
            draggedObject.transform.position = curPosition;
        }
    }

    void DropObject()
    {
        draggedObject.GetComponent<MeshRenderer>().material.color = lastObjectColor;
        draggedObject = null;
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
    }


    private void Move()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);

    }

    void CameraMovement()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
        Camera.main.transform.Rotate(-Vector3.right * Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);
    }
}
