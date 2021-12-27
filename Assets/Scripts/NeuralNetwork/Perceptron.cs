using System.Collections;
using System.Collections.Generic;
using static System.Math;
using UnityEngine;

public class Perceptron : System.ICloneable
{
	public float[] weights;
	float m_Fitness;

	public float Fitness 
	{
		get { return m_Fitness; }
		set { m_Fitness = value; }
	}

	public Perceptron()
	{
		initWeights();
	}


	private void initWeights()
	{
		weights = new float[2];
		for (int i = 0; i < weights.Length; i++)
		{
			weights[i] = Random.Range(-3f, 3f);
		}
	}

	public float AltGuess(Vector2 i_Position)
	{
		float sum = 0;
		float[] vector = Vector2Array(i_Position);
		for (int i = 0; i < weights.Length; i++)
		{
			sum += vector[i] * weights[i];
		}

		return (float)Tanh(sum);

	}


	public Vector2 Guess(Vector2 position)
	{
		//KeyCode answer = KeyCode.None;
		Vector2 answer = new Vector2();
		float[] positionArray = Vector2Array(position);

		float sum = 0;
		for (int i = 0; i < weights.Length; i++)
		{
			sum += weights[i] * positionArray[i];
		}

		float bias = 0.3f;
		float result = (float)Sin((double)sum);

		//LEFT
		if (result >= -1f && result < -0.5f)
		{
			answer = Vector2.left;
		}

		//RIGHT
		else if (result >= -0.5f && result < 0)
		{
			answer = Vector2.right;
		}

		//UP
		else if (result >= 0 && result < 0.5f)
		{
			answer = Vector2.up;
		}

		//DOWN
		else if (result >= 0.5f && result <= 1f)
		{
			answer = Vector2.down;
		}

		Debug.Log("Answer is: " + answer);

		return answer;
	}

	public void Train(Vector2 position)
	{

	}

	private float[] Vector2Array(Vector2 i_Vector)
	{
		return new float[] { i_Vector.x, i_Vector.y };
	}

	public Color Mutate(float i_MutationRate)
	{
		Color color = new Color(0, 0, 0);
		for(int i = 0; i < weights.Length; i++)
		{
			if (Random.Range(0, 1f) <= i_MutationRate)
			{
				weights[i] += Random.Range(-1f, 1f);
				color.r += Random.Range(-0.1f, 0.1f);
				color.g += Random.Range(-0.1f, 0.1f);
				color.b += Random.Range(-0.1f, 0.1f);
			}
		}
		return color;
	}

	//public int CompareTo(Perceptron other)
	//{
	//	return (int) (Mathf.Sign(this.Fitness - other.Fitness));
	//}

	public object Clone()
	{
		Perceptron p = new Perceptron();
		p.weights = (float[]) this.weights.Clone();
		p.Fitness = this.Fitness;
		return p;
	}

	public Perceptron CloneWithType()
	{
		return (Perceptron)Clone();
	}

	public void InitFromOther(float[] i_Other)
	{
		for(int i = 0; i < weights.Length; i++)
		{
			weights[i] = i_Other[i];
		}

	}
}
