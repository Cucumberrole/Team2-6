using UnityEngine;

public class StageClear : MonoBehaviour
{
    public static bool Map1Play;
    public static bool Map1Clear;
    public GameObject NotClear1;
    public GameObject Clear1;

    public static bool Map2Play;
    public static bool Map2Clear;
    public GameObject Map2;
    public GameObject NotClear2;
    public GameObject Clear2;
    public GameObject HibeMap2;

    public static bool Map3Play;
    public static bool Map3Clear;
    public GameObject Map3;
    public GameObject NotClear3;
    public GameObject Clear3;
    public GameObject HibeMap3;

    public static bool Map4Play;
    public static bool Map4Clear;
    public GameObject Map4;
    public GameObject NotClear4;
    public GameObject Clear4;
    public GameObject HibeMap4;

    public static bool Map5Play;
    public static bool Map5Clear;
    public GameObject Map5;
    public GameObject NotClear5;
    public GameObject Clear5;
    public GameObject HibeMap5;

    public static bool Map6Play;
    public static bool Map6Clear;
    public GameObject Map6;
    public GameObject NotClear6;
    public GameObject Clear6;
    public GameObject HibeMap6;

    public static bool Map7Play;
    public static bool Map7Clear;
    public GameObject Map7;
    public GameObject NotClear7;
    public GameObject Clear7;
    public GameObject HibeMap7;

    public static bool Map8Play;
    public static bool Map8Clear;
    public GameObject Map8;
    public GameObject NotClear8;
    public GameObject Clear8;
    public GameObject HibeMap8;

    private void Start()
    {
        if (Map1Play && Goal.isGoal)
        {
            Map1Clear = true;
        }

        if (Map2Play && Goal.isGoal)
        {
            Map2Clear = true;
        }

        if (Map3Play && Goal.isGoal)
        {
            Map3Clear = true;
        }

        if (Map4Play && Goal.isGoal)
        {
            Map4Clear = true;
        }

        if (Map5Play && Goal.isGoal)
        {
            Map5Clear = true;
        }

        if (Map6Play && Goal.isGoal)
        {
            Map6Clear = true;
        }

        if (Map7Play && Goal.isGoal)
        {
            Map7Clear = true;
        }

        if (Map8Play && Goal.isGoal)
        {
            Map8Clear = true;
        }
        Map1Play = false;
        Map2Play = false;
        Map3Play = false;
        Map4Play = false;
        Map5Play = false;
        Map6Play = false;
        Map7Play = false;
        Map8Play = false;

        if (Map1Clear)
        {
            NotClear1.SetActive(false);
            Clear1.SetActive(true);

            HibeMap2.SetActive(false);
            Map2.SetActive(true);
        }

        if (Map2Clear)
        {
            NotClear2.SetActive(false);
            Clear2.SetActive(true);

            HibeMap3.SetActive(false);
            Map3.SetActive(true);
        }

        if (Map3Clear)
        {
            NotClear3.SetActive(false);
            Clear3.SetActive(true);

            HibeMap4.SetActive(false);
            Map4.SetActive(true);
        }

        if (Map4Clear)
        {
            NotClear4.SetActive(false);
            Clear4.SetActive(true);

            HibeMap5.SetActive(false);
            Map5.SetActive(true);
        }

        if (Map5Clear)
        {
            NotClear5.SetActive(false);
            Clear5.SetActive(true);

            HibeMap6.SetActive(false);
            Map6.SetActive(true);
        }

        if (Map6Clear)
        {
            NotClear6.SetActive(false);
            Clear6.SetActive(true);

            HibeMap7.SetActive(false);
            Map7.SetActive(true);
        }

        if (Map7Clear)
        {
            NotClear7.SetActive(false);
            Clear7.SetActive(true);

            HibeMap8.SetActive(false);
            Map8.SetActive(true);
        }

        if (Map8Clear)
        {
            NotClear8.SetActive(false);
            Clear8.SetActive(true);
        }
    }
}