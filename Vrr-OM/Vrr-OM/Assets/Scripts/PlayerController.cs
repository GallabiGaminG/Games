using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration = 12f;
    public float maxSpeed = 18f;
    public float turnSpeed = 180f;

    [Header("Handling")]
    [Range(0f, 1f)] public float driftFactor = 0.1f;   // 0: hiç drift yok, 1: çok yan kayar
    public float traction = 4f;                        // hız vektörünü öne doğru toplar

    private Rigidbody2D rb;
    private float throttle;    // W/S
    private float steer;       // A/D

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 2f;
        rb.angularDamping = 2f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector2.zero;
        rb.linearVelocity = Vector2.zero;        // 🚫 Başlangıçta itme olmasın
        rb.angularVelocity = 0f;           // 🚫 Dönme hızı sıfırla
    }

    void Update()
    {
        // İstersen GetAxis yerine GetAxisRaw kullan (daha keskin tepki)
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        // Yön ters gelirse tek satırda düzelt: steer = -Input.GetAxis("Horizontal");
        Debug.Log("Gaz/Fren"+throttle);
        Debug.Log("Yön"+steer);
    }

    void FixedUpdate()
    {
        // İleri/geri itme kuvveti
        rb.AddForce((Vector2)transform.up * throttle * acceleration);

        // Hız limiti
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // Yan kayma azaltma (drift kontrol)
        //Vector2 forward = transform.up;
        //Vector2 right = transform.right;

        //float fwd = Vector2.Dot(rb.linearVelocity, forward);
        //float side = Vector2.Dot(rb.linearVelocity, right);

        // side bileşenini azalt (driftFactor ile)
        //rb.linearVelocity = forward * fwd + right * driftFactor; //* side

        // Direksiyon: hızla orantılı olsun (yerinde dönmesin)
        //float speedPercent = Mathf.Clamp01(rb.linearVelocity.magnitude / (maxSpeed * 0.5f));
        //float rotate = -steer * turnSpeed * speedPercent * Time.fixedDeltaTime; // yön ters ise -'yı kaldır
        //rb.MoveRotation(rb.rotation + rotate);

        // Ekstra çekiş: hız vektörünü ileri doğru hizalar
        //rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, forward * fwd, traction * Time.fixedDeltaTime);
    }
}