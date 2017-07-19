using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignageScript : MonoBehaviour {

    float RotationSpeed = 150f;
    float rotationtimer = 0f;
    float rotationtimertrigger = 30f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right * (RotationSpeed * Time.deltaTime));

        rotationtimer += 1;
        if (rotationtimer >= rotationtimertrigger)
        {
            RotationSpeed *= -1;
            rotationtimer = 0f;
        }
    }
}
