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

    public Weapon weapon;

    public ParticleSystem shock;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.transform.LookAt(Camera.main.transform.position + transform.forward);
    }

    void Update()
    {

        //Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.eulerAngles.x, -45, 45),  Camera.main.transform.eulerAngles.y,  Camera.main.transform.eulerAngles.z);
        Move();
        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
            Jump();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

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
                draggedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        go.transform.Rotate(Vector3.up * 150 * Time.deltaTime, Space.World);
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

        shock.Play();
    }

    void DropObject()
    {
        draggedObject.GetComponent<MeshRenderer>().material.color = lastObjectColor;
        draggedObject = null;
        shock.Stop();
    }

    void Shoot()
    {
        //RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit, 10))
        // {

        // }
        // if (!hit.collider)
        //     weapon.Shoot((weapon.firePoint.position - hit.point).normalized);
        // else
        weapon.Shoot(ray.direction);// + new Vector3(0.1f, 0, 0));

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

        Vector3 cameraEuler = Camera.main.transform.eulerAngles;

        if (cameraEuler.x > 60 && cameraEuler.x < 200)
            cameraEuler.x = 60;
        else if (cameraEuler.x < 300 && cameraEuler.x > 200)
            cameraEuler.x = 300;

        Camera.main.transform.eulerAngles = cameraEuler;
        Camera.main.transform.Rotate(-Vector3.right * Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);

    }
}
