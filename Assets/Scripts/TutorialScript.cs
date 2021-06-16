using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private const string TutorialKey = "TUTORIAL";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(TutorialKey))
            gameObject.SetActive(false);
        PlayerPrefs.SetInt(TutorialKey, 1);
    }
}
