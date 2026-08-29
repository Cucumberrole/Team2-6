using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove3 : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("PlayMap3");
        StageClear.Map3Play = true;
    }
}
