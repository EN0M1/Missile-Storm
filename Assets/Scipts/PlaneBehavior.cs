using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlaneBehavior : MonoBehaviour
{
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float speed;
    public float turnSpeed;
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
        RotateTowardsMouse();
    }

    // taken from KingP movement script
    void FixedUpdate()
    {
        //mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        //newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        //body.MovePosition(newPosition);

        MoveForward();
    }

    // taken from youtube: Unity C# - How to face the mouse position in 2D
    // link: https://www.youtube.com/watch?v=_XdqA3xbP2A&list=LL&index=1&t=90s
    
    /*
    void FaceMouse()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
    */

    // Used ChatGPT
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    void MoveForward()
    {
        body.linearVelocity = transform.up * speed;
    }
}
