using UnityEngine;

internal class CameraRotator: MonoBehaviour
{
    [SerializeField]
    private float startSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float speedPerLevel;

    private float previousSpeed;
    private float side = 1;
    private void Start()
    {
        side = Random.value < 0.5f ? 1 : -1;
    }

    private void Update()
    {
        var speed = Mathf.Clamp((startSpeed + GroundScript.Instance.Level - 1) * speedPerLevel, -maxSpeed, maxSpeed) * side * (GroundScript.Instance.Level % 2 - 0.5f) * 2;
        speed = Mathf.Lerp(previousSpeed, speed, Time.deltaTime);
        previousSpeed = speed;
        if (GroundScript.Instance.LevelsSkipped)
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}