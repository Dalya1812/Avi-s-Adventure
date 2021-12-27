using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
	bool started;
	float timeStarted;
	float timerDuration;
	Action action;

	private void Update()
	{
		if (started)
		tickTime();
	}

	private void tickTime()
	{
		if (timeStarted + (timerDuration * Time.timeScale) < Time.realtimeSinceStartup)
		{
			started = false;
			action.Invoke();
		}
	}

	public void Fire(float i_TimerDuration, Action i_Action)
	{
		started = true;
		this.timerDuration = i_TimerDuration;
		this.timeStarted = Time.realtimeSinceStartup;
		this.action = i_Action;
	}

	public void ResetWithCurrentParameters()
	{
		started = true;
		this.timeStarted = Time.realtimeSinceStartup;
	}

	public void Stop()
	{
		started = false;
	}

	public bool isRunning()
	{
		return started;
	}
}

//public void setParameters(float timerDuration, Action action)
//{
//	this.timerDuration = timerDuration;
//	this.action = action;
//}

//public void fire()
//{
//	started = true;
//	this.timeStarted = Time.realtimeSinceStartup;
//}
