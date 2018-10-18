using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrail : MonoBehaviour {

    public GameObject trailObject;
    public float newObjectDistance = 1f;
    public float sizeRatio = 1.0f;

    private Vector3 currentObjectStart;
    private GameObject currentObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 if(currentObject == null)
        {
            currentObject = Instantiate(trailObject, transform.position, Quaternion.identity) as GameObject;
            currentObjectStart = currentObject.transform.position;
        }
        else
        {
            var toTransformVector = transform.position - currentObjectStart;


            if (toTransformVector == Vector3.zero)
                toTransformVector = Vector3.forward;

            currentObject.transform.rotation = Quaternion.LookRotation(toTransformVector, Vector3.up);

            Vector3 newScale = currentObject.transform.localScale;
            float dist = (currentObjectStart - transform.position).magnitude;
            newScale.z = dist * sizeRatio;
            currentObject.transform.localScale = newScale ;

            if (dist >= newObjectDistance)
            {
                currentObject = Instantiate(trailObject, transform.position, Quaternion.identity) as GameObject;
                currentObjectStart = currentObject.transform.position;
            }
        }
	}
}
