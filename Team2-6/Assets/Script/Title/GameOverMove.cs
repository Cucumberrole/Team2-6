using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveGameOver : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
