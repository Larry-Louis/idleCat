using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 所有生产、消耗的资源
/// </summary>
public class Population : MonoBehaviour
{
    private readonly double growSpeed = 0.008;

    private int ID;
    public int tick = 0;
    private int hightick;
    private Display display;
    private Resource catbo;
    private int Max = 0;
    private double growSpeedNow =0;
    private double growNow = 0;
    private Text numText;
    private Text maxText;

    protected static int nowID = 0;

    public int Num = 0;

    private void Awake() {
        display = FindObjectOfType<Display>();

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        numText = transform.Find("num").GetComponent<Text>();
        maxText = transform.Find("max").GetComponent<Text>();

        hightick = display.hightick;
        catbo = display.resources[0];
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
            numText.text = Num.ToString();
            maxText.text = "/" + Max.ToString();

            if (catbo.GetNum() <= 10) {
                tick++;
                if (tick >= 250) {
                    Debug.Log(Num);
                    growSpeedNow = 0;
                    Num = Num > 1 ? Num - 1 : 0;
                    tick = 0;
                }
            } else {
                tick = 0;
                growSpeedNow = growSpeed;
            }
        }
    }

    public Population() {
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

    public void AddNum(int num) {
        Num += num;
    }

    public void ExpNum(int num) {
        if (Num >= num) {
            Num -= num;
        }
    }

    public int GetNum() {
        return Num;
    }

    public double GetGrowNow() {
        return growNow;
    }

    private int Grow() {
        if (Num >= max) {
            return max;
        } else {
            growNow += growSpeedNow;
            if (growNow >= 1f) {
                Num++;
                growNow = 0;
            }
            return Num;
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