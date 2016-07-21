using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour
{
    public GameObject[] obstacles;
    private Transform player;
    private WayPoints wayPoints;
    private float startLength = 50;
    private float minLength = 100;
    private float maxLength = 200;
    private int targetIndex;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        wayPoints = transform.Find("wayPoints").GetComponent<WayPoints>();
        targetIndex = wayPoints.wayPoints.Length - 2;
    }

    void Start()
    {
        CreateObstacle();
    }

    void CreateObstacle()
    {
        float startZ = transform.position.z - 3000;
        float endZ = startZ + 3000;
        float z = startZ + startLength;
        //没超过森林总长度，生成障碍物
        while (z < endZ)
        {
            z += Random.Range(minLength, maxLength);
            if (z > endZ)
            {
                break;
            }
            else
            {
                Vector3 position = GetPositionByZ(z);
                int type = Random.Range(0, obstacles.Length);
                GameObject newObstacle = (GameObject)Instantiate(obstacles[type], position, Quaternion.identity);
                newObstacle.transform.parent = transform;
            }
        }
    }

    Vector3 GetPositionByZ(float z)
    {
        Transform[] points = wayPoints.wayPoints;
        int index = 0;
        for (int i = 0; i < points.Length; i++)
        {
            //需要生成障碍物的两点之间的坐标
            if (z <= points[i].position.z && z >= points[i + 1].position.z)
            {
                index = i;
                break;
            }
        }
        //利用比例获得坐标 开始坐标 结束坐标 比例=（当前坐标-开始坐标）/（结束坐标-开始坐标）
        return Vector3.Lerp(points[index + 1].position, points[index].position, (z - points[index + 1].position.z) / (points[index].position.z - points[index + 1].position.z));
    }

    public Vector3 GetNextPoint()
    {
        while (true)
        {
            Transform[] points = wayPoints.wayPoints;
            //玩家和下一个点的距离
            if (points[targetIndex].position.z - player.position.z < 10)
            {
                targetIndex--;
                if (targetIndex < 0)
                {
                    //通知EnvManager生成森林
                    EnvManager.instance.CreateForest();
                    Destroy(gameObject, 2.0f);
                    return EnvManager.instance.forest1.GetNextPoint();
                }
            }
            else
            {
                return points[targetIndex].position;
            }
        }
    }
}
