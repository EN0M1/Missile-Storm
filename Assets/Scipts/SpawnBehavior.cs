using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public Planes planesDB;
    public GameObject[] missileVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 5.0f;
    public float minSpawn;
    public float maxSpawn;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPlane();
        spawnMissile();
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float timeElapsed = currentTime - startTime;
        if (timeElapsed > spawnRatio)
        {
            spawnMissile();
        }
    }

    void spawnMissile()
    {
        int numVariants = missileVariants.Length;
        if (numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(missileVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            MissileBehavior missileBehavior = newObject.GetComponent<MissileBehavior>();
            missileBehavior.initialPosition();
        }

        spawnRatio = Random.Range(minSpawn, maxSpawn);
    }

    void spawnPlane()
    {
        targetObject = Instantiate(planesDB.getPlane(PlaneManager.selection).prefab,
            new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        CameraBehavior cameraScript = Camera.main.GetComponent<CameraBehavior>();
        cameraScript.SetTarget(targetObject);

        updateMissileTargets(targetObject);
    }

    // ChatGPT used
    void updateMissileTargets(GameObject newTarget)
    {
        MissileBehavior[] missiles = FindObjectsByType<MissileBehavior>(FindObjectsSortMode.None);
        foreach (MissileBehavior missile in missiles)
        {
            missile.setTarget(newTarget);
        }
    }
}