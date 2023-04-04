using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMap : MonoBehaviour
{
    [SerializeField] private LayerMask markLayer;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (IsTouched())
        {
            Destroy(gameObject);
        }
    }
    public bool IsTouched()
    {
        return Physics2D.OverlapBox(transform.position, transform.GetComponent<Renderer>().bounds.size, 0f, markLayer);
    }
}
