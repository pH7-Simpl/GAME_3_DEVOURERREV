using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [SerializeField] private GameObject lightningStrike;
    [SerializeField] private GameObject waterStrike;
    [SerializeField] private GameObject windStrike;
    [SerializeField] private GameObject fireStrike;
    // Start is called before the first frame update
    private float duration = 0f;
    private bool attacking = false;

    // Update is called once per frame
    void Update()
    {
        if(!attacking) {
            StartCoroutine(AttackSequence());
            attacking = true;
        }
    }

    private IEnumerator AttackSequence() {
        AtkStart(Random.Range(0, 4));
        yield return new WaitForSeconds(4f);
        attacking = false;
    }
    private void AtkStart(int choice){
        switch (choice){
            case 0:
                StartCoroutine(LightningStrike());
            break;
            case 1:
                StartCoroutine(WaterStrike());
            break;
            case 2:
                StartCoroutine(WindStrike());
            break;
            case 3:
                StartCoroutine(FireStrike());
            break;
        }
    }
    private IEnumerator LightningStrike() {
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator WaterStrike() {
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator WindStrike() {
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator FireStrike() {
        yield return new WaitForSeconds(1f);
    }
}
