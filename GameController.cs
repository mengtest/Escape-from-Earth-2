using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
    Menu,
    Start,
    End
}

public class GameController : MonoBehaviour
{
    public static GameState gameState = GameState.Menu;
    public Image start;
    public Image end;
    private AudioClip ac;

    void Start()
    {
        end.enabled = false;
    }

    void Update()
    {
        if (gameState == GameState.Menu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Start;
                start.enabled = false;
            }
        }
        else if (gameState == GameState.End)
        {
            end.enabled = true;
			if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Menu;
				end.enabled = false;
                SceneManager.LoadScene(0);
            }
        }
    }
}
