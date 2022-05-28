using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    private readonly float speed = 0.75f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop() {
        animator.enabled = false;
    }

    public void Hide() {
        animator.enabled = false;
        gameObject.SetActive(false);
    }

    public void DeactiveBack() {
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
    }
}
