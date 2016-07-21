using UnityEngine;
using System.Collections;

public class PoliceCar : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isPlay = false;

    void Update()
    {
        if (isPlay == false && GameController.gameState == GameState.End)
        {
            isPlay = true;
            audioSource.Play();
        }
    }
}
