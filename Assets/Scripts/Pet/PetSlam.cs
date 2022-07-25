using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetSlam : MonoBehaviour
{
	[SerializeField]
	public Transform PetPos;

	[SerializeField]
	AudioSource releaseAudio;
	bool releaseSound = true;

	[SerializeField]
	AudioSource FillingreleaseAudio;

	private Vector2 Center;

	[SerializeField]
	public float explosionStrength, range = 2, duration = 0.5f;

	[SerializeField]
	private AudioSource explode;

	private Animator animator;
	private Camera camera;
	private float preSize = 0f;
	private bool chargedUp = false;
	private GameObject camHolder;
	[SerializeField]
	private Image image;
	[SerializeField]
	private GameObject textRelease;
	[SerializeField]
	Slider manaBar;
	[SerializeField]
	float chargeManaBarDuration = 20f;

	public bool manaBarFilled = false;

    private void Start()
    {
			animator = GetComponent<Animator>();
			camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			camHolder = GameObject.FindGameObjectWithTag("CameraHolder");
			preSize = camera.orthographicSize;
			manaBar.value = 100;
			manaBarFilled = true;
		}

    void Update() {
		if (releaseSound && Input.GetKeyDown(KeyCode.LeftShift))
		{
			FillingreleaseAudio.volume = 100;
			FillingreleaseAudio.Play();
			releaseSound = false;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			Time.timeScale -= 1f * Time.deltaTime;
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0.35f, 1f);
			Time.fixedDeltaTime = Time.timeScale * .02f;
			if (Time.timeScale < 0.5f)
				chargedUp = true;
			camera.orthographicSize -= 4.5f * Time.deltaTime;
			camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, preSize -1.5f, preSize);
			image.rectTransform.localScale = new Vector3(image.rectTransform.localScale.x + 20f * Time.deltaTime, image.rectTransform.localScale.y, image.rectTransform.localScale.z);
			if (image.rectTransform.localScale.x >= 7) 
			{
				image.rectTransform.localScale = new Vector3(7, image.rectTransform.localScale.y, image.rectTransform.localScale.z);
				textRelease.SetActive(true);
			}
		}
		else
		{
			FillingreleaseAudio.volume = 0;
			releaseSound = true;

			textRelease.SetActive(false);
			camera.orthographicSize = preSize;
			Time.timeScale = 1f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			image.rectTransform.localScale = new Vector3(image.rectTransform.localScale.x - 50f * Time.deltaTime, image.rectTransform.localScale.y, image.rectTransform.localScale.z);

			if (image.rectTransform.localScale.x <= 0)
				image.rectTransform.localScale = new Vector3(0, image.rectTransform.localScale.y, image.rectTransform.localScale.z);
		}

		if (manaBarFilled && chargedUp && Input.GetKeyUp(KeyCode.LeftShift))
		{
			animator.SetTrigger("Attack");
			chargedUp = false;
			ManaBarSetZeroValue();
		}
	}

	public void Slam(){
		releaseAudio.Play();
		explode.Play();
		StartCoroutine(Shake(0.1f, 0.1f));
		var colliders = Physics2D.OverlapCircleAll(transform.position, range);

		if (colliders.Length > 0)
		{            
			for(int i =0 ; i< colliders.Length;i++){

				if(colliders[i].gameObject.tag == "Enemy"){

					if(!colliders[i].GetComponent<EnemyManager>().isAiming)
						{
						StartCoroutine(colliders[i].GetComponent<NPCFollower>().StopFollowingFor(duration));
						Transform t = colliders[i].transform;
						Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();
						Vector2 ForceVec = (t.position - transform.position).normalized * explosionStrength;
						rb.velocity = ForceVec;
						StartCoroutine(rb.gameObject.GetComponent<EnemyManager>().Fall(duration));
						}
				}
			}
		}
		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
		camera.orthographicSize = preSize;
	}

	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 origin = camHolder.transform.localPosition;

		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			camHolder.transform.localPosition = new Vector3(x, y, origin.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
		camHolder.transform.localPosition = origin;
	}

	private void ManaBarSetZeroValue()
    {

		manaBar.value = 0;
		manaBarFilled = false;
		StartCoroutine(ChargeManaBar());

	}
	private IEnumerator ChargeManaBar()
	{

		float t = 0;
		while (t < chargeManaBarDuration)
		{
			t += Time.deltaTime ;

			manaBar.value = (t/chargeManaBarDuration)*100 ;

			yield return null;
            
		}

		manaBarFilled = true;

	}
}
