using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckPoint;
    private Player player;
    private float gravitStore;
    public GameObject deathParticle;
    public GameObject respawnParticle;

    public int pointPenaltyOnDeath;

    public float respawnDelay;

    private CameraController camera;

    private HealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        camera = FindObjectOfType<CameraController>();
        healthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        if (healthManager.getDead())
        {
            Instantiate(deathParticle, player.transform.position, player.transform.rotation);
            player.enabled = false;
            player.GetComponent<Renderer>().enabled = false;

            camera.isFollowing = false;
            player.transform.position = currentCheckPoint.transform.position;
            healthManager.FullHealth();
            //gravitStore = player.GetComponent<Rigidbody2D>().gravityScale;
            //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ScoreManager.AddPoints(-pointPenaltyOnDeath);
            Debug.Log("Player Respawn");
            yield return new WaitForSeconds(respawnDelay);
            
            camera.isFollowing = true;
            Instantiate(respawnParticle, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
            //player.GetComponent<Rigidbody2D>().gravityScale = gravitStore;
            player.enabled = true;
            player.GetComponent<Renderer>().enabled = true;
        }
       
    }
}
