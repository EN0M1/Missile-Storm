using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlaneBehavior : MonoBehaviour
{
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float speed;
    public AudioSource[] audioSources;
    public Camera cam;
    public Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = new Vector2(mousePosG.x, mousePosG.y);
        body.MovePosition(Vector2.MoveTowards(body.position, targetPosition, speed * Time.fixedDeltaTime));
    }
}
