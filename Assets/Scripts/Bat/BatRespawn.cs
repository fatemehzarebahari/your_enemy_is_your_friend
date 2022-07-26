using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRespawn : MonoBehaviour
{
    
	public Transform batPrefab;
    public void respawn(Vector3 position){
        Debug.Log("in rewpawn");
        Transform Bat  = Instantiate(batPrefab,position,Quaternion.identity);
        Bat.SetParent(transform,false);

    }
}
