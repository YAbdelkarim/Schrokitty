using UnityEngine;
using System.Collections;
using TMPro;

public class TemporaryActivation : MonoBehaviour
{
    
    public float showDuration = 2f;


    public void ShowArrow()
    {
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(showDuration);
        gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
