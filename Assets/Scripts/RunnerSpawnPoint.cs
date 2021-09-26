using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.runnerSpawners.Add(this);
    }

    void OnDestroy()
    {
        GameManager.instance.runnerSpawners.Remove(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward); 
    }
}
