using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour {

    float lifetime = 0;

	// Use this for initialization
	void Start () {

        if (lifetime != 0)
            Destroy(gameObject, lifetime);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
