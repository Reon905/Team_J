using UnityEngine;

public class CarVisualShake : MonoBehaviour
{
    private Vector3 defaultPos;
    private float shakePower = 0f;


    [Header("Sway Settings")]
    [SerializeField] private float swaySpeed = 10f;   // —h‚ê‚é‘¬‚³
    [SerializeField] private float swayWidth = 5f; // —h‚ê‚é•

    void Start()
    {
        defaultPos = transform.localPosition;
    }

    void Update()
    {
   
        if (shakePower > 0f)
        {
            //¶‰E‚É‚ä‚Á‚­‚è—h‚ç‚·
            float x = Mathf.Sin(Time.time * swaySpeed) * swayWidth * shakePower;

            transform.localPosition = defaultPos + new Vector3(x, 0f, 0f);
        }
        else
        {
            transform.localPosition = defaultPos;
        }
    }

    // ŠO‚©‚ç—h‚ê‚Ì‹­‚³‚ğ“n‚·
    public void SetShake(float power)
    {
        shakePower = power;
    }
}
