using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    float dirX, moveSpeed = 3f;
    bool moveRight = true;


	// Update is called once per frame
	void Update()
	{
		if (gameObject.name == "BottomPlatform(Danger)")
		{
			if (transform.position.x > 10f)
				moveRight = false;
			if (transform.position.x < -8f)
				moveRight = true;

			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
		}
		else
		{
			if (transform.position.x > 4f)
				moveRight = false;
			if (transform.position.x < -4f)
				moveRight = true;

			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

		}
	}
}
