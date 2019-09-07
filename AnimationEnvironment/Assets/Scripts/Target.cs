using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int torque;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bomb"))
            GetComponent<Rigidbody>().AddTorque(Vector3.up * torque);
    }
}
