using UnityEngine;

public class BallJump : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D ballRigidbody;
    [SerializeField]
    private float force = 10;
    [SerializeField]
    private float jumpInterval = 0.4f;
    [SerializeField]
    private AudioSource jumpSource;

    public Vector2 Position => ballRigidbody.position;
    public SpriteRenderer BallSpriteRenderer { get; private set; }

    private CircleCollider2D circleCollider;
    private bool isJumping;
    private float lastJumpTime = float.NegativeInfinity;

    private void Awake()
    {
        BallSpriteRenderer = ballRigidbody.GetComponent<SpriteRenderer>();
        circleCollider = ballRigidbody.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!GroundScript.Instance.Started)
            return;

        if (IsBallGrounded() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            DoJump();
            isJumping = true;
            lastJumpTime = Time.time;
            jumpSource.Play();
        }
        else if (isJumping && Input.GetKey(KeyCode.Mouse0) && Time.time - lastJumpTime < jumpInterval)
        {
            DoJump();
        }
        else
        {
            isJumping = false;
        }
    }
    
    public bool IsBallGrounded() => GetGround();

    public Transform GetGround()
    {
        var ground = Physics2D.Raycast(ballRigidbody.transform.position, Vector2.down, circleCollider.radius + 0.05f).transform;
        if (ground is null)
            return null;
        return ground.transform.CompareTag("Ground") ? ground : ground.parent;
    }
    
    private void DoJump()
    {   
        ballRigidbody.AddForce(Vector2.up * (force * ballRigidbody.mass * Time.deltaTime), ForceMode2D.Impulse);
    }
}
