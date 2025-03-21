using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public float minX = -9.09f;
    public float maxX = 9.11f;
    public float minY = -4.15f;
    public float maxY = 4.21f;
    public float speed;
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
        
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = target.transform.position;

        body = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = body.position;
    
        float distance = Vector2.Distance(currentPosition, targetPosition);
        if (distance > 0.1)
        {
            float difficulty = getDifficultyPercentage();
        
            speed = speed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed);
            body.MovePosition(newPosition);
        }
        else
        {
            targetPosition = getRandomPosition();
        }
    }

    Vector2 getRandomPosition()
    {   
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randX, randY);
        return v;
    }

    private float getDifficultyPercentage() 
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad);
        return difficulty;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            explode();
        }
        if (collision.gameObject.tag == "Plane")
        {
            // game over;
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

    public void explode()
    {

    }
}
