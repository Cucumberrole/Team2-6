using UnityEngine;

public class modoru : MonoBehaviour
{
    public GameObject Paper;
    public GameObject button;
    public void OnObject()
    {
        Paper.SetActive(false);
        button.SetActive(false);
    }
}
