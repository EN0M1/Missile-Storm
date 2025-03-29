using UnityEngine;
using System.Collections.Generic;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject[] missileVariants;
    public GameObject targetObject;
    public CameraBehavior cameraBehavior;
    public Camera mainCamera;
    public float lastSpawnTime;
    public float spawnCooldown;
    public float spawnDistance = 7.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnCooldown)
        {
            spawnMissile();
            lastSpawnTime = Time.time;
        }
    }

    void spawnMissile()
    {
        if (missileVariants.Length == 0 || cameraBehavior == null || targetObject == null) return;

        int selection = Random.Range(0, missileVariants.Length);
        Vector3 planePosition = targetObject.transform.position;
        Vector3 planeForward = targetObject.transform.up;

        Vector3 spawnPos = planePosition + planeForward * spawnDistance;

        GameObject newMissile = Instantiate(missileVariants[selection], spawnPos, Quaternion.identity);

        MissileBehavior missileBehavior = newMissile.GetComponent<MissileBehavior>();

        missileBehavior.setTarget(targetObject);
    }
}