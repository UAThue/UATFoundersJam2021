using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipController : MonoBehaviour
{
    private Animator anim;
    public float timeBetweenSlips = 0.5f;
    public float stunbleChance = 0.1f;
    public float stumbleChanceReduction = 0.5f;
    private float nextAllowedSlipTime; 

    public void Start()
    {
        anim = GetComponentInParent<Animator>();
        nextAllowedSlipTime = Time.time;
    }


    public void OnTriggerEnter(Collider other)
    {
        SlipTrigger slipper = other.GetComponent<SlipTrigger>();
        if (slipper != null) {
            if (Time.time >= nextAllowedSlipTime)
            {
                if (Random.value < stunbleChance)
                {
                    Stumble();
                }
                else
                { 
                    Fall();
                }
                nextAllowedSlipTime = Time.time + timeBetweenSlips;

            }
            Destroy(other.gameObject);
        }
    }

    public void Stumble()
    {
        // Cut stumble chance in half each time they stumble
        stunbleChance *= (1 - stumbleChanceReduction);

        anim.SetTrigger("Stumble");
    }

    public void Fall()
    {
        anim.SetTrigger("Fall");
    }

}
