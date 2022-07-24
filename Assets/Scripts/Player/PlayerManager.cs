using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
public bool isAlive = true;
private Animator animator;
[SerializeField]
private GameObject gameOverCanvas;

private void Awake(){
    animator = GetComponent<Animator>();

}

  public void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag=="Bullet"){
        Debug.Log("Bullets detected");
        isAlive = false;
        gameOverCanvas.SetActive(true);
        // animator.SetTrigger("die");
        animator.SetTrigger("staydead");
    }
  }

  public void OnRestart(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
