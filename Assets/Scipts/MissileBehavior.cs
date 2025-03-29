using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float speed;
    public float turnSpeed = 0.0f;
    Vector2 targetPosition;
    private bool hasRotated = false;

    public Vector2 predictedTargetPosition;
    public float leadTime = 0.5f;
    public float spawnTime;

    public GameObject target;
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Time.time;
        initialPosition();
        body = GetComponent<Rigidbody2D>();

        // ChatGPT used
        Rigidbody2D targetRigidbody = target.GetComponent<Rigidbody2D>();
        if (targetRigidbody != null)
        {
            Vector2 targetVelocity = targetRigidbody.linearVelocity;
            predictedTargetPosition = (Vector2)target.transform.position + targetVelocity * leadTime;
        }
        RotateToCenter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 currentPosition = body.position;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, predictedTargetPosition, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);

        DestroyMissile();
    }

    // ChatGPT
    void RotateToCenter()
    {
        if (!hasRotated) // Rotate only once
        {
            // Get the position of the center of the screen (Camera's position)
            Vector2 screenCenter = Camera.main.transform.position;

            // Calculate the direction to the center of the screen
            Vector2 direction = (screenCenter - (Vector2)transform.position).normalized;

            // Calculate the angle to rotate towards the center
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Rotate the missile towards the target angle
            float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

            // Mark that the missile has rotated
            hasRotated = true;
        }
    }

    Vector2 getRandomPosition()
    {   
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        return new Vector2(randX, randY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(gameObject);
        }
    }

    public void initialPosition() 
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
    }

    public void setTarget(GameObject plane)
    {
        target = plane;
    }

    void DestroyMissile()
    {
        if (Time.time - spawnTime >= 5.0f)
        {
            Destroy(gameObject);
        }
    }
}
