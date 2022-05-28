using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkDropdown : MonoBehaviour
{
    private Job job;
    private int free;
    private int worker;

    public Professioner pro;
    public int Changenum;
    // Start is called before the first frame update
    void Start()
    {
        job = FindObjectOfType<Job>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WorkerAdd() {
        if (Changenum == 0) {
            Changenum = job.free;
        }
        free = job.free;
        worker = job.worker;
        if (Changenum >= free) {
            Changenum = free;
        }
        Professioner.pro[(int)job.pros] += Changenum;
        worker += Changenum;
        free -= Changenum;
        job.free = free;
        job.worker = worker;
    }

    public void WorkerMin() {
        if (Changenum == 0) {
            Changenum = Professioner.pro[(int)job.pros];
        }
        free = job.free;
        worker = job.worker;
        if (Changenum >= Professioner.pro[(int)job.pros]) {
            Changenum = Professioner.pro[(int)job.pros];
        }
        Professioner.pro[(int)job.pros] -= Changenum;
        worker -= Changenum;
        free += Changenum;
        job.free = free;
        job.worker = worker;
    }
}
