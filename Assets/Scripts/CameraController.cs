using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float speed = 10;
    public float lookSpeed = 10;


    public void Init(Transform mTarget)
    {
        target = mTarget;
    }

    public void LookAtTarget()
    {
        
        var dir = target.position - transform.position;

        var rot = Quaternion.LookRotation(dir, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.fixedDeltaTime);
    }

    public void MoveToTarget()
    {
        var pos = target.position + target.forward * offset.z + target.right * offset.x + target.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.fixedDeltaTime);
    }

    public void FixedTick()
    {

        LookAtTarget();
        MoveToTarget();
    }


}
