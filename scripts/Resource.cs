using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 所有生产、消耗的资源
/// </summary>
public class Resource : MonoBehaviour
{
    private int ID;
    private Display display;
    private int tick = 0;
    private int hightick;
    private int upPerSecond;
    private string unitRe;
    private string unitMax;
    private string unitGrow;
    private Text numText;
    private Text maxText;
    private Text growText;

    protected static int nowID = 0;

    public List<Building> proBuilding;
    public List<float> productSpeed;
    public List<Professioner> proWorker;
    public List<float> workSpeed;
    public List<Population> populations;
    public float exSpeed;
    public double growSpeed = 0;
    public double Num = 0;
    public int Max = 5000;

    private void Awake() {
        display = FindObjectOfType<Display>();

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        hightick = display.hightick;
        upPerSecond = display.upPerSecond;

        numText = transform.Find("num").GetComponent<Text>();
        maxText = transform.Find("max").GetComponent<Text>();
        growText = transform.Find("grow").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        if (tick < hightick) {
            tick++;
        }
        else {
            Num = Grow();
            numText.text = Num >= 0 ? DisNum(Math.Abs(Num),ref unitRe) : '-' + DisNum(Math.Abs(Num), ref unitRe);
            maxText.text = "/" + DisNum(Max,ref unitMax);
            growText.text = growSpeed >= 0 ? '+' + DisNum(Math.Abs(growSpeed * upPerSecond), ref unitGrow) + " / 秒" : '-' + DisNum(Math.Abs(growSpeed * upPerSecond), ref unitGrow) + " / 秒";

            tick = 0;
        }
    }

    public static string DisNum(double num,ref string unit) {
        switch (unit) {
            default:
                if (num - Math.Truncate(num) < 0.01f) return String.Format("{0}", Math.Truncate(num));
                if (num >= 1000) { unit = "K"; }
                return String.Format("{0:F}", num);
            case "K":
                if (num < 1000) unit = "";
                else if (num >= 1000000) unit = "M";
                return String.Format("{0:F2}K", num / 1000);
            case "M":
                if (num < 1000000) unit = "K";
                else if (num >= 1000000000) unit = "B";
                return String.Format("{0:F2}M", num / 1000000);
            case "B":
                if (num < 1000000000) unit = "M";
                else if (num >= 1000000000000) unit = "T";
                return String.Format("{0:F2}B", num / 1000000000);
            case "T":
                if (num < 1000000000000) unit = "B";
                else if (num >= 1000000000000000) unit = "Qa";
                return String.Format("{0:F2}K", num / 1000000000000);
            case "Qa":
                if (num < 1000000000000000) unit = "T";
                else if (num >= 1000000000000000000) unit = "Qt";
                return String.Format("{0:F2}K", num / 1000000000000000);
            case "Qt":
                if (num < 1000000000000000000) unit = "Qa";
                else if (num >= 1000000000000000000000f) unit = "Sx";
                return String.Format("{0:F2}M", num / 1000000000000000000);
            case "Sx":
                if (num < 1000000000000000000000f) unit = "Qt";
                else if (num >= 1000000000000000000000000f) unit = "Se";
                return String.Format("{0:F2}B", num / 1000000000000000000000f);
            case "Se":
                if (num < 1000000000000000000000000f) unit = "Sx";
                else if (num >= 1000000000000000000000000000f) unit = "Oc";
                return String.Format("{0:F2}K", num / 1000000000000000000000000f);
            case "Oc":
                if (num < 1000000000000000000000000000f) unit = "Se";
                else if (num >= 1000000000000000000000000000000f) return "超过最大值！";
                return String.Format("{0:F2}K", num / 1000000000000000000000000000f);
        }
    }

    public Resource() {
        nowID++;
        ID = nowID;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public int Id {
        get { return ID; }
        set { ID = value; }
    }

    public string GetName() {
        return name;
    }

    public void AddNum(double num) {
        Num += num;
    }

    public void AddNum(int num) {
        Num += num;
    }

    public void ExpNum(double num) {
        if (Num >= num) {
            Num -= num;
        }
    }

    public void ExpNum(int num) {
        if (Num >= num) {
            Num -= num;
        } else {
            Num = 0;
        }
    }

    public double GetNum() {
        return Num;
    }

    private double Grow() {
        growSpeed = 0;
        for (int i = 0; i < proBuilding.Count; i++) {
            growSpeed += proBuilding[i].Num * productSpeed[i];
        }
        for (int i = 0; i < proWorker.Count; i++) {
            growSpeed += Professioner.pro[(int)proWorker[i].pros] * workSpeed[i];
        }
        if (populations.Count > 0) {
            growSpeed -= populations[0].GetNum() * exSpeed;
        }
        if (Num + growSpeed > max) {
            return max;
        } 
        else if (Num + growSpeed <= 0) {
            return 0;
        } else {
            return Num + growSpeed;
        }
    }

    public int max {
        get { return Max; }
        set { Max = value; }
    }

    public Transform GetTrans() { 
        return GetComponent<Transform>();
    }
}