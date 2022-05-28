using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkDropdownMenu : MonoBehaviour
{
    private bool onIt = false;
    private bool onShow = false;
    private int tick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onIt && onShow) {
            tick++;
            if (tick == 20) {
                if (!onIt) {
                    gameObject.SetActive(false);
                    tick = 0;
                }
                onShow = false;
            }
        }
        else { tick = 0; }
    }

    public RectTransform Rect {
        get {
            return GetComponent<RectTransform>();
        }
    }

    public void Show() {
        gameObject.SetActive(true);
        onIt = true;
        onShow = true;
    }

    public void Hide() {
        onIt = false;
    }

    public void SetPos(float y) {
        transform.localPosition = new(transform.localPosition.x, y, 0);
    }

}
