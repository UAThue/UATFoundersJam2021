using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public float currentMoveSpeed;
    public float minMoveSpeed = 1.0f;
    public float maxMoveSpeed = 5.0f;

    public float moveSpeed;
    public float acceleration = 0.5f;
    private Animator anim;

    public int scoreValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.runners.Add(this);

        anim = GetComponent<Animator>();
        currentMoveSpeed = 0.0f;

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void OnDestroy()
    {
        GameManager.instance.runners.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;


        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, moveSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("Speed", currentMoveSpeed);        
    }


}
