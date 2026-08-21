using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public float waitTime = 3f;

    void Start()
    {
        Invoke(nameof(GameOver), waitTime);
    }

    void GameOver()
    {
        SceneManager.LoadScene("TitleScene");
    }
}