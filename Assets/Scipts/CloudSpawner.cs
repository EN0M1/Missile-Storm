using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    // Used ChatGPT for this script
    public GameObject[] cloudPrefabs;
    public float spawnDistance = 15.0f;
    public float spawnRate = 1.0f;
    public Vector2 spawnBounds = new Vector2(4.0f, 4.0f);
    public int maxClouds = 5;
    private Transform player;
    private float timer = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            if (GameObject.FindGameObjectsWithTag("Cloud").Length < maxClouds)
            {
                SpawnCloud();
            }
            timer = 0.0f;
        }
    }

    void SpawnCloud()
    {
        GameObject randomCloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
        Vector2 moveDirection = (Vector2)player.up; 
        Vector2 spawnPosition = (Vector2)player.position + moveDirection * spawnDistance;
        spawnPosition += new Vector2(Random.Range(-spawnBounds.x, spawnBounds.x), Random.Range(-spawnBounds.y, spawnBounds.y));
        GameObject newCloud = Instantiate(randomCloud, spawnPosition, Quaternion.Euler(0f, 0f, 90f));
        newCloud.tag = "Cloud";
    }

    void LateUpdate()
    {
        foreach (GameObject cloud in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            if (Vector2.Distance(cloud.transform.position, player.position) > spawnDistance * 2) 
            {
                Destroy(cloud);
            }
        }
    }
}
