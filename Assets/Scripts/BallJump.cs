using UnityEngine;

public class BallJump : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D ballRigidbody;
    [SerializeField]
    private float force = 10;
    [SerializeField]
    private AudioSource jumpSource;

    public Vector2 Position => ballRigidbody.position;
    public SpriteRenderer BallSpriteRenderer { get; private set; }

    private CircleCollider2D circleCollider;
    private bool isJumping;

    private void Awake()
    {
        BallSpriteRenderer = ballRigidbody.GetComponent<SpriteRenderer>();
        circleCollider = ballRigidbody.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!GroundScript.Instance.Started)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            DoJump(true);
        else if (isJumping && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
            DoJump(false);
    }
    
    public bool IsBallGrounded() => GetGround();

    public Transform GetGround()
    {
        var ground = Physics2D.Raycast(ballRigidbody.transform.position, Vector2.down, circleCollider.radius + 0.05f).transform;
        if (ground is null)
            return null;
        return ground.transform.CompareTag("Ground") ? ground : ground.parent;
    }
    
    private void DoJump(bool firstTouch)
    {
        if (firstTouch)
            isJumping = true;

        if (!IsBallGrounded())
        {
            isJumping = false;
        }
        if (!isJumping)
            return;

        if (firstTouch)
            jumpSource.Play();

        ballRigidbody.AddForce(Vector2.up * (force * ballRigidbody.mass * Time.deltaTime), ForceMode2D.Impulse);
    }
}
