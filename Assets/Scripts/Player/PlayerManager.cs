using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
public bool isAlive = true;
private Animator animator;
[SerializeField]
private GameObject gameOverCanvas;
private Collider2D col;
public event Action onDeath;

private void Awake(){
  animator = GetComponent<Animator>();
  col = GetComponent<Collider2D>();
}

  public void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy"){
        Debug.Log("Bullets detected");
        isAlive = false;
        gameOverCanvas.SetActive(true);
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("staydead");
        col.enabled = false;
        StartCoroutine(stopGame());
    }
  }

  public void OnRestart(){
        if(SceneManager.GetActiveScene().buildIndex != 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        else
        SceneManager.LoadScene(1);

        Time.timeScale = 1;
  }
  public void OnMenu()
  {
    SceneManager.LoadScene(0);
  }

  private IEnumerator stopGame(){
    yield return new WaitForSeconds(1f);
    Time.timeScale = 0;
  }
}
