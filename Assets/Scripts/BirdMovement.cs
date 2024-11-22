using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float verticalJumpSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip pointSound;
    [SerializeField] private AudioClip wingSound;

    private bool isJumping = false;
    private bool isFalling = false;
    private float birdRotation = 0f;
    public bool isAlive = true;

    private void Update()
    {
        if (!isAlive) return;
        if(GameManagement.Instance.CurrentGameState == GameState.InGame)
        {
            MoveBird();
            DecideBirdRotation();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Score":
                SoundManagement.Instance.PlaySound(pointSound, transform, 1f);
                ScoreManagement.Instance.IncreaseScore();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Pipe":
            case "Ground":
                SoundManagement.Instance.PlaySound(hitSound, transform, 1f);
                BirdDeath();
                break;
        }
    }

    private bool IsJumpInputReceived()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0);
    }

    private void MoveBird()
    {
        if (IsJumpInputReceived())
        {
            SoundManagement.Instance.PlaySound(wingSound, transform, 1f );
            rb.velocity = new Vector2(0, verticalJumpSpeed); // Sadece dikey hýz
            isJumping = true;
            isFalling = false;
        }
        else if (rb.velocity.y < 0)
        {
            isFalling = true;
            isJumping = false;
        }
    }

    private void DecideBirdRotation()
    {
        birdRotation = isJumping ? 35f : isFalling ? -35f : 0f;
        rb.rotation = Mathf.Lerp(rb.rotation, birdRotation, Time.deltaTime * rotationSpeed);
    }

    private void BirdDeath()
    {
        Time.timeScale = 0f;
        SoundManagement.Instance.PlaySound(dieSound, transform, 1f);
        isAlive = false;
        GameManagement.Instance.GameOver();
    }

    public void BirdReset()
    {
        isAlive = true;
        rb.transform.position = new Vector3(0, 1.125f, 0);
        rb.transform.rotation = Quaternion.identity;
    }
}
