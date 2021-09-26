using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        SlipTrigger slipper = other.GetComponent<SlipTrigger>();
        if (slipper != null) {
            anim.SetTrigger("Swat");
            anim.SetFloat("Random", Random.value);
            anim.SetLayerWeight(1, 1.0f);
            Destroy(other.gameObject);
            GameManager.instance.PlayRandomSoundFromList(GameManager.instance.stumbleSounds, transform.position);
        }
    }
}
