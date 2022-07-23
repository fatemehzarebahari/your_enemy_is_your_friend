using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLuncher : MonoBehaviour
{

    [SerializeField]
    LineRenderer linePrefab;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform shoothingStartPosition;

    [SerializeField]
    float aimingLength = 1;


    bool aim = false;
    bool shooting = false;

    Transform player;
    LineRenderer line;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        line = Instantiate(linePrefab);
    }
    void Update()
    {
        if (aim)
        {
            aim_line();
            line.enabled = true;
        }
        else
            line.enabled = false;

        if (shooting)
        {
            shoot_bullet();
            shooting = false;
        }
        

    }
    void aim_line()
    {
        Vector2 target_position = player.position + (player.position - transform.position) * aimingLength;
        Vector3[] position = new Vector3[3] { transform.position, target_position, new Vector3(0, 0, 0) };
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.SetPositions(position);
    }
    void shoot_bullet()
    {
        Vector2 aim = player.position - shoothingStartPosition.position;
        GameObject bullet_Obj = Instantiate(bulletPrefab, shoothingStartPosition.position, new Quaternion());
        bullet_Obj.GetComponent<bullet>().shoot(aim);
    
    }

    public void aiming()
    {
        aim = true;
    }
    public void stopAiming()
    {
        aim = false;
    }
    public void shoot()
    {
        shooting = true;
    }
}
