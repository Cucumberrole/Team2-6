using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Titlemove : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public float size = 1.05f;

    [Header("判定に使うオブジェクト")]
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    [Header("上方向の移動先")]
    public Button upButton1;
    public Button upButton2;
    public Button upButton3;

    private Vector3 normalScale;
    private Button thisButton;
    private bool canPlaySelectSE;

    void Start()
    {
        normalScale = transform.localScale;
        thisButton = GetComponent<Button>();

        ChangeNavigation();
        StartCoroutine(EnableSelectSE());
    }

    void Update()
    {
        ChangeNavigation();
    }

    private IEnumerator EnableSelectSE()
    {
        yield return null;
        canPlaySelectSE = true;
    }

    void ChangeNavigation()
    {
        if (thisButton == null)
        {
            return;
        }

        Navigation nav = thisButton.navigation;
        nav.mode = Navigation.Mode.Explicit;

        if (object1 != null && object1.activeInHierarchy)
        {
            nav.selectOnUp = upButton1;
        }
        else if (object2 != null && object2.activeInHierarchy)
        {
            nav.selectOnUp = upButton2;
        }
        else if (object3 != null && object3.activeInHierarchy)
        {
            nav.selectOnUp = upButton3;
        }

        thisButton.navigation = nav;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = normalScale * size;

        if (canPlaySelectSE)
        {
            GameAudioManager.Instance?.PlaySelect();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = normalScale;
    }

    public void ChangeScene()
    {
        GameAudioManager.Instance?.PlayDecide();
        SceneManager.LoadScene("TitleScene");
    }
}
