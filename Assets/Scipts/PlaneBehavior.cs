using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlaneBehavior : MonoBehaviour
{
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float start;
    public float speed;
    public float turnSpeed;
    public Camera cam;
    public Rigidbody2D body;
    public float baseSpeed;
    public float dashSpeed;
    public float dashDuration;
    public bool dashing;
    public static float cooldownRate;
    public float endLastDash;
    public static float cooldown = 0.0f;

    public GameObject explosionEffect;

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
        Dash();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            StartCoroutine(WaitAndLoadScene("GameOver"));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            body.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
        Destroy(gameObject);
    }

    // KingP
    private void Dash()
    {
        if (dashing == true)
        {
            float currenttime = Time.time;
            float timeDashing = currenttime - start;
            if (timeDashing > dashDuration)
            {
                dashing = false;
                speed = baseSpeed;
                cooldown = cooldownRate;
            }
        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
            if (cooldown < 0.0)
            {
                cooldown = 0.0f;
            }
            if (cooldown == 0.0 && Input.GetMouseButtonDown(0))
            {
                dashing = true;
                speed = dashSpeed;
                start = Time.time;
            }
        }
    }
}
