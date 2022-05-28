using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text Tiptext;
    private Text Tooltiptext;
    private CanvasGroup canvasGroup;
    private readonly float smooth = 10;
    private float tipAlpha;
    private string disStr;
    private string effStr;

    // Start is called before the first frame update
    void Start()
    {
        Tiptext = GetComponent<Text>();
        Tooltiptext = transform.Find("content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha != tipAlpha) {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, tipAlpha, smooth * Time.deltaTime);
            if (canvasGroup.alpha - tipAlpha <= 0.01f) {
                canvasGroup.alpha = tipAlpha;
            }
        }
    }

    public void TextDisplay(string demand) {
        Tiptext.text = disStr + demand + effStr;
        Tooltiptext.text = disStr + demand + effStr;
    }

    public void Show(string str, string demand, string eff) {
        disStr = str;
        effStr = eff;
        TextDisplay(demand);
        tipAlpha = 1;
    }

    public void Hide() {
        tipAlpha = 0;
        disStr = "";
    }

    public void SetPos(Vector3 pos) {
        transform.localPosition = pos;
    }
}
