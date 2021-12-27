using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontal, vertical;
    public bool canRun = true;
    public bool hasStopped = false;
    Vector2 previousPosition;
    List<Vector2> prevPositions;
    Vector2 vectorFromBrain;

    [SerializeField] float speedModifier;
    Rigidbody2D rb;

    public void InitSelf()
    {
        prevPositions = new List<Vector2>();
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.localPosition;
    }

    
    void Update()
    {
        //if(canRun)
		{
            ApplyMovement(vectorFromBrain);
            if(prevPositions.Count < 5)
			{
                prevPositions.Add((Vector2)transform.localPosition);
            }

            else
			{
                foreach(Vector2 pos in prevPositions)
				{
                    if (pos != (Vector2)transform.localPosition)
					{
                        hasStopped = false;
                        prevPositions = new List<Vector2>();
                        break;
					}

                    canRun = false;
                    hasStopped = true;
				}
			}
            
		}
    }

    public bool HasStopped()
	{
		return ((Vector2)transform.localPosition == previousPosition);
	}

	public void SetMovementVector(Vector2 i_Vec)
	{
        vectorFromBrain = i_Vec;
	}

	public Vector2 GetInput()
	{
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
	}

    public void ApplyMovement(Vector2 i_Input)
	{
        Vector2 movement = i_Input * speedModifier;
		rb.MovePosition(transform.position + (Vector3) movement);
		rb.AddForce(movement);
    
    }
}