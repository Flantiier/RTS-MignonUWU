using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;

    public void StartTutorial()
    {
        TutorialManager.Instance.StartTutorial(tutorial);
    }
}
