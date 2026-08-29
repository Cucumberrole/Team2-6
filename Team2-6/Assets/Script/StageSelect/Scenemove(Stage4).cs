using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove4: MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("PlayMap4");
        StageClear.Map4Play = true;
    }
}
