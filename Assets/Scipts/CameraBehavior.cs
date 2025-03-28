using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    public void SetTarget(GameObject newTarget)
    {
        player = newTarget;
        offset = transform.position - player.transform.position;
    }
}
