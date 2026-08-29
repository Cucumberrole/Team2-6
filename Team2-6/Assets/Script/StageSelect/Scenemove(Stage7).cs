using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove7 : MonoBehaviour
{
    public void ChangeScene()
    {
          SceneManager.LoadScene("PlayMap7");
        StageClear.Map7Play = true;
    }
}
