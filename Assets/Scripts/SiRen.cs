using UnityEngine;
using System.Collections;

public class SiRen : MonoBehaviour
{
    public float rotateSpeed = 360.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
