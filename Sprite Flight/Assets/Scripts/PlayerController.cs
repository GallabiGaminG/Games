using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    private float score = 0f;
    public float scoreMultiplier = 10f;

    public GameObject boosterFlame;
    public UIDocument uiDocument;
    public GameObject explosionEffect;

    private Label scoreText;
    private Button restartButton;
    private Label highScoreText;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boosterFlame.SetActive(false);
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;

        highScoreText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;

        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdateScore();
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        //score = elapsedTime * scoreMultiplier;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        //Debug.Log("Elapsed Time : " + elapsedTime);
        Debug.Log("Score: " + score);

        scoreText.text = "Score: " + score;
    }
    void MovePlayer()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
        if (Mouse.current.leftButton.isPressed)
        {
            // --- Calculate mouse direction ---
            //Vector3 mousePos = Mouse.current.position.value;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            //Debug.Log("Mouse was pressed");
            //Debug.Log("Mouse position: " + mousePos);

            // --- Move player in direction of mouse ---
            //Vector2 direction = mousePos - transform.position;
            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;

            rb.AddForce(direction * thrustForce);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
            PlayerPrefs.Save();
        }

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
