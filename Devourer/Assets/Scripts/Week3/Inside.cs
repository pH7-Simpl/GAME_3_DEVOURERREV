using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Inside : GoingOutside
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("gameManager").GetComponent<gameManager>().enabled = false;
            Animator animator = other.GetComponent<Animator>();
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
            rb2D.velocity = new Vector2(-8f, rb2D.velocity.y);
            other.transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetBool("isJumping", false);
            animator.SetFloat("speed", 1);
            LoadScene(2);
        }
    }
}
