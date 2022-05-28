using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Findparent() {
        Debug.Log(transform.parent.name);
        Debug.Log(transform.parent.parent.name);
    }
}
