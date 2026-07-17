using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
