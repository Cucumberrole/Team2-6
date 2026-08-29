using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove2 : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("PlayMap2");
        StageClear.Map2Play = true;
    }
}
