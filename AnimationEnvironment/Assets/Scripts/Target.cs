using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int torque;
    public ParticleSystem arc1, arc2;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bomb"))
        {
            GetComponent<Rigidbody>().AddTorque(Vector3.up * torque);
            arc1.Play();
            arc2.Play();
            //Instantiate(arc1, transform.position, transform.rotation);
            //Instantiate(arc2, transform.position, transform.rotation);
        }

    }
}
