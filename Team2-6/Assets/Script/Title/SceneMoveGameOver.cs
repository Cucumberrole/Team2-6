using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveGameOver : MonoBehaviour
{
    public void ChangeScene()
    {
        GameAudioManager.Instance?.PlayDecide();
        SceneManager.LoadScene("TitleScene");
    }
}
