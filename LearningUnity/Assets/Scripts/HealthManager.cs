using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public static int playerHealth;

    public int maxPlayerHealth;

    Text text;

    private LevelManager levelManager;

    public bool isDead = false;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();

        text = GetComponent<Text>();

        playerHealth = maxPlayerHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (playerHealth <= 0)
        {
            levelManager.RespawnPlayer();
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        text.text = playerHealth.ToString();
	}

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    public bool getDead()
    {
        return isDead;
    }
}
