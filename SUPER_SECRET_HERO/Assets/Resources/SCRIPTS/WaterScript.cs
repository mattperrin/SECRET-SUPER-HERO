using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

    float changeTileTimer = 0f;
    float changeTileTrigger = .5f;
    int childIndex = 0;

	// Use this for initialization
	void Start () {
        //Debug.Log(transform.childCount.ToString());
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        childIndex = Random.Range(0, transform.childCount);
        transform.GetChild(childIndex).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        changeTileTimer += Time.deltaTime;
        if (changeTileTimer > changeTileTrigger)
        {
            transform.GetChild(childIndex).gameObject.SetActive(false);

            childIndex += 1;
            if (childIndex >= transform.childCount) { childIndex = 0; }

            transform.GetChild(childIndex).gameObject.SetActive(true);
            changeTileTimer = 0f;
        }
    }
}
