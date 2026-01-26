using UnityEngine;
using UnityEngine.UI;

public class NextLevel: MonoBehaviour
{

  [SerializeField] private Text nextLevelText;

    void Start()
    {
        int nextLevel = Customize.nextLevel;
        nextLevelText.text = $"ŽŸ‚ÌƒŒƒxƒ‹‚Ì‹àŠz: {nextLevel}‰~";

    }
}
