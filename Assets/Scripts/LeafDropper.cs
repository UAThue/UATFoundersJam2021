using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDropper : MonoBehaviour
{
    public GameObject leafPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject newLeaf = Instantiate(leafPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition + (Vector3.forward * 10)), Quaternion.identity ) as GameObject;

        }
    }
}
