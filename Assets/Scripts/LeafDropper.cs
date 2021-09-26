using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDropper : MonoBehaviour
{
    public GameObject leafPrefab;
    public Transform targetDrop;
    public Vector3 dropOffset;
    public Vector3 targetPositionOffset;
    public LayerMask groundLayer;
    public float leafLifespawn = 5.0f;
    public List<Color> leafColors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;


        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundLayer)) {
            targetDrop.position = hit.point + targetPositionOffset;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            if (GameManager.instance.numLeaves >= 1)
            {
                GameManager.instance.numLeaves -= 1;
                GameObject newLeaf = Instantiate(leafPrefab, targetDrop.position + dropOffset, Quaternion.identity) as GameObject;
                Material leafMat = newLeaf.GetComponent<Renderer>().material;
                leafMat.color = leafColors[Random.Range(0, leafColors.Count)];
                GameManager.instance.PlayRandomSoundFromList(GameManager.instance.leafDropSounds, targetDrop.position);
            }
        }
    }
}
