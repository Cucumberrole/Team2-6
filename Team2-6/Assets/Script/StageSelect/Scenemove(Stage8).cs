using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove8 : MonoBehaviour
{
    public void ChangeScene()
    {
           SceneManager.LoadScene("PlayMap8");
        StageClear.Map8Play = true;
    }
}
