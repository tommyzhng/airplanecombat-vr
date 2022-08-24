using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadPlane : MonoBehaviour
{
    private int planeIndex;
    //Spawn Plane
    public GameObject[] planePrefabs;
    public Transform spawnLocation;

    //Move Camera
    public GameObject vrCam;
    public Vector3[] spawnOffset;

    

    // Start is called before the first frame update
    void Awake()
    {
        
    }
    private void Start()
    {
        planeIndex = PlayerPrefs.GetInt("selectedPlane");
        //Spawn Plane
        GameObject plane = planePrefabs[planeIndex];
        GameObject spawn = Instantiate(plane, spawnLocation.position, Quaternion.identity);
        spawn.name = "plane";
        //Move Camera
        vrCam.transform.parent = GameObject.Find("plane").transform;
        vrCam.transform.localPosition = spawnOffset[planeIndex];
    }

}
