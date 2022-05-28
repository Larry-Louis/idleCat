using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {
    private Display display;
    private Highlight highlight;
    private MyButton bt;
    private Tooltip tooltip;
    private bool isShowTooltip = false;
    private bool feed = true;
    private int tick = 0;
    private int hightick;
    private int lagerSize;
    private float resourseXPo = -420f;
    public string unitRef;
    private List<string> tooltipReq = new List<string>(10);
    public List<string> unit;

    public int Num;
    public float reqAddRate;
    public List<Resource> reqRes;
    public List<double> require;
    public List<Resource> conRes;
    public List<double> conResNum;

    private void Awake() {
        display = FindObjectOfType<Display>();

        gameObject.SetActive(false); 
        
    }
    // Start is called before the first frame update
    void Start()
    {
        display = FindObjectOfType<Display>();
        highlight = GameObject.FindObjectOfType<Highlight>();
        bt = GetComponent<MyButton>();
        tooltip = FindObjectOfType<Tooltip>();

        gameObject.SetActive(false);

        Num = 0;
        
        hightick = display.hightick;
        lagerSize = display.lagerSize;
        unit = new List<string>(reqRes.Count);
        for (int i = 0; i < reqRes.Count; i++) {
            unit.Add("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tick >= hightick) {
            FeedBuild();
        } else {
            tick++;
        }
    }

    private void FeedBuild() {
        if (isShowTooltip == true) {
            for (int i = 0; i < reqRes.Count; i++) {
                if (reqRes[i].GetNum() < require[i]) {
                    feed = false;
                    tooltipReq[i + 1] = TooltipReq(i, "red");
                }
                else {
                    tooltipReq[i + 1] = TooltipReq(i, "black");
                }
            }
            if (feed == true) {
                bt.myOnEnable();
            }
            else {
                bt.myDisEnable();
            }
            tooltip.TextDisplay(combine());
            feed = true;
        } else {
            for (int i = 0; i < reqRes.Count; i++) {
                if (this.reqRes[i].GetNum() < this.require[i]) {
                    bt.myDisEnable();
                    return;
                }
            }
            bt.myOnEnable();
        }
    }

    public void Build(int num) {
        Num += num;
        TextChange();
        bt.demand = string.Format("<size={0:D}><color=black>\n____________________________________</color></size>", lagerSize);
        for (int i = 0; i < reqRes.Count; i++) {
            reqRes[i].ExpNum((double)num * require[i]);
            require[i] = require[i] * Mathf.Pow(1 + reqAddRate, num);
            unitRef = unit[i];
            bt.demand += string.Format("<size={2:D}><color=black>\n{0,-21}{1,21:F2}</color></size>", reqRes[i].name, Resource.DisNum(require[i], ref unitRef), lagerSize);
            unit[i] = unitRef;
        }
        tooltip.TextDisplay(bt.demand);
    }

    /// <summary>
    /// 以（int）的形式在按钮文本中显示该建筑的数量
    /// </summary>
    private void TextChange() {
        transform.Find("text").GetComponent<Text>().text = $"{name}  ({Num})";
    }

    public void Show() {
        gameObject.SetActive(true);
        for (int i = 0; i < reqRes.Count; i++) {
            reqRes[i].Show();
        }
        for (int i = 0; i < conRes.Count; i++) {
            conRes[i].Show();
        }
    }

    /// <summary>
    /// 当指针进入时
    /// </summary>
    public void OnPointerEnter() {
        for (int i = 0; i < reqRes.Count; i++) {
            highlight.SetPos(new(resourseXPo, reqRes[i].GetTrans().position.y - 490, 0), i);
        }
        highlight.Show(reqRes.Count);
        DefaultTooltipPrinter(reqRes.Count, "black");
        isShowTooltip = true;
    }
    
    /// <summary>
    /// 当指针退出时
    /// </summary>
    public void OnPointerExit() {
        highlight.Hide();
        isShowTooltip = false;
    }

    public string DefaultTooltipPrinter(int length,string color) {
        tooltipReq = new List<string>(0);
        if (length == 0) return combine();
        tooltipReq.Add(string.Format("<size={0:D}><color=black>\n____________________________________</color></size>", lagerSize));
        for (int i = 0; i < length; i++) {
            tooltipReq.Add(TooltipReq(i, color));
        }
        return combine();
    }

    private string combine() {
        string tooltip = "";
        for (int i = 0; i < tooltipReq.Count; i++) {
            tooltip += tooltipReq[i];
        }
        return tooltip;
    }

    private string TooltipReq(int i,string color) {
        unitRef = unit[i];
        string res =  string.Format("<size={2:D}><color=black>\n{0,-21}</color><color=" + color + ">{1,21:F2}</color></size>", reqRes[i].name, Resource.DisNum(require[i], ref unitRef), lagerSize);
        unit[i] = unitRef;
        return res;
    }
}
