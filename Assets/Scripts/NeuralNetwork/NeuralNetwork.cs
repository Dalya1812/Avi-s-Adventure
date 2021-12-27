using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    Perceptron horizontalM;
    Perceptron verticalM;
    
    public void InitSelf()
	{
        horizontalM = new Perceptron();
        verticalM = new Perceptron();
	}

    public void InitFromOther(Perceptron[] list)
	{
        horizontalM = (Perceptron) list[0].Clone();
        verticalM = (Perceptron) list[1].Clone();
    }

    public Vector2 GenerateOutput(Vector2 i_Input)
	{
        float horizontal = horizontalM.AltGuess(i_Input);
        float vertical = verticalM.AltGuess(i_Input);

        return new Vector2(horizontal, vertical);
	}

    public void Mutate(float i_MutationChance)
	{
        Color color = new Color();
        color += horizontalM.Mutate(i_MutationChance);
        color += verticalM.Mutate(i_MutationChance);

        GetComponent<SpriteRenderer>().color += color;
	}

    public Perceptron[] GetPerceptrons()
	{
        return new Perceptron[] { horizontalM, verticalM };
	}
}
