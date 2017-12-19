using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    [SerializeField]
    private Vector3 speed;

    [SerializeField]
    private Vector3 aceleration;

    private Vector3 gravity = new Vector3(0, -9.81f, 0);

    [SerializeField]
    private Vector3 windFriction;

    [SerializeField]
    private Vector3 windSpeed;

    [SerializeField]
    public Vector3 forceAplyied;

    private float  WIND_COEFICIENT = 0.02f;

    [SerializeField]
    private float mass;

    [SerializeField]
    public float radius;

    private void Start()
    {
        gravity *= mass;
    }

    void Update ()
    {
        windFriction = (-speed * WIND_COEFICIENT) * Time.deltaTime;

        aceleration = (gravity + windFriction + windSpeed + forceAplyied) * Time.deltaTime;

        speed += aceleration;

        this.transform.position += (speed * Time.deltaTime);
	}
}
