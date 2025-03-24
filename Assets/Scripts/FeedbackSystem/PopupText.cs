using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public static PopupText instance;
    [SerializeField] private TMP_Text textPrefab;

    private void Awake()
    {
        instance = this;
    }
    public void ShowMsg(string Text, Vector3 wldPos)
    {
        Vector3 dir =  wldPos - Camera.main.transform.position;
        var txt = Instantiate(textPrefab, wldPos, Quaternion.LookRotation(dir));;
        txt.text = Text;
        txt.transform.LeanMoveY(txt.transform.position.y + 2.5f, 0.85f).setEase(LeanTweenType.easeOutCubic);
        Destroy(txt.gameObject, 1.25f);
    }

    [ContextMenu("Test")]
    public void Test()
    {
        int rndDmg = Random.Range(1, 20);
        ShowMsg(rndDmg.ToString(), Vector3.up * 1.5f);
    }
}
