using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCam : MonoBehaviour
{
    [SerializeField] private GameObject endMark;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endMark.transform.position, Time.deltaTime);
    }
}
