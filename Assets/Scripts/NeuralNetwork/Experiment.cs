using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Experiment : MonoBehaviour
{
    [SerializeField] GameObject player;
    float lastUpdateTime;
    Vector2 lastGuess;
    private Perceptron brain;
    // Start is called before the first frame update
    void Start()
    {
        lastUpdateTime = Time.realtimeSinceStartup;
        brain = new Perceptron();
        lastGuess = brain.Guess(player.transform.position);
    }

	private void Update()
	{
        
        if (Time.realtimeSinceStartup > lastUpdateTime + 1)
		{
            lastGuess = brain.Guess(player.transform.position);
            lastUpdateTime = Time.realtimeSinceStartup;
		}

		//player.GetComponent<Movement>().ApplyMovement(lastGuess);

	}
	public void launchGuess()
	{
        Vector2 guess =brain.Guess(player.transform.position);
        print("Current Guess" + guess);
        //for(int i = 0; i < 100; i++)
		{
            //player.GetComponent<Movement>().ApplyMovement(guess);
        }

    }

	public void ResetGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public KeyCode[] keys()
	{
        return new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
	}

    public void SpeedUp()
	{
        Time.timeScale += 1;
	}

    public void SlowDown()
	{
        Time.timeScale -= 1;
	}

}
