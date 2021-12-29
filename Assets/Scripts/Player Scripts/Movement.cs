using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontal, vertical;

    [SerializeField] float speedModifier;
    Rigidbody2D rb;

    public void InitSelf()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Vector2 vectorMov = GetInput();
        ApplyMovement(vectorMov);
		
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