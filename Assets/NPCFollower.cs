using UnityEngine;


public class NPCFollower : MonoBehaviour
{
    [SerializeField]
    Transform player, pet;

    [SerializeField]
    float delay = 0.1f;

    [SerializeField]
    float firstDist;

    public bool isForwarding = true;
    public bool havingSpecifiedZone = false;
    

    Vector2 player_pos, pet_pos;
    Vector2 distance;
    
    float dist;

    

    // Update is called once per frame
    void Update()
    {
        if (isForwarding)
        {
            pet_pos = pet.localPosition;
            player_pos = player.localPosition;
            dist = (player_pos - pet_pos).magnitude;
            Vector2 aim_Vector = player_pos - pet_pos;

            if (dist > firstDist)
            {
                distance = player_pos - (aim_Vector / aim_Vector.magnitude) * firstDist;
                pet.localPosition = Vector2.Lerp(pet_pos, distance, delay);
            }
            if (havingSpecifiedZone)
            {
                if (dist < firstDist)
                {
                    distance = (-aim_Vector / aim_Vector.magnitude) * firstDist;
                    pet.localPosition = Vector2.Lerp(pet_pos, distance, Mathf.SmoothStep(0f, 1f, delay));
                }
            }
        }
    }
}
