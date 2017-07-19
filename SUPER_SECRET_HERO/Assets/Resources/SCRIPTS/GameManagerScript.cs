using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public float ThiefTimeTrigger = 30f;
    float ThiefTimer = 0f;
    GameObject[] Citizens;
    GameObject Hero;
    public Vector3 HelpRotation;

    Text CountdownTextField;
    bool startCountdown = false;
    float countdownTimer = 15f;

    public bool skipTitles = false;
    float titleTimer = 6f;

    bool lost = false;
    bool won = false;

    public bool DisableTimer = false;

    public AudioClip TeamTaco;
    public AudioClip SuperSecretHero;
    public AudioClip NormalSong;
    public AudioClip FastSong;
    public AudioClip LosingSound;
    public AudioClip WinningSound;
    public AudioClip Help;

    GameObject thiefTarget;

    // Use this for initialization
    void Start () {
        Citizens = GameObject.FindGameObjectsWithTag("CITIZEN");
        Hero = GameObject.FindGameObjectWithTag("HERO");

        CountdownTextField = GameObject.Find("COUNTDOWN").GetComponent<Text>();
        CountdownTextField.text = "";

        if (!skipTitles)
        {
           // GameObject.Find("TEAMTACO").SetActive(true);
           // GameObject.Find("SECRETSUPERHEROLOGO").SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {


            SceneManager.LoadScene("TestScene");
        }
     
        if (lost || won)
        {
            return;
        }

        if (!skipTitles)
        {
            //Debug.Log(titleTimer.ToString());
            titleTimer -= Time.deltaTime;
            if (titleTimer < 3f)
            {
                try
                {
                    GameObject.Find("TEAMTACO").SetActive(false);
                    GetComponent<AudioSource>().PlayOneShot(SuperSecretHero);
                }
                catch { }
            }

            if (titleTimer < 0f)
            {
                try
                {
                    GameObject.Find("SECRETSUPERHEROLOGO").SetActive(false);
                }
                catch { }
                skipTitles = true;
                GetComponent<AudioSource>().clip = NormalSong;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
            return;
        }

        if (startCountdown)
        {
            countdownTimer -= Time.deltaTime;

            if(countdownTimer <= 0f && !DisableTimer)
            {
                GoToTimeFailure();
                return;
            }

            int intTime  = (int)countdownTimer;
            int seconds = (int)intTime % 60;
            int fraction = (int)((countdownTimer - seconds) * 100);
            //fraction = fraction % 1000;
            string timeText = string.Format("{0:00}:{1:00}", seconds, fraction);

            //string minSec = string.Format("{0}:{1:00}", (int)count % 60, (int)Mathf.Repeat(count, 1.0f));

            CountdownTextField.text = timeText; //count.ToString();
        }

        if (ThiefTimer != -1)
        {
            ThiefTimer += Time.deltaTime;
        }

        //thiefTarget = null;
        if (ThiefTimer > ThiefTimeTrigger)
        {
            float bestDistance = 0f;
            foreach(GameObject citizen in Citizens)
            {
                float currentDist = Vector3.Distance(citizen.transform.position, Hero.transform.position);
                if (currentDist >= bestDistance)
                {
                    thiefTarget = citizen;
                    bestDistance = currentDist;
                }
            }

            Vector3 tempThiefPos = (thiefTarget.transform.position);
            tempThiefPos.x -= .6f;


            Instantiate(Resources.Load("PREFABS/THIEF"), tempThiefPos, Quaternion.identity);            
            Instantiate(Resources.Load("PREFABS/HELP"), tempThiefPos, Quaternion.identity);
            thiefTarget.GetComponent<CitizenScript>().GettingRobbed();

            GameObject.Find("DistressIndicator").GetComponent<DistressDirectionScript>().ActivateDirection();



            startCountdown = true;
            ThiefTimer = -1;
        }


	}

    public void GoToTimeFailure()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = LosingSound;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();

        lost = true;
        GameObject.Find("YOULOSTTIME").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
        thiefTarget.GetComponent<CitizenScript>().StopScreamingAidan();
    }

    public void GoToSeenFailure()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = LosingSound;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();

        lost = true;
        GameObject.Find("YOULOSTSEEN").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);


        thiefTarget.GetComponent<CitizenScript>().StopScreamingAidan();
    }

    public void GoToWin()
    {
        if (lost) { return;  }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = WinningSound;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();

        thiefTarget.GetComponent<CitizenScript>().StopScreamingAidan();

        won = true;
        GameObject.Find("YOUWIN").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
       
    }

    public void NowSuper()
    {
        GetComponent<AudioSource>().clip = FastSong;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }
}
