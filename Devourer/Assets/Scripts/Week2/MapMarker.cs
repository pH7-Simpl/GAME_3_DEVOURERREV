using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static bool lookMap = false;
    public Camera MC;
    public Camera MapCamera;
    private void Awake()
    {
        player = GameObject.Find("Player");
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();
        MapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();
        MapCamera.transform.position = new Vector3(0f, 0f, -5f);
        MapCamera.transform.SetParent(transform);
        MC.enabled = true;
        lookMap = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(lookMap);
        }
    }

    private void ToggleMap(bool look)
    {
        lookMap = !lookMap;
        if (lookMap == false)
            Time.timeScale = 1f;
        else {
            Time.timeScale = 0f;
        }
        MC.enabled = look;
        MapCamera.enabled = !look;
    }
}
