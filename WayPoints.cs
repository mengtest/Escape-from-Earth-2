using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour
{
    public Transform[] wayPoints;

    void OnDrawGizmos()
    {
        iTween.DrawPath(wayPoints);
    }
}
