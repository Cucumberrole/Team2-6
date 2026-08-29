using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove6 : MonoBehaviour
{
    public void ChangeScene()
    {
         SceneManager.LoadScene("PlayMap6");
        StageClear.Map6Play = true;
    }
}
