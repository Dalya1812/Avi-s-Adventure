using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Population population;
	Timer timer;
	int currentIndex;
	[SerializeField] int populationSize;
	[SerializeField] GameObject player;

	private void Start()
	{
		timer = GetComponent<Timer>();
		currentIndex = 0;
		population = new Population(0.2f, populationSize, player);

		population[0].Run();
		//RunSingleGeneration();
		timer.Fire(5, RunSingleGeneration);

	}

	public bool isGenerationTested()
	{
		if (currentIndex < population.Size())
		{
			return false;
		}

		else
		{
			return true;
		}
	}
	public void RunSingleGeneration()
	{
		Player temp = population[currentIndex++];
		temp.Stop();
		print(population[currentIndex - 1].GetInstanceID() + " Stopped");
		if (!isGenerationTested())
		{
			population[currentIndex].Run();
			timer.Fire(3, RunSingleGeneration);
		}

		else
		{
			RunGeneticOperators();
		}

	}

	public void RunGeneticOperators()
	{
		population.CrossOver();
		population.Mutate();
		population.ResetPopulation();
		currentIndex = 0;
		RunSingleGeneration();
	}
}
