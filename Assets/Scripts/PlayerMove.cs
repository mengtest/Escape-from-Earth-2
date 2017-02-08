using UnityEngine;
using System.Collections;

public enum TouchDir
{
    None,
    Left,
    Right,
    Up,
    Down
}

public class PlayerMove : MonoBehaviour
{
    public bool isSliding = false;
    public bool isJumping = false;
    public int nowLaneIndex = 1;
    public int targetLaneIndex = 1;
    public float moveSpeed = 100.0f;
    public float horizontalSpeed = 4.0f;

    private bool isUp = true;
    private float moveHorizontal = 0;
    private float slideTime = 1.4f;
    private float slideTimer = 0;
    private float jumpSpeed = 40f;
    private float nowHeight = 0;
    private float targetHeight = 25.0f;
    private float[] xOffset = new float[3] { -15.0f, 0, 15.0f };
    private TouchDir touchDir = TouchDir.None;
    private Vector3 lastMouseDown = Vector3.zero;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    void Update()
    {
        if (GameController.gameState == GameState.Start)
        {
            Vector3 targetPos = EnvManager.instance.forest1.GetNextPoint();
            targetPos = new Vector3(targetPos.x + xOffset[targetLaneIndex], targetPos.y, targetPos.z);
            Vector3 moveDir = targetPos - transform.position;
            //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

            MoveControl();
        }
    }

    void MoveControl()
    {
        TouchDir dir = GetTouchDir();
        if (targetLaneIndex != nowLaneIndex)
        {
            float moveLength = Mathf.Lerp(0, moveHorizontal, Time.deltaTime * horizontalSpeed);
            transform.position = new Vector3(transform.position.x + moveLength, transform.position.y, transform.position.z);
            moveHorizontal -= moveLength;
            if (Mathf.Abs(moveHorizontal) < 0.5f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                moveHorizontal = 0;
                nowLaneIndex = targetLaneIndex;
            }
        }
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > slideTime)
            {
                isSliding = false;
                slideTimer = 0;
            }
        }
        if (isJumping)
        {
            float y = Time.deltaTime * jumpSpeed;
            if (isUp)   //正在向上跳跃
            {
                player.position = new Vector3(player.position.x, player.position.y + y, player.position.z);
                nowHeight += y;
                if (targetHeight - nowHeight < 0.5f)
                {
                    isUp = false;
                    player.position = new Vector3(player.position.x, targetHeight, player.position.z);
                    nowHeight = targetHeight;
                }
            }
            else   //正在向下降落
            {
                player.position = new Vector3(player.position.x, player.position.y - y, player.position.z);
                nowHeight -= y;
                if (nowHeight - 0 < 0.5f)
                {
                    isJumping = false;
                    player.position = new Vector3(player.position.x, 0, player.position.z);
                    nowHeight = 0;
                }
            }
        }
    }

    TouchDir GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouseUp = Input.mousePosition;
            Vector3 touchOffset = mouseUp - lastMouseDown;
            if (Mathf.Abs(touchOffset.x) > 50 || Mathf.Abs(touchOffset.y) > 50)
            {
                if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y))
                {
                    if (touchOffset.x > 50)  //向右滑动，实际应该向左移动
                    {
                        if (targetLaneIndex > 0)
                        {
                            targetLaneIndex--;
                            moveHorizontal = -15.0f;
                        }
                        return TouchDir.Right;
                    }
                    else if (touchOffset.x < -50) //向左滑动，实际应该向右移动
                    {
                        if (targetLaneIndex < 2)
                        {
                            targetLaneIndex++;
                            moveHorizontal = 15.0f;
                        }
                        return TouchDir.Left;
                    }
                }
                else
                {
                    if (touchOffset.y > 50)  //向上滑动，实际应该向上移动
                    {
                        isJumping = true;
                        isUp = true;
                        return TouchDir.Up;
                    }
                    else if (touchOffset.y < -50) //向下滑动，实际应该向下移动
                    {
                        isSliding = true;
                        return TouchDir.Down;
                    }
                }
            }
        }
        return TouchDir.None;
    }
}
