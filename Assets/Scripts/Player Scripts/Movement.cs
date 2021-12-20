using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontal, vertical;
    public bool canRun;
    Vector2 vectorFromBrain;

    [SerializeField] float speedModifier;
    Perceptron Steering
	{
        get; set;
	}

    Rigidbody2D rb;

    public void InitSelf()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(canRun)
		{
            ApplyMovement(vectorFromBrain);
		}
    }

    public void SetMovementVector(Vector2 i_Vec)
	{
        if (vectorFromBrain != i_Vec)
        {
            print("Different!");
        }

        else print("Same!");
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

        Vector2 movement = i_Input * speedModifier * Time.deltaTime;
        print(i_Input);
        rb.MovePosition((Vector2)transform.position + movement);
	}
}
