using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRespawn : MonoBehaviour
{
    
	public Transform batPrefab;
    public void respawn(){
        Debug.Log("in rewpawn");
        Transform Bat  = Instantiate(batPrefab);
        Bat.SetParent(transform,false);

    }
}
