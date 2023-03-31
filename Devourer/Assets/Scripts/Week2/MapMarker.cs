using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static bool lookMap = false;
    public Camera MC; 
    public Camera MapCamera; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        MC = GameObject.Find("Player/Main Camera").GetComponent<Camera>();
        MC.transform.SetParent(transform);
        MapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();
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
        lookMap = !lookMap;
        MC.enabled = look;
        MapCamera.enabled = !look;
        Time.timeScale = look ? 1f : 0f;
    }
}
