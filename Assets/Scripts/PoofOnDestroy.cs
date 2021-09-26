using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofOnDestroy : MonoBehaviour
{
    public GameObject poof;
    public List<AudioClip> sounds;

    public void OnDestroy()
    {
        if (poof != null) {
            Instantiate(poof, transform.position, transform.rotation);
        }
        if (sounds!= null) {
            GameManager.instance.PlayRandomSoundFromList(sounds, transform.position);
        }
    }
}
