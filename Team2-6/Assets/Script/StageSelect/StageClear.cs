using UnityEngine;
using UnityEngine.Tilemaps;

public class StageClear : MonoBehaviour
{
    public bool Map1Clear;
    public GameObject NotClear1;
    public GameObject Clear1; 
    public bool Map2Clear;
    public GameObject NotClear2;
    public GameObject Clear2;
    public GameObject HibeMap2;
    public bool Map3Clear;
    public GameObject NotClear3;
    public GameObject Clear3;
    public GameObject HibeMap3;
    public bool Map4Clear;
    public GameObject NotClear4;
    public GameObject Clear4;
    public GameObject HibeMap4;
    public bool Map5Clear;
    public GameObject NotClear5;
    public GameObject Clear5;
    public GameObject HibeMap5;
    public bool Map6Clear;
    public GameObject NotClear6;
    public GameObject Clear6;
    public GameObject HibeMap6;
    public bool Map7Clear;
    public GameObject NotClear7;
    public GameObject Clear7;
    public GameObject HibeMap7;
    public bool Map8Clear;
    public GameObject NotClear8;
    public GameObject Clear8;
    public GameObject HibeMap8;
    private void Start()
    {
        if (Map1Clear)
        {
            NotClear1.SetActive(false);
            Clear1.SetActive(true);
            HibeMap2.SetActive(false);
        }
        if (Map2Clear)
        {
            NotClear2.SetActive(false);
            Clear2.SetActive(true);
            HibeMap3.SetActive(false);
        }
        if (Map3Clear)
        {
            NotClear3.SetActive(false);
            Clear3.SetActive(true);
            HibeMap4.SetActive(false);
        }
        if (Map4Clear)
        {
            NotClear4.SetActive(false);
            Clear4.SetActive(true);
            HibeMap5.SetActive(false);
        }
        if (Map5Clear)
        {
            NotClear5.SetActive(false);
            Clear5.SetActive(true);
            HibeMap6.SetActive(false);
        }
        if (Map6Clear)
        {
            NotClear6.SetActive(false);
            Clear6.SetActive(true);
            HibeMap7.SetActive(false);
        }
        if (Map7Clear)
        {
            NotClear7.SetActive(false);
            Clear7.SetActive(true);
            HibeMap8.SetActive(false);
        }
        if (Map8Clear)
        {
            NotClear8.SetActive(false);
            Clear8.SetActive(true);
        }
    }

}

