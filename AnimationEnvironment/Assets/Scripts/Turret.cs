using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject head;
    public Weapon weapon;
    public GameObject target;

    public int fireRange;

    public float fireInterval;
    float nextFireInterval;

    void Update()
    {
        nextFireInterval += Time.deltaTime;

        Vector3 targetPosition = target.transform.position;
        targetPosition.y += 1f;
        head.transform.LookAt(targetPosition);

        float distance = (transform.position - target.transform.position).sqrMagnitude;
        Debug.Log(distance);
        if (distance < fireRange * fireRange)
        {
            if (nextFireInterval >= fireInterval)
            {
                Shoot();
                nextFireInterval = 0;
            }


        }


    }

    void Shoot()
    {
        weapon.Shoot(weapon.firePoint.transform.forward);
    }
}
