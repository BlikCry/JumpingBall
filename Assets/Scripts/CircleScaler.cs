using UnityEngine;

public class CircleScaler : MonoBehaviour
{
    [SerializeField]
    private float destroyScale;
    [HideInInspector]
    public float Scale = 1f;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * Scale, Time.deltaTime);
        if (Mathf.Abs(transform.localScale.x - destroyScale) < 0.1f)
            Destroy(gameObject);
    }
}
