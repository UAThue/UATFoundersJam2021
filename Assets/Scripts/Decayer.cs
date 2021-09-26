using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decayer : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private float decayStartTime;
    public float percentBeforeDecay = 0.9f;
    public float lifespan;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        decayStartTime = Time.time + (percentBeforeDecay * lifespan);


        Destroy(gameObject, lifespan);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;

        if (Time.time >= decayStartTime)
        {
            float decayTime = (1 - percentBeforeDecay) * lifespan;
            Color newColor = material.color;
            newColor.a = Mathf.Lerp(1.0f, 0.0f, (Time.time - decayStartTime) / decayTime);
            renderer.material.color = newColor;
        }
    }
}
