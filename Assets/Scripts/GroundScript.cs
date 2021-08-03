using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundScript : MonoBehaviour
{
    public const string CheckpointKey = "CHECKPOINT";

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
    private Transform startupCircle;
    [SerializeField]
    private float deathLevel = -6f;

    public static GroundScript Instance;

    public bool PrepareStarted;
    public bool LevelsSkipped { get; private set; }
    public int Level { get; private set; }
    public int SkipLevels => skipLevels;
    public event Action OnDeath;

    private Transform currentInnerGround;
    private Transform currentOuterGround;
    private bool isDead;

    private static int skipLevels;

    private void Awake()
    {
        Instance = this;
        skipLevels = PlayerPrefs.GetInt(CheckpointKey, 0);
        currentInnerGround = startupCircle;
        
    }

    private void Start()
    {
        ballJump.Ball.SetActive(false);
    }

    private void Update()
    {
        if(!PrepareStarted)
            return;

        if (isDead)
            return;

        if (ballJump.Position.y < deathLevel)
        {
            isDead = true;
            skipLevels = Math.Max(Level - 6, 0);
            PlayerPrefs.SetInt(CheckpointKey, SkipLevels);
            OnDeath?.Invoke();
        }
        
        var ground = ballJump.GetGround();

        if (!LevelsSkipped && currentOuterGround != null && currentOuterGround.localScale.x + 0.01f > 1f)
        {
            if (skipLevels > 0)
            {
                skipLevels--;
                ChangeGround();
            }
            else
            {
                ballJump.Ball.SetActive(true);
                LevelsSkipped = true;
            }
        }

        if (ground == currentInnerGround || currentOuterGround is null)
            ChangeGround();
    }

    private void ChangeGround()
    {
        Level++;
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

        if (currentOuterGround != null)
        {
            currentOuterGround.GetComponentsInChildren<PolygonCollider2D>().ToList().ForEach(c => c.enabled = false);
            currentOuterGround.GetComponent<CircleScaler>().Scale = 10f;
        }

        newGround.GetComponent<CircleScaler>().Scale = innerGroundScale;

        currentInnerGround.GetComponent<CircleScaler>().Scale = 1f;
        currentInnerGround.GetComponent<Animation>().Play();
        currentInnerGround.GetComponentsInChildren<PolygonCollider2D>().ToList().ForEach(c => c.enabled = true);
        ballJump.BallSpriteRenderer.color = currentInnerGround.GetComponent<SpriteRenderer>().color;

        currentOuterGround = currentInnerGround;
        currentInnerGround = newGround;
    }
}