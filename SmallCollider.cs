using UnityEngine;
using System.Collections;

public class SmallCollider : MonoBehaviour
{
    private PlayerAnimation pa;

    void Awake()
    {
        pa = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.obstacle && GameController.gameState == GameState.Start && pa.animationState == AnimationState.Slide)
        {
            GameController.gameState = GameState.End;
        }
    }
}
