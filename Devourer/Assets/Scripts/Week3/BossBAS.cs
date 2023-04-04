using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaS : MonoBehaviour
{
    [SerializeField] private GameObject lightningStrike;
    [SerializeField] private GameObject waterStrike;
    [SerializeField] private GameObject windStrike;
    [SerializeField] private GameObject fireStrike;
    // Start is called before the first frame update
    private bool timeToAttack = false;
    private bool attacking = false;

    // Update is called once per frame
    private void Awake() {
        EnterRoomAnimation();
    }
    void Update()
    {
        if(timeToAttack && !attacking) {
            StartCoroutine(AttackSequence());
            attacking = true;
        }
    }

    private IEnumerator AttackSequence() {
        AttackStart(Random.Range(0, 4));
        yield return new WaitForSeconds(4f + Random.Range(0f, 4f));
        attacking = false;
    }
    private IEnumerator EnterRoomAnimation() {
        //play player enter room here
        yield return null;
        //set time to attack here
        timeToAttack = true;
    }
    private void AttackStart(int choice){
        switch (choice){
            case 0:
                LightningStrikeOnce();
            break;
            case 1:
                WaterStrikeOnce();
            break;
            case 2:
                WindStrikeOnce();
            break;
            case 3:
                FireStrikeOnce();
            break;
        }
    }
    //atack has to be under 4 seconds
    private void LightningStrikeOnce() {
        StartCoroutine(LightningStrike());
    }
    private IEnumerator LightningStrike() {
        Debug.Log("Lightning Strike");
        yield return new WaitForSeconds(1f);
        Debug.Log("Lightning Strike Finished");
    }
    private void WaterStrikeOnce() {
        StartCoroutine(WaterStrike());
    }
    private IEnumerator WaterStrike() {
        Debug.Log("Water Strike");
        yield return new WaitForSeconds(1f);
        Debug.Log("Water Strike Finished");
    }
    private void WindStrikeOnce() {
        StartCoroutine(WindStrike());
    }
    private IEnumerator WindStrike() {
        Debug.Log("Wind Strike");
        yield return new WaitForSeconds(1f);
        Debug.Log("Wind Strike Finished");
    }
    private void FireStrikeOnce() {
        StartCoroutine(FireStrike());
    }
    private IEnumerator FireStrike() {
        Debug.Log("Fire Strike");
        yield return new WaitForSeconds(1f);
        Debug.Log("Fire Strike Finished");
    }
}
