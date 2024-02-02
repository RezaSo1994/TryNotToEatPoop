using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NardeController : MonoBehaviour
{
    private Transform tr;
    private Animator _anim;
    public float Distance = 42;
    private void Awake()
    {
        tr = transform;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(tr.position.x);
        if(tr.position.x < Distance)
        {
            _anim.SetBool("Show", true);
        }
        else
        {
            _anim.SetBool("Show", false);
        }
    }
}
