using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    private Transform child;
    private int num = 0;
    private int i;
    private Image image;
    private Color startColor = new (1f, 0.75f, 0f, 0.3f);
    private float startAlpha;
    private float highlightAlpha;
    private readonly float smooth = 10;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        image = child.GetComponent<Image>();
        startAlpha = startColor.a;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (image.color.a != highlightAlpha) {
            image.color = new (startColor.r, startColor.g, startColor.b, Mathf.Lerp(image.color.a, highlightAlpha, smooth * Time.deltaTime));
            if (image.color.a - highlightAlpha <= 0.01f) {
                image.color = new (startColor.r, startColor.g, startColor.b, highlightAlpha);
            }
            i = num;
            while (i > 0) {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = image.color;
                i--;
            }
        }
    }

    public void Show(int i) {
        highlightAlpha = startAlpha;
        num = i - 1;
    }

    public void Hide() {
        highlightAlpha = 0;
    }

    public void SetPos(Vector3 pos, int i) {
        transform.GetChild(i).localPosition = pos;
    }
}
