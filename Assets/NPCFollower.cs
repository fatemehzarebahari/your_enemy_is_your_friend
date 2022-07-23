using UnityEngine;


public class NPCFollower : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float speed = 3f;

    [SerializeField]
    float firstDist;

    Rigidbody2D rb;
    public bool isForwarding = true;

    Vector2 player_pos, pet_pos;
    
    float dist;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (isForwarding)
        {
            pet_pos = rb.position;
            player_pos = player.position;
            dist = (player_pos - pet_pos).magnitude;

            if (dist > firstDist)
            {
                Vector2 aim_Vector = player_pos - pet_pos;
                rb.velocity = aim_Vector * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

        }
    }
}
