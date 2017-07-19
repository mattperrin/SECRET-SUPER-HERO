using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour {

    float spawnTimer = 0f;
    float spawnTimerTrigger = .5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTimerTrigger)
        {
            GameObject citizen = (GameObject)Instantiate(Resources.Load("PREFABS/CitizenPrefab"), GameObject.Find("SPAWN POINT").transform.position, Quaternion.identity);
            citizen.transform.parent = GameObject.Find("SPAWN POINT").transform;

            spawnTimer = 0f;
        }

    }
}
