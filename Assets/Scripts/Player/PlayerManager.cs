using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
public bool isAlive = true;
private Animator animator;
[SerializeField]
private GameObject gameOverCanvas;
private Collider2D col;

private void Awake(){
  animator = GetComponent<Animator>();
  col = GetComponent<Collider2D>();
}

  public void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag=="Bullet"){
        Debug.Log("Bullets detected");
        isAlive = false;
        gameOverCanvas.SetActive(true);
        animator.SetTrigger("staydead");
        col.enabled = false;
    }
  }

  public void OnRestart(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
