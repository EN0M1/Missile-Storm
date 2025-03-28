using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float speed;
    public float turnSpeed;
    Vector2 targetPosition;

    public GameObject target;
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FacePlane();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            TrackPlane();
        }
        else
        {
            targetPosition = getRandomPosition();
        }
    }

    void FacePlane()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    void TrackPlane()
    {
        body.linearVelocity = transform.up * speed;
    }

    Vector2 getRandomPosition()
    {   
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        return new Vector2(randX, randY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag == "Missile")
        {
            explode();
        }
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        */
    }

    public void initialPosition() 
    {
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
    }

    public void setTarget(GameObject plane)
    {
        target = plane;
    }

    /*
    public void explode()
    {
        Destroy(gameObject);
    }
    */
}
