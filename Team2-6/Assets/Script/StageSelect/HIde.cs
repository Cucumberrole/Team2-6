using UnityEngine;

public class HIde : MonoBehaviour
{
    public GameObject hide;
    public GameObject hide1;
    public void OnObject()
    {
        hide.SetActive(false);
        hide1.SetActive(false);
    }
}
