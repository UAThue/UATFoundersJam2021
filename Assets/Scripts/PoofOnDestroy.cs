using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofOnDestroy : MonoBehaviour
{
    public GameObject poof;
    public AudioClip sound;

    public void OnDestroy()
    {
        if (poof != null) {
            Instantiate(poof, transform.position, transform.rotation);
        }
        if (sound!= null) {
            AudioSource.PlayClipAtPoint(sound, transform.position);
        }
    }
}
