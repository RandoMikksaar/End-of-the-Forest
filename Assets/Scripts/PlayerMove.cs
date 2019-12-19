//Unitys peab X ja  Z telje rotationi freezima rigid bodys

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	Vector3 targetPosition;
	Vector3 lookAtTarget;
	Vector3 m_YAxis;

	Rigidbody m_Rigidbody;
	Quaternion playerRot;

	float rotSpeed = 5;
	float speed = 5;

	bool moving = false;

	// Use this for initialization
	void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		//This locks the RigidBody so that it does not move or rotate in the Y axis (can be seen in Inspector).
		m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
		//Set up vector for moving the Rigidbody in the z axis
		m_YAxis = new Vector3(0, 0, 5);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			SetTargetPosition();
		}
		if(moving)
			Move();
	}

	void SetTargetPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			targetPosition = hit.point;
			//this.transform.LookAt(targetPosition);
			lookAtTarget = new Vector3(targetPosition.x
                - transform.position.x,
				transform.position.y,
				targetPosition.z - transform.position.z);
			playerRot = Quaternion.LookRotation(lookAtTarget);
			moving = true;
		}
	}

	void Move()
    {
		transform.rotation = Quaternion.Slerp(transform.rotation,
					playerRot,
					rotSpeed * Time.deltaTime);

		transform.position = Vector3.MoveTowards(transform.position,
					targetPosition,
					speed * Time.deltaTime);

		if (transform.position == targetPosition)
			moving = false;
	}
}