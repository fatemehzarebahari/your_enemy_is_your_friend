using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollower : MonoBehaviour
{
    [SerializeField]
    Transform player, pet;

    float deltaX, deltaY, newDeltaX, newDeltaY;
    Vector2 player_pos;

    void Awake()
    {

        deltaX = pet.localPosition.x - player.localPosition.x;
        deltaY = pet.localPosition.x - player.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {

        newDeltaX = pet.localPosition.x - player.localPosition.x;
        newDeltaY = pet.localPosition.y - player.localPosition.y;
        if (newDeltaX != deltaX || newDeltaY != deltaY)
        {
            player_pos = player.localPosition;
            player_pos.x += deltaX;
            player_pos.y += deltaY;
            pet.localPosition = Vector2.Lerp(pet.localPosition, player_pos, Mathf.SmoothStep(0f, 1f, 0.4f));
        }
    }
}
