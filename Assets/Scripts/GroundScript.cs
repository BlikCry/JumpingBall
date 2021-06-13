using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundScript : MonoBehaviour
{
    [SerializeField]
    private BallJump ballJump;
    [SerializeField]
    private GameObject circle;
    [SerializeField]
    private GameObject square;
    [SerializeField]
    private GameObject angle5;
    [SerializeField]
    private GameObject star6;
    [SerializeField]
    private float innerGroundScale = 0.3f;
    [SerializeField]
    private float deathLevel = -6f;

    public static GroundScript Instance;

    public bool Started;
    public int Level { get; private set; }
    public event Action OnDeath;

    private Transform previousBallGround;
    private bool isDead = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isDead)
            return;

        if (ballJump.Position.y < deathLevel)
        {
            isDead = true;
            OnDeath?.Invoke();
        }

        var ground = ballJump.GetGround();
        if (ground != null)
        {
            if (ground != previousBallGround)
            {
                Level++;
                ChangeGround(previousBallGround, ground);
            }

            previousBallGround = ground;
        }
    }

    private void ChangeGround(Transform previousGround, Transform ground)
    {
        var obj = circle;
        if (Level >= 7 && Level < 12 && Random.value < 0.5)
            obj = angle5;
        if (Level >= 12 && Level < 20)
        {
            var value = Random.Range(0, 3);
            if (value == 1)
                obj = angle5;
            if (value == 2)
                obj = square;
        }
        if (Level >= 20)
        {
            var value = Random.Range(0, 4);
            if (value == 1)
                obj = angle5;
            if (value == 2)
                obj = square;
            if (value == 3)
                obj = star6;
        }

        var newGround = Instantiate(obj, Vector3.zero, Quaternion.identity).transform;
        newGround.localScale = Vector3.zero;

        if (previousGround != null)
        {
            previousGround.GetComponentsInChildren<PolygonCollider2D>().ToList().ForEach(c => c.enabled = false);
            previousGround.GetComponent<CircleScaler>().Scale = 10f;
        }

        newGround.GetComponent<CircleScaler>().Scale = innerGroundScale;

        ground.GetComponent<CircleScaler>().Scale = 1f;
        ground.GetComponent<Animation>().Play();
        ground.GetComponentsInChildren<PolygonCollider2D>().ToList().ForEach(c => c.enabled = true);
        ballJump.BallSpriteRenderer.color = ground.GetComponent<SpriteRenderer>().color;
    }
}