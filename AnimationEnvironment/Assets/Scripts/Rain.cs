using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Vector3 followPosition = target.transform.position;
        followPosition.y += 7;
        transform.position = followPosition;

    }
}
