using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", moveSpeed);
        if (Input.GetKeyDown(KeyCode.P)) {
            anim.SetTrigger("Fall");
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            anim.SetTrigger("Stumble");
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            anim.SetTrigger("Swat");
            anim.SetFloat("Random", Random.value);
            anim.SetLayerWeight(1, 1.0f);
        }

    }
}
