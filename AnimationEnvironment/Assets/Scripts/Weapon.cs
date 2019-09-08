using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
     public float shootForce;
    public Explosive explosivePrefab;
    public Transform firePoint;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 direction)
    {
        Explosive explosive = Instantiate(explosivePrefab, firePoint.position, Quaternion.identity);
        explosive.GetComponent<Rigidbody>().AddForce(direction * shootForce);
    }
}
