using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
	List<Player> pop;
	[SerializeField] float m_MutationRate;
	[SerializeField] int m_PopulationSize;
	GameObject player;

	public Population(float i_MutationRate, int i_PopulationSize, GameObject i_Player)
	{
		m_MutationRate = i_MutationRate;
		m_PopulationSize = i_PopulationSize;
		player = i_Player;

		initPopulation();
	}

	private void initPopulation()
	{
		pop = new List<Player>();
		for (int i = 0; i < m_PopulationSize; i++)
		{
			Player p = GameObject.Instantiate(player).GetComponent<Player>();
			p.InitSelf();
			pop.Add(p);
		}
	}

	public Player this[int i]
	{
		get { return pop[i]; }
		set { pop[i] = value; }
	}

	public int Size()
	{
		return pop.Count;
	}




	public void Mutate()
	{
		foreach(Player p in pop)
		{
			p.Mutate(m_MutationRate);
		}
	}

	public void CrossOver()
	{
		List<Player> temp = new List<Player>();
		pop.Sort();
		for(int i = 0; i < m_PopulationSize/4; i++)
		{
			temp.Add(pop[pop.Count -  1 - i]);
		}

		int currentSize = temp.Count;
		for(int i = 0; i < currentSize; i++)
		{
			for(int j = 0; j < 3; j++)
			{
				temp.Add(temp[i]);
			}
		}

		pop = temp;
	}



}
