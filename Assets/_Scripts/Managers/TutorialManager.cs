using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region Variables
    [Header("Tutorial at Start")]
    [SerializeField] private Tutorial startTuto;

    [Header("Tutorial UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleField;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private Image imageField;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    [Header("Events")]
    [SerializeField] private GameEvent OnTutorialStarted;
    [SerializeField] private GameEvent OnTutorialEnded;
    #endregion

    #region Properties
    public static TutorialManager Instance { get; private set; }
    public Tutorial CurrentTutorial { get; private set; }
    public int CurrentIndex { get; private set; }
    #endregion

    #region Builts_In
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        EnablePanel(false);
    }

    private void Start()
    {
        if (!startTuto)
            return;

        StartTutorial(startTuto);
    }

    private void OnEnable()
    {
        nextButton.onClick.AddListener(Next);
        previousButton.onClick.AddListener(Previous);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(Next);
        previousButton.onClick.RemoveListener(Previous);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Enable the tutorial panel and loads the given tutorial
    /// </summary>
    public void StartTutorial(Tutorial tutorial)
    {
        if (CurrentTutorial)
            return;

        CurrentTutorial = tutorial;
        CurrentIndex = 0;

        if (tutorial.infos.Length <= 0)
            return;

        //Ui
        titleField.text = tutorial.tutorialName;
        SetText();
        EnablePanel(true);
        //Events
        OnTutorialStarted.Raise();
    }

    /// <summary>
    /// Set current tutorial text and image
    /// </summary>
    private void SetText()
    {
        //Set text
        TutorialInfos infos = GetTutorial();
        textField.text = infos.sentence;
        
        //Set image
        if (infos.image)
        {
            imageField.enabled = true;
            imageField.sprite = infos.image;
        }
        else
            imageField.enabled = false;
    }

    /// <summary>
    /// Enable the tutorial panel
    /// </summary>
    private void EnablePanel(bool state)
    {
        panel.SetActive(state);
    }

    /// <summary>
    /// Return the tutorial infos at index
    /// </summary>
    private TutorialInfos GetTutorial()
    {
        return CurrentTutorial.infos[CurrentIndex];
    }

    /// <summary>
    /// Load the next sentence
    /// </summary>
    private void Next()
    {
        //Exit
        if (CurrentIndex >= CurrentTutorial.infos.Length - 1)
        {
            //Events
            CurrentTutorial.RaiseEndEvent();
            CurrentTutorial = null;
            //UI
            EnablePanel(false);
            OnTutorialEnded.Raise();
            return;
        }

        //Continue
        CurrentIndex++;
        SetText();
    }

    /// <summary>
    /// Load the previous sentence
    /// </summary>
    private void Previous()
    {
        //Exit
        if (CurrentIndex <= 0)
            return;

        //Continue
        CurrentIndex--;
        SetText();
    }
    #endregion
}
