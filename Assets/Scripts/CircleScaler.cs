using UnityEngine;

public class CircleScaler : MonoBehaviour
{
    [SerializeField]
    private float destroyScale;
    [HideInInspector]
    public float Scale = 1f;

    private void Update()
    {
        var scale = 1f;
        if (!GroundScript.Instance.LevelsSkipped)
            scale = Mathf.Clamp(Mathf.Pow(GroundScript.Instance.SkipLevels, 2), 5, 100);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * Scale, Time.deltaTime * scale);
        if (Mathf.Abs(transform.localScale.x - destroyScale) < 0.1f)
            Destroy(gameObject);
    }
}
