using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{

    float moveSpeed = 10f;

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    float ctrlThreshold = .2f;

    KeyCode currentKeyDown = KeyCode.Escape;


    public bool AreYouSuper = false;
    Vector3 MyOrigin;
    // Use this for initialization
    void Start()
    {
        MyOrigin = transform.Find("ORIGIN").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DebugLineCast();

        if (Input.GetKey(KeyCode.A))
        {
            if (currentKeyDown != KeyCode.A)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.D))
        {
            if (currentKeyDown != KeyCode.D)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (currentKeyDown != KeyCode.W)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (currentKeyDown != KeyCode.S)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
            BecomeASuperHero();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }

        /*
        GameObject[] POIS = GameObject.FindGameObjectsWithTag("POINTOFINTEREST");
        Debug.Log("COUNT: " + POIS.Length.ToString());


        foreach (GameObject poi in POIS)
        {
            float dist = Vector3.Distance(poi.transform.position, transform.position);

            if (dist < 20f)
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
        }
        */
    }

    private void DebugLineCast()
    {
        MyOrigin = transform.Find("ORIGIN").transform.position;
        //GameObject[] POIS = GameObject.FindGameObjectsWithTag("POINTOFINTEREST");
        GameObject[] POIS = GameObject.FindGameObjectsWithTag("CITIZEN");

        foreach (GameObject poi in POIS)
        {
            float dist = Vector3.Distance(poi.transform.position, MyOrigin);
            if (dist < 15f)
            {
                if (Physics.Linecast(MyOrigin, poi.transform.Find("ORIGIN").transform.position))
                {
                    Debug.DrawLine(MyOrigin, poi.transform.Find("ORIGIN").transform.position, Color.cyan);
                }
                else
                {
                    Debug.DrawLine(MyOrigin, poi.transform.Find("ORIGIN").transform.position, Color.red);
                }
            }
        }
    }

    private void BecomeASuperHero()
    {
        if (!AreYouSuper)
        {
            MyOrigin = transform.Find("ORIGIN").transform.position;

            GameObject[] citizens = GameObject.FindGameObjectsWithTag("CITIZEN");

            foreach (GameObject citizen in citizens)
            {
                Vector3 TargetPosition = citizen.transform.Find("ORIGIN").transform.position;

                float dist = Vector3.Distance(TargetPosition, MyOrigin);
                if (dist <= 20f)
                {
                    //Debug.DrawLine(MyOrigin, TargetPosition, Color.red);

                    if (!Physics.Linecast(MyOrigin, TargetPosition))
                    {
                        //UH OH YOU WERE SEEN
                        GameObject.Find("GAMEMANAGER").GetComponent<GameManagerScript>().GoToSeenFailure();
                        return;
                    }
                }
            }

            transform.Find("Garret2").gameObject.SetActive(false);
            transform.Find("Garret3").gameObject.SetActive(true);

            moveSpeed += 10;
            GameObject.Find("CAMERAGO").GetComponent<CameraFollowScript>().moveSpeed += 10;

            transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
            GameObject.Find("GAMEMANAGER").GetComponent<GameManagerScript>().NowSuper();
        }

        
        AreYouSuper = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (AreYouSuper)
        {
            if (other.tag == "THIEF")

            {
                GameObject.Find("GAMEMANAGER").GetComponent<GameManagerScript>().GoToWin();
            }
        }
    }


}
