using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Titlemove : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public float size = 1.05f;

    [Header("判定するオブジェクト")]
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    [Header("それぞれのUp移動先")]
    public Button upButton1;
    public Button upButton2;
    public Button upButton3;

    private Vector3 normalScale;
    private Button thisButton;

    void Start()
    {
        normalScale = transform.localScale;
        thisButton = GetComponent<Button>();

        ChangeNavigation();
    }

    void Update()
    {
        // オブジェクトの状態が途中で変わる場合にも対応
        ChangeNavigation();
    }

    void ChangeNavigation()
    {
        Navigation nav = thisButton.navigation;
        nav.mode = Navigation.Mode.Explicit;

        // object1がTrue
        if (object1 != null && object1.activeInHierarchy)
        {
            nav.selectOnUp = upButton1;
        }
        // object2がTrue
        else if (object2 != null && object2.activeInHierarchy)
        {
            nav.selectOnUp = upButton2;
        }
        // object3がTrue
        else if (object3 != null && object3.activeInHierarchy)
        {
            nav.selectOnUp = upButton3;
        }

        thisButton.navigation = nav;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = normalScale * size;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = normalScale;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}