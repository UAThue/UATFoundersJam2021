using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextDecayer : MonoBehaviour
{
    private List<Text> textObjects;
    private float decayStartTime;
    public float percentBeforeDecay = 0.9f;
    public float lifespan;


    // Start is called before the first frame update
    void Start()
    {
        textObjects = new List<Text>(GetComponentsInChildren<Text>());

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

            foreach (Text textbox in textObjects) {
                Color newColor = textbox.color;
                newColor.a = Mathf.Lerp(1.0f, 0.0f, (Time.time - decayStartTime) / decayTime);
                textbox.color = newColor;
            }
        }
    }
}
