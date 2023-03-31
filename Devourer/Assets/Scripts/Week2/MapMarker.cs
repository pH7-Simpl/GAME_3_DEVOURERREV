using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static bool lookMap = false;
    public Camera MainCamera; 
    public Camera MapCamera; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Map Camera").transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        transform.position = player.transform.position;
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(lookMap);
        }
    }

    private void ToggleMap(bool look)
    {
        lookMap = !lookMap;
        MainCamera.enabled = look;
        MapCamera.enabled = !look;
        Time.timeScale = look ? 1f : 0f;
    }
}
