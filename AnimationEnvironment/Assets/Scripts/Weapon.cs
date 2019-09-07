using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
     public float shootForce;
    public Bomb bombPrefab;
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
        Bomb bullet = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(direction * shootForce);
    }
}
