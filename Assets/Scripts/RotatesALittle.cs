using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatesALittle : MonoBehaviour
{
    public float minTimeBetweenTurns = 0.5f;
    public float maxTimeBetweenTurns = 3.5f;
    public float minTurnTime = 0.1f ;
    public float maxTurnTime = 0.5f;
    public float turnSpeed = 90.0f;
    private bool turnsRight;

    private float timeStartNextTurn;
    private float timeEndNextTurn;

 
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f) {
            turnsRight = true; }
        else {
            turnsRight = false; }

        CalculateNextTurnTimes();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;

        if (Time.time >= timeStartNextTurn) {
            if (Time.time <= timeEndNextTurn) {
                // Turn
                if (turnsRight) {
                    transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
                }
                else {
                    transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
                }
            } else {
                CalculateNextTurnTimes();
            }
        }     
    }
    

    public void CalculateNextTurnTimes ()
    {
        timeStartNextTurn = Time.time + Random.Range(minTimeBetweenTurns, maxTimeBetweenTurns);
        timeEndNextTurn = timeStartNextTurn + Random.Range(minTurnTime, maxTurnTime);
        // Flip direction
        turnsRight = !turnsRight;
    }

}
