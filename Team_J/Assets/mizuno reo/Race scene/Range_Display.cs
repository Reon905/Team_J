using UnityEngine;

public class Range_Display : MonoBehaviour
{
    private void Update()
    {
        Vector2 origin = transform.position;
        Vector2 directon = Vector2.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, directon, 10f);

        Debug.DrawRay(origin, directon * 10f, Color.red);

        if(hit.collider != null )
        {
            Debug.Log("ãóó£ÅF" + hit.distance);
        }
    }
}
