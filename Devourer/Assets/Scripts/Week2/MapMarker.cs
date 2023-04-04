using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Camera MC;
    public Camera MapCamera;
    private gameManager gm;
    private void Awake()
{
    player = GameObject.FindGameObjectWithTag("Player");
    MC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    MapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();
    MapCamera.transform.SetParent(transform);
    MapCamera.enabled = false; 
    MC.enabled = true;
    gm = FindObjectOfType<gameManager>();
}

    private void Update()
    {
        if(player != null) {
            if(!gm.GetDoorOpening()) {
            transform.position = player.transform.position;
            MapCamera.transform.position = player.transform.position + new Vector3(0, 0, -5f);
        }
        }
        MapCamera.enabled = gm.IsSeeMap();
        MC.enabled = !gm.IsSeeMap();
    }
}
