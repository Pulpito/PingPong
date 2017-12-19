﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleConstraints : MonoBehaviour
{
    public bool active;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;

    void Start()
    {
    }

    void LateUpdate()
    {
        if (active)
        {

            Vector3 ToParent = (parent.position - transform.position).normalized;
            Vector3 ToChild = (child.position - transform.position).normalized;
            Vector3 axis = Vector3.Cross(ToParent, ToChild).normalized;

            float angle = Mathf.Acos(Vector3.Dot(ToParent, ToChild)) *Mathf.Rad2Deg;

            angle = Mathf.Clamp(angle, minAngle, maxAngle);

            Quaternion qrot = Quaternion.AngleAxis(angle, axis);

            //transform.rotation = qrot;
            //WRONG! we do not consdier the father's rotation

            //transform.rotation = qrot* parent.rotation; 
            //WRONG! qrot is calculated from global coordinated, and here we are using it in local coordinates


            transform.rotation = parent.rotation;
            transform.Rotate(axis, 180 + angle, Space.World);




        }
    }

    private float ComputeAngle(Vector3 ToParent, Vector3 ToChild)
    {
       
        return 0.0f;
    }
}
