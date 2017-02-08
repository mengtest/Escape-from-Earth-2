using UnityEngine;
using System.Collections;

public enum AnimationState
{
    Idle,
    Run,
    TrunLeft,
    TrunRight,
    Slide,
    Jump,
    Death
}

public class PlayerAnimation : MonoBehaviour
{
    public AnimationState animationState = AnimationState.Idle;
    private Animation anim;
    private PlayerMove pm;
    private bool isDeath = false;

    void Awake()
    {
        anim = transform.Find("Prisoner").GetComponent<Animation>();
        pm = GetComponent<PlayerMove>();
    }

    void Update()   //动画判断
    {
        if (GameController.gameState == GameState.Menu)
        {
            animationState = AnimationState.Idle;
        }
        else if (GameController.gameState == GameState.Start)
        {
            animationState = AnimationState.Run;
            if (pm.targetLaneIndex < pm.nowLaneIndex)   //向右动画
            {
                animationState = AnimationState.TrunLeft;
            }
            else if (pm.targetLaneIndex > pm.nowLaneIndex)   //向右动画
            {
                animationState = AnimationState.TrunRight;
            }
            else if (pm.isSliding)
            {
                animationState = AnimationState.Slide;
            }
            else if (pm.isJumping)
            {
                animationState = AnimationState.Jump;
            }
        }
        else if (GameController.gameState == GameState.End)
        {
            animationState = AnimationState.Death;
        }
    }

    void LateUpdate()   //动画播放
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                PlayIdle();
                break;
            case AnimationState.Run:
                PlayAnimation("run");
                break;
            case AnimationState.TrunLeft:
                anim["left"].speed = 1.5f;
                PlayAnimation("left");
                break;
            case AnimationState.TrunRight:
                anim["right"].speed = 1.5f;
                PlayAnimation("right");
                break;
            case AnimationState.Slide:
                PlayAnimation("slide");
                break;
            case AnimationState.Jump:
                PlayAnimation("jump");
                break;
            case AnimationState.Death:
                PlayDeath();
                break;
        }
    }

    void PlayIdle()
    {
        if (anim.IsPlaying("Idle_1") == false && anim.IsPlaying("Idle_2") == false)
        {
            anim.Play("Idle_1");
            anim.PlayQueued("Idle_2");
        }
    }

    void PlayDeath()
    {
        if (isDeath == false && anim.IsPlaying("death") == false)
        {
            anim.Play("death");
            isDeath = true;
        }
    }

    void PlayAnimation(string name)
    {
        if (anim.IsPlaying(name) == false)
        {
            anim.Play(name);
        }
    }
}
