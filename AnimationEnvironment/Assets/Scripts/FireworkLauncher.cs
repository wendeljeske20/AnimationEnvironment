using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkLauncher : MonoBehaviour
{
    public GameObject head;
    public Weapon weapon;
    //public GameObject target;

    //public int fireRange;

    public float fireInterval;
    float nextFireInterval;

    void Update()
    {
        nextFireInterval += Time.deltaTime;


        head.transform.LookAt(head.transform.position + Vector3.up);
        float interval = Random.Range(fireInterval - 1, fireInterval + 1);

        if (nextFireInterval >= interval)
        {
            Shoot();
            nextFireInterval = 0;
        }



    }

    void Shoot()
    {
        weapon.Shoot(weapon.firePoint.transform.forward);
    }
}
