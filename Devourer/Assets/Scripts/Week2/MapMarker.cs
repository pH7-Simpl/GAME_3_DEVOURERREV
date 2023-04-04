using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static bool lookMap = false;
    public Camera MC;
    public Camera MapCamera;
    void Start()
    {
        player = GameObject.Find("Player");
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();
        MapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();
        MapCamera.transform.SetParent(transform);
        MC.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(lookMap);
        }
    }

    private void ToggleMap(bool look)
    {
        if (lookMap == false)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
        MC.enabled = look;
        MapCamera.enabled = !look;
        lookMap = !lookMap;
    }
}
