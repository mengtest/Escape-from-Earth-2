using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    private Transform player;
    private Vector3 offset;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}
