using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficFlowScript : MonoBehaviour {

    public enum OrientationType
    {
        Horizontal,
        Vertical
    }

    public OrientationType Orientation = OrientationType.Horizontal;

    public bool AllowNorth = true;
    public bool AllowWest = true;
    public bool AllowEast = true;
    public bool AllowSouth = true;
}
