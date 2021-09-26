using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCurveWind : MonoBehaviour
{

    public float startTime;
    public float force = 10;
    public float pokeRange = 1.0f;
    private Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        // Load Components
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;


        rb.AddForceAtPosition(force * UpWiggle(0.25f) * Mathf.Sin(Time.time - startTime), transform.position + (Random.insideUnitSphere * pokeRange) );          


    }

    public Vector3 UpWiggle (float wiggleAmount)
    {
        return new Vector3(Random.value * wiggleAmount, 1.0f, Random.value * wiggleAmount);
    }



}
