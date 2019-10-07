using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 interval = new Vector2(0.2f, 1f);
    public Vector2 forceMagnitude = new Vector2(500f, 1000f);

    public float nextMove = 0f;

    public Rigidbody2D rb;
    public Vector2 previousVelocity = Vector2.zero;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Time.fixedTime > nextMove)
        {
            float magnitude = Random.Range(forceMagnitude.x, forceMagnitude.y);
            float heading = Random.Range(0f, 360f);
            Vector2 dir = Quaternion.Euler(0, 0, heading) * Vector2.up;

            Debug.DrawRay(transform.position, dir, Color.yellow, 0.1f, false);

            Vector2 force = dir * magnitude * Time.fixedDeltaTime;
            rb.AddForce(force);

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1f);

            nextMove = Time.fixedTime + Random.Range(interval.x, interval.y);
        }

        previousVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 newVelocity = Vector2.Reflect(previousVelocity, contact.normal);
        rb.velocity = Vector2.ClampMagnitude(newVelocity, 1f);
    }
}
