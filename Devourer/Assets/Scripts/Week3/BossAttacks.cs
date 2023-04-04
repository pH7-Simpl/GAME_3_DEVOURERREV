using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    private float duration = 0f;
    Random rdm = new Random();

    // Update is called once per frame
    void Update()
    {
        AtkStart(rdm.Next(4));
    }
    private void AtkStart(int random){
        switch (random){
            case 0:
            break;
            case 1:
            break;
            case 2:
            break;
            case 3:
            break;
        }
    }
}
