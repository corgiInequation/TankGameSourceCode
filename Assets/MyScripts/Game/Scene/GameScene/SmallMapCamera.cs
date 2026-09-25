using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTank;
    public float height = 20;
    //Vector3的x, y, z不可以直接改变，只能一整个替换
    private Vector3 pos;

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerTank == null)
        {
            return;
        }
        pos.x = playerTank.position.x;
        pos.z = playerTank.position.z;
        pos.y = height;
       transform.position = pos;
    }
}
