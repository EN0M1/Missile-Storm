using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MissileTracking : MonoBehaviour
{
    public Transform target;

    public float speed;
    public float rotateSpeed;

    public GameObject explosionEffect;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            StartCoroutine(WaitAndLoadScene("GameOver"));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
        Destroy(gameObject);
    }

    /*
    void OnTriggerEnter2D()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        OnTriggerEnter2D();
        yield return new WaitForSeconds(OnTriggerEnter2D());
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
    */
}
