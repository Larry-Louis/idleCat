using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    private Display display;
    private Canvas canvas;
    private Tooltip tooltip;
    private bool isShowTooltip;
    private Vector2 vector2 = new(20, -30);
    private readonly Color colorDis = new(0.5f, 0.5f, 0.5f, 1f);

    public string discribe;
    public string demand;
    public string effect;

    private void Awake() {
        display = FindObjectOfType<Display>();
    }
    // Start is called before the first frame update
    void Start() {
        canvas = display.canvas;
        tooltip = GameObject.FindObjectOfType<Tooltip>();
        isShowTooltip = false;
    }

    // Update is called once per frame
    void Update() {
        if (isShowTooltip == true) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out Vector2 position);
            tooltip.SetPos(position + vector2);
        }
    }

    public void OnPointerEnter() {
        tooltip.Show(discribe, demand, effect);
        isShowTooltip = true;
    }

    public void OnPointerExit() {
        tooltip.Hide();
        demand = "";
        isShowTooltip = false;
    }

    public void myOnEnable() {
        GetComponent<Button>().interactable = true;
        transform.Find("text").GetComponent<Text>().color = Color.black;
    }

    public void myDisEnable() {
        GetComponent<Button>().interactable = false;
        transform.Find("text").GetComponent<Text>().color = colorDis;
    }
}