using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float turnSpeed = 150f;

    private Rigidbody2D rb;
    private float moveInput;
    private float steerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = -Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // İleri / geri hareket
        rb.AddForce(transform.up * moveInput * acceleration);

        // Maksimum hız sınırı
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // Dönüş
        // float direction = Mathf.Sign(Vector2.Dot(rb.linearVelocity, rb.GetRelativeVector(Vector2.up)));
        // rb.rotation += steerInput * turnSpeed * Time.fixedDeltaTime * direction;
    }
}