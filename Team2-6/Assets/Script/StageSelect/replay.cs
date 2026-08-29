using UnityEngine;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour
{
    public void ChangeScene()
    {
        if (StageClear.Map1Play)
        {
            SceneManager.LoadScene("PlayMap1");
        }
        else if (StageClear.Map2Play)
        {
            SceneManager.LoadScene("PlayMap2");
        }
        else if (StageClear.Map3Play)
        {
            SceneManager.LoadScene("PlayMap3");
        }
        else if (StageClear.Map4Play)
        {
            SceneManager.LoadScene("PlayMap4");
        }
        else if (StageClear.Map5Play)
        {
            SceneManager.LoadScene("PlayMap5");
        }
        else if (StageClear.Map6Play)
        {
            SceneManager.LoadScene("PlayMap6");
        }
        else if (StageClear.Map7Play)
        {
            SceneManager.LoadScene("PlayMap7");
        }
        else if (StageClear.Map8Play)
        {
            SceneManager.LoadScene("PlayMap8");
        }
    }
}
