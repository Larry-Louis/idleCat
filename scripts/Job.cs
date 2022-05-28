using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job : MonoBehaviour
{
    private Display display;
    private Population population;
    private int tick = 0;
    private int hightick;
    private int popu;
    private int Worker = 0;
    private int Free = 0;
    public enum Pros
    {
        Null,
        ·¥Ä¾¹¤,
    };
    public Pros pros;

    private static Job instance = null;
    private Job() { }
    public static Job Instance 
        { 
        get {
            if (instance = null) 
                { instance = new Job(); }
            return instance;
        } 
    }

    private void Awake() {
        instance = this;
        display = FindObjectOfType<Display>(true);
    }
    // Start is called before the first frame update
    void Start() {
        population = FindObjectOfType<Population>(true);
        Free = population.GetNum();
        hightick = display.hightick;
    }

    // Update is called once per frame
    void Update() {
        if (tick >= hightick) {
            popu = population.GetNum();
            Free = popu - Worker;
            transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "¿ÕÏÐµÄÐ¡Ã¨£º" + Free + " / " + popu;
        }
        else {
            tick++;
        }
    }

    public int worker {
        get { return Worker; }
        set { Worker = value; }
    }

    public int free { 
        get { return Free; } 
        set { Free = value; }
    }
}