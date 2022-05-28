using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Professioner : MonoBehaviour
{
    private Display display;
    private Job job;
    private Text text;
    private int tick = 0;
    private int hightick;
    private int worker = 0;
    private int free = 0;
    private WorkDropdownMenu addPanel;
    private WorkDropdownMenu minPanel;
    
    public static int[] pro = new int[10];
    public enum Pros
    {
        Null,
        ·¥Ä¾¹¤,
        Å©Ãñ,
    };
    public Pros pros;

    private void Awake() {
        display = FindObjectOfType<Display>(true);
        job = FindObjectOfType<Job>(true);
        addPanel = transform.parent.GetChild(0).GetComponent<WorkDropdownMenu>();
        minPanel = transform.parent.GetChild(1).GetComponent<WorkDropdownMenu>();
    }

    // Start is called before the first frame update
    void Start() {
        hightick = display.hightick;
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        addPanel.Rect.SetAsLastSibling();
        addPanel.Hide();
        minPanel.Rect.SetAsLastSibling();
        minPanel.Hide();
    }

    // Update is called once per frame
    void Update() {
        if (tick >= hightick) {
            text.text = pros.ToString() + " (" + pro[(int)pros] + ")";
            tick = 0;
        } else {
            tick++;
        }
    }

    public void WorkerAdd(int i) {
        free = job.free;
        worker = job.worker;
        if (i >= free) {
            i = free;
        }
        pro[(int)pros] += i;
        worker += i;
        free -= i;
        job.free = free;
        job.worker = worker;
    }

    public void WorkerMin(int i) {
        free = job.free;
        worker = job.worker;
        if (i >= pro[(int)pros]) {
            i = pro[(int)pros];
        }
        pro[(int)pros] -= i;
        worker -= i;
        free += i;
        job.free = free;
        job.worker = worker;
    }

    public void ReturnPro() {
        job.pros = (Job.Pros)(int)pros;
    }

    public void ShowAddPanel() {
        addPanel.Show();
        addPanel.SetPos(transform.localPosition.y - transform.GetComponent<RectTransform>().sizeDelta.y + 1);
    }

    public void HideAddPanel() {
        addPanel.Hide();
    }

    public void ShowMinPanel() {
        minPanel.Show();
        minPanel.SetPos(transform.localPosition.y - transform.GetComponent<RectTransform>().sizeDelta.y + 1);
    }

    public void HideMinPanel() {
        minPanel.Hide();
    }
}
