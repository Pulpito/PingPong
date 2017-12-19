using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iksolver : MonoBehaviour {

	// Array to hold all the joints
	// index 0 - root
	// index END - End Effector
	public GameObject[] joints;
	// The target for the IK system
	public GameObject targ;
	// Array of angles to rotate by (for each joint)
	public float[] theta;

	// The sine component for each joint
	[SerializeField]
	private float[] sin;
	// The cosine component for each joint
	[SerializeField]
	private float[] cos; 

	// To check if the target is reached at any point
	public bool done = false;
	// To store the position of the target
	private Vector3 tpos;

	// Max number of tries before the system gives up (Maybe 10 is too high?)
	[SerializeField]
	private int Mtries = 10;
	// The number of tries the system is at now
	[SerializeField]
	private int tries = 0;
	
	// the range within which the target will be assumed to be reached
	private float epsilon = 0.1f;


	// Initializing the variables
	void Start () {
		theta = new float[joints.Length];
		sin = new float[joints.Length];
		cos = new float[joints.Length];
		tpos = targ.transform.position;
	}
	
	// Running the solver - all the joints are iterated through once every frame
	void Update () {
		// if the target hasn't been reached
		if (!done)
		{	
			// if the Max number of tries hasn't been reached
			if (tries <= Mtries)
			{
                // starting from the second last joint (the last being the end effector)
                // going back up to the root
                for (int i = joints.Length - 2; i >= 0; i--)
                {
                    // The vector from the ith joint to the end effector
                    Vector3 r1 = joints[joints.Length - 1].transform.position - joints[i].transform.position;
                    // The vector from the ith joint to the target
                    Vector3 r2 = tpos - joints[i].transform.position;

                    // to avoid dividing by tiny numbers
                    if (r1.magnitude * r2.magnitude <= 0.001f)
                    {
                        // cos ? sin? 
                        sin[i] = 0.0f;
                        cos[i] = 1.0f;
                    }
                    else
                    {
                        // find the components using dot and cross product
                        sin[i] = Vector3.Cross(r1, r2).magnitude / r1.magnitude * r2.magnitude;
                        cos[i] = Vector3.Dot(r1, r2) / (r1.magnitude * r2.magnitude);

                    }

                    // The axis of rotation 
                    Vector3 axis = Vector3.Cross(r1, r2);

                    // find the angle between r1 and r2 (and clamp values if needed avoid errors)
                    theta[i] = Mathf.Acos(cos[i]);

                    //theta[i] = Mathf.Atan2(sin[i], cos[i]);

                    //Optional. correct angles if needed, depending on angles invert angle if sin component is negative
                    if (sin[i] < 0)
                        theta[i] *= -1;

                    // obtain an angle value between -pi and pi, and then convert to degrees
                    theta[i] *= Mathf.Rad2Deg;

                    // rotate the ith joint along the axis by theta degrees in the world space.
                    Quaternion rot = Quaternion.AngleAxis(theta[i], axis);
                    joints[i].transform.rotation = rot * joints[i].transform.rotation;
                }

                // increment tries
                tries++;
			}
		}

        // find the difference in the positions of the end effector and the target
        Vector3 diff = tpos - joints[joints.Length - 1].transform.position;
		
		// if target is within reach (within epsilon) then the process is done
		if (diff.magnitude < epsilon)
		{
			done = true;
		}
		// if it isn't, then the process should be repeated
		else
		{
			done = false;
		}
		
		// the target has moved, reset tries to 0 and change tpos
		if(targ.transform.position!=tpos)
		{
			tries = 0;
			tpos = targ.transform.position;
		}
	}

    /*
	// function to convert an angle to its simplest form (between -pi to pi radians)
	double SimpleAngle(double theta)
	{
		theta = TODO
		return theta;
	}*/
}
