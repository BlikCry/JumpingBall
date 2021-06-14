using UnityEngine;
using Random = UnityEngine.Random;

public class CircleRotator : MonoBehaviour
{
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float speedPerLevel;
    [SerializeField]
    private float totalMaxSpeed;
    [SerializeField]
    private float speedDelta;

    private float speed;

    private static bool side;
    private static float prevSpeed;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);

        side = !side;
        speed = (Random.Range(minSpeed, maxSpeed) + speedPerLevel * GroundScript.Instance.Level) * (side ? 1 : -1);
        speed = Mathf.Clamp(speed, -totalMaxSpeed, totalMaxSpeed);

        if (Mathf.Abs(prevSpeed - speed) < speedDelta)
            speed = prevSpeed + speedDelta * (side ? 1 : -1) * (Mathf.Abs(prevSpeed) > speedDelta + minSpeed ? -1 : 1);

        prevSpeed = speed;
    }

    private void Update()
    {
        if (GroundScript.Instance.LevelsSkipped)
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
