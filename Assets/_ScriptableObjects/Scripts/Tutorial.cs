using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Tutorial: ScriptableObject
{
    public string tutorialName = "New tutorial";
    public TutorialInfos[] infos;
}

[System.Serializable]
public struct TutorialInfos
{
    public Sprite image;
    [TextArea(5,5)]
    public string sentence;
}
