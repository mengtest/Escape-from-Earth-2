using UnityEngine;
using System.Collections;

public class EnvManager : MonoBehaviour
{
    public static EnvManager instance;
    public Forest forest1;
    public Forest forest2;
    public GameObject[] forests;
    private int forestCount = 2;

    void Awake()
    {
        instance = this;
    }

    public void CreateForest()
    {
        forestCount++;
        int type = Random.Range(0, forests.Length);
        GameObject newForest = (GameObject)Instantiate(forests[type], new Vector3(0, 0, 3000 * forestCount), Quaternion.identity);
        forest1 = forest2;
        forest2 = newForest.GetComponent<Forest>();
    }
}
