using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{

    public enum DirectionPointing
    {
        Ignore,
        North,
        East,
        South,
        West
    }
    public DirectionPointing DirectionToPointTo = DirectionPointing.Ignore;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag != "POINTOFINTEREST")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        int rnd = 0;
        bool matchFound = false;
        do
        {
            rnd = Random.Range(0, transform.childCount);
            if (transform.GetChild(rnd).gameObject.tag != "POINTOFINTEREST")
            {
                matchFound = true;
            }

        } while (!matchFound);


        transform.GetChild(rnd).gameObject.SetActive(true);

        switch (DirectionToPointTo)
        {
            case (DirectionPointing.North):
                transform.Rotate(0, 180f, 0);
                break;

            case (DirectionPointing.East):
                transform.Rotate(0, 270f, 0);
                break;

            case (DirectionPointing.South):
                //DO NOTHING
                break;

            case (DirectionPointing.West):
                //yield 90
                transform.Rotate(0, 90f, 0);
                break;

        }


    }
}
