using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter() {
        transform.GetChild(0).GetComponent<Text>().color = Color.gray;
    }

    public void OnPointerExit() {
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }
}
