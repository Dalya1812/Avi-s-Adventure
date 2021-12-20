using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
	Population population;
	Timer timer;
	int currentIndex;
	[SerializeField] GameObject player;

	private void Start()
	{
		timer = GetComponent<Timer>();
		currentIndex = 0;
		population = new Population(0.1f, 10, player);

		RunSingle();
		//timer.Fire(1, RunSingle);

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
	public void RunSingle()
	{
		Player temp = population[currentIndex++];
		temp.Stop();
		print(population[currentIndex - 1].GetInstanceID() + " Stopped");
		if (!isGenerationTested())
		{
			population[currentIndex].Run();
			timer.Fire(5, RunSingle);
		}
	}
}
