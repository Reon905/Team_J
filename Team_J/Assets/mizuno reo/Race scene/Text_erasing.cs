using UnityEngine;
using System.Collections;
public class Text_erasing : MonoBehaviour 
{
    [SerializeField] float hideTime = 1.0f;

    private void OnEnable()
    {
        StartCoroutine(HideAfterTime());
    }

    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(hideTime);
        gameObject.SetActive(false);
    }
}
