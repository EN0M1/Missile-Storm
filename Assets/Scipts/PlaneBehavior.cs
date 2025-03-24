using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlaneBehavior : MonoBehaviour
{
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float speed;
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
        FaceMouse();
    }

    // taken from KingP movement script
    void FixedUpdate()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }

    // taken from youtube: Unity C# - How to face the mouse position in 2D
    // link: https://www.youtube.com/watch?v=_XdqA3xbP2A&list=LL&index=1&t=90s
    void FaceMouse()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
