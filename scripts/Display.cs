using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public MyButton catPro;
    private int tick = 0;

    public Canvas canvas;
    public Building[] buildings = new Building[10];
    public PopuBuilding[] popuBuildings;
    public Resource[] resources = new Resource[10];
    public GameObject[] switchButtons = new GameObject[10];
    public RectTransform[] tabSwitch = new RectTransform[10];
    public MyButton[] myButtons = new MyButton[10];

    public Population population;

    public readonly int hightick = 5;
    public readonly int lowtick = 25;
    public readonly int upPerSecond = 10;
    public readonly int lagerSize = 16;

    private void Awake() {
        canvas = GetComponent<Canvas>();
        //Resource类对象获取
        for (int i = 0; i < transform.Find("resources").childCount - 1; i++) {
            resources[i] = transform.Find("resources").GetChild(i).GetComponent<Resource>();
        }

        //Building类对象获取
        for (int i = 0; i < transform.GetChild(1).Find("buildings").GetChild(0).childCount; i++) {
            buildings[i] = transform.GetChild(1).Find("buildings").GetChild(0).GetChild(i).GetComponent<Building>();
        }

        for (int i = 0; i < transform.Find("switch").childCount; i++) {
            switchButtons[i] = transform.Find("switch").GetChild(i).gameObject;
        }

        for (int i = 0; i < transform.Find("main").childCount; i++) {
            tabSwitch[i] = transform.Find("main").GetChild(i).GetComponent<RectTransform>();
        }

        for (int i = 0; i < transform.GetChild(1).Find("buildings").GetChild(0).childCount; i++) {
            myButtons[i] = transform.GetChild(1).Find("buildings").GetChild(0).GetChild(i).GetComponent<MyButton>();
        }

        population = FindObjectOfType<Population>(true);
        popuBuildings = FindObjectsOfType<PopuBuilding>(true);
    }

    // Start is called before the first frame update
    void Start() {
        catPro = transform.GetChild(1).GetChild(0).Find("buttons").Find("精炼猫薄荷").GetComponent<MyButton>();
        resources[0].Show();
        tabSwitch[0].SetAsLastSibling();
        initialEffect();
    }

    private void FixedUpdate()
    {   
        if (tick >= lowtick) {
            if (resources[0].GetNum() < 100) {
                catPro.myDisEnable();
            }
            else {
                catPro.myOnEnable();
            }
            if (resources[0].GetNum() > 6 && resources[0].GetNum() < 20) {
                buildings[2].Show();
                resources[1].Show();
            }
            if (resources[1].GetNum() > 4 && resources[1].GetNum() < 20) {
                popuBuildings[0].Show();
                switchButtons[1].SetActive(true);
            }
            if (switchButtons[1].activeInHierarchy) {
                switchButtons[1].transform.GetChild(0).GetComponent<Text>().text = "小村庄 (" + population.GetNum() + ")";
            }
        } else {
            tick++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchTab(int i) {
        tabSwitch[i].SetAsLastSibling();
    }

    private void initialEffect() {
        myButtons[2].effect = string.Format("\n<size=16><color=black>效果：\n____________________________________</color></size>\n猫薄荷 产出：0.63/秒                        ");
    }
}

