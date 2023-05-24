using UnityEngine;

[CreateAssetMenu]
public class Tutorial: ScriptableObject
{
    public string tutorialName = "New tutorial";
    public GameEvent gameEvent;
    public TutorialInfos[] infos;

    public void RaiseEndEvent()
    {
        if (!gameEvent)
            return;

        gameEvent.Raise();
    }
}

[System.Serializable]
public struct TutorialInfos
{
    public Sprite image;
    [TextArea(5,5)]
    public string sentence;
}
