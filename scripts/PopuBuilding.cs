using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopuBuilding : MonoBehaviour
{
    private Display display;
    private Text text;
    private Highlight highlight;
    private MyButton bt;
    private Tooltip tooltip;
    private Population popu;
    private bool feed = true;
    private bool isShowTooltip = false;
    private int tick = 0;
    private int hightick;
    private int lagerSize;
    private double per;
    private float resourseXPo = -420f;
    private List<string> tooltipReq = new List<string>(10);

    public int Num;
    public int contriNum;
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
    void Start() {
        highlight = GameObject.FindObjectOfType<Highlight>();
        tooltip = FindObjectOfType<Tooltip>();
        popu = FindObjectOfType<Population>();
        bt = GetComponent<MyButton>();
        text = transform.Find("text").GetComponent<Text>();

        Num = 0;
        hightick = display.hightick;
        lagerSize = display.lagerSize;
    }

    // Update is called once per frame
    void Update() {
        if (tick >= hightick) {
            FeedBuild();
            TextChange();
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
        }
        else {
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
        bt.demand = string.Format("<size={0:D}><color=black>\n____________________________________</color></size>", lagerSize);
        for (int i = 0; i < reqRes.Count; i++) {
            reqRes[i].ExpNum((double)num * require[i]);
            require[i] = require[i] * Mathf.Pow(1 + reqAddRate, num);
            bt.demand += string.Format("<size={2:D}><color=black>\n{0,-23}{1,23:F2}</color></size>", reqRes[i].name, require[i], lagerSize);
        }
        Contribute(num);
        tooltip.TextDisplay(bt.demand);
    }

    /// <summary>
    /// 以（int）的形式在按钮文本中显示该建筑的数量
    /// </summary>
    private void TextChange() {
        per = popu.GetGrowNow();
        if (per != 0) {
            text.text = $"{this.name}  ({Num})  [{Math.Truncate(per * 100)}%]";
        } else {
            text.text = $"{this.name}  ({Num})";
        }
    }

    /// <summary>
    /// 更改该建筑产出的资源的增长速度
    /// </summary>
    /// <param name="re">资源表</param>
    /// <param name="numRe">对应资源增长速度改变值</param>
    /// <param name="n">该建筑的数量</param>
    private void Contribute(int n) {
        popu.max += n * contriNum;
    }

    public void Show() {
        gameObject.SetActive(true);

        popu = display.population;
        popu.Show();
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

    public string DefaultTooltipPrinter(int length, string color) {
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

    private string TooltipReq(int i, string color) {
        return string.Format("<size={2:D}><color=black>\n{0,-23}</color><color=" + color + ">{1,23:F2}</color></size>", reqRes[i].name, require[i], lagerSize);
    }
}
