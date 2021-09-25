using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipController : MonoBehaviour
{
    private Animator anim;

    public void Start()
    {
        anim = GetComponentInParent<Animator>();
    }


    public void OnTriggerEnter(Collider other)
    {
        SlipTrigger slipper = other.GetComponent<SlipTrigger>();
        if (slipper != null) {
            Fall();
            Destroy(other.gameObject);
        }
    }

    public void Stumble()
    {
        anim.SetTrigger("Stumble");
    }

    public void Fall()
    {
        anim.SetTrigger("Fall");
    }

}
