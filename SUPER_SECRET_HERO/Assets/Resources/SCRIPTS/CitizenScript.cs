using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenScript : MonoBehaviour {

    GameObject targetPOI;
    float moveSpeed = 5f;
    GameObject[] POIS;
    bool BeingRobbed = false;

    // Use this for initialization
    void Start () {

        POIS = GameObject.FindGameObjectsWithTag("POINTOFINTEREST");

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).gameObject.SetActive(true);

        if (targetPOI == null)
        {
            GetANewTargetPOI();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (BeingRobbed)
        {

        }
        else
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                GetANewTargetPOI();
            }

            //DrawDebugLine(targetPOI);
            try
            {
                MoveTowardsPOI();

                if (Vector3.Distance(targetPOI.transform.position, transform.position) <= .5f)
                {
                    GetANewTargetPOI();
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message.ToString());
            }
        }
    }

    void MoveTowardsPOI()
    {
        Vector3 targetDir = targetPOI.transform.position - transform.position;
        float step = 10f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void GetANewTargetPOI()
    {
        int rnd = -1;
        bool matchFound = false;
        int attemptCounter = 0;

        do
        {
            attemptCounter += 1;

            rnd = UnityEngine.Random.Range(0, POIS.Length);
            if (!Physics.Linecast(transform.position, POIS[rnd].transform.position))
            {
                float dist = Vector3.Distance(POIS[rnd].transform.position, transform.position);
                if (dist > UnityEngine.Random.Range(2f, 10f) && dist < 40f)
                {
                    targetPOI = POIS[rnd];
                    matchFound = true;
                    return;
                }
            }

            if (attemptCounter > 200)
            {
                //WE NEED TO FIND BETTER APPROACH... DESTROY & RESPAWN?
                return;
            }
        } while (!matchFound);

    }

    void DrawDebugLine(GameObject poi)
    {
        if (!Physics.Linecast(transform.position, poi.transform.position))
        {
            Debug.DrawLine(transform.position, poi.transform.position, Color.cyan);
        }
        else
        {
            Debug.DrawLine(transform.position, poi.transform.position, Color.red);
        }
    }

    public void GettingRobbed()
    {
        BeingRobbed = true;

        GetComponent<AudioSource>().Play();
    }

    public void StopScreamingAidan()
    {
        GetComponent<AudioSource>().Stop();
    }
}
