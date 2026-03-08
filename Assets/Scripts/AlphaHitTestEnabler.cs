using UnityEngine;
using UnityEngine.UI;

public class AlphaHitTestEnabler : MonoBehaviour
{
    [SerializeField] private float value = 0.1f;
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        if (img != null)
        {
            img.alphaHitTestMinimumThreshold = value;
        }
    }
}
