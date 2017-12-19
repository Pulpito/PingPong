using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public GameObject Ball;

    private bool collision = false;

    public float distance;

    public bool x, y, z;

    public Vector3 axis;

    void Update ()
    {
        if(z)
        {
            axis = CrossProduct(transform.up, -transform.right);
        }

        if(!collision)
        {
            

        }

        else
        {
            
        }
	}

    private Vector3 CrossProduct(Vector3 vectorUp, Vector3 vectorRight)
    {
        Vector3 normal = new Vector3 (vectorUp.y * vectorRight.z - vectorUp.z * vectorRight.y, -1 * (vectorUp.x * vectorRight.z - vectorUp.z * vectorRight.x), vectorUp.y * vectorRight.x - vectorUp.x * vectorRight.y);
        return normal;
    }
}
