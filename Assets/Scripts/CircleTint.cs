using System.Linq;
using UnityEngine;

namespace Assets
{
    public class CircleTint : MonoBehaviour
    {
        [SerializeField]
        private Color[] colors;

        private void Awake()
        {
            var color = colors[Random.Range(0, colors.Length)];
            GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(c =>
            {
                color.a = c.color.a;
                c.color = color;
            });
        }
    }
}