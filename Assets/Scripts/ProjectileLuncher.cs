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
    bool locked = false;
    bool shooting = false;

    Transform player;
    LineRenderer line;

    Vector2 target_position;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        line = Instantiate(linePrefab);
    }
    void Update()
    {
        if (aim && !locked)
        {
            aim_line();
            line.enabled = true;
        }
        else if (!aim)
            line.enabled = false;

        if (shooting)
        {
            shoot_bullet();
            shooting = false;
        }

    }
    void aim_line()
    {
       target_position = player.position + (player.position - shoothingStartPosition.position) * aimingLength;
        Vector3[] position = new Vector3[3] { shoothingStartPosition.position, target_position, new Vector3(0, 0, 0) };
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.SetPositions(position);
    }
    void shoot_bullet()
    {
        Vector2 aim = target_position - (Vector2)shoothingStartPosition.position;
        GameObject bullet_Obj = Instantiate(bulletPrefab, shoothingStartPosition.position, new Quaternion());
        bullet_Obj.GetComponent<bullet>().shoot(aim);
    
    }

    public void aiming()
    {
        aim = true;
    }

    public void setLocked(){
        locked = true;
    }
    public void stopAiming()
    {
        aim = false;
        locked = false;
    }
    public void shoot()
    {
        shooting = true;
    }
}
