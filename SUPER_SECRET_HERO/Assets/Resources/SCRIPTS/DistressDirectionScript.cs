using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistressDirectionScript : MonoBehaviour {

    public void ActivateDirection()
    {
        transform.Find("Cube").transform.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            GameObject Thief = GameObject.FindGameObjectWithTag("THIEF");

            Vector3 targetDir = Thief.transform.position - transform.position;
            float step = 1000f * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            //Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        catch { }
    }
}
