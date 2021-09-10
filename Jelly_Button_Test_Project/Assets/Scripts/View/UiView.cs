using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IUiView
{
    private IUiController mController;

    [SerializeField]
    private Text currentScoreText;

    [SerializeField]
    private Text highestScoreText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text asteroidsText;

    [SerializeField]
    private Text pressAnyKeyText;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text highestScoreCongratText;

    [SerializeField]
    private Button playAgainButton;

    private void Awake()
    {
        mController = SingleManager.Get<IUiController>();
        mController.SetView(this);
        playAgainButton.onClick.AddListener(PlayAgainButtonClicked);
    }

    private void PlayAgainButtonClicked()
    {
        mController.PlayAgainButtonClicked();
    }

    public string CurrentScoreText { set => currentScoreText.text = value; }
    public string HighestScoreText { set => highestScoreText.text = value; }
    public string TimeText { set => timeText.text = value; }
    public string ObstaclesText { set => asteroidsText.text = value; }
    public bool ShowCurrentScoreText { set => currentScoreText.enabled = value; }
    public bool ShowHighestScoreText { set => highestScoreText.enabled = value; }
    public bool ShowHighestScoreCongratText { set => highestScoreCongratText.enabled = value; }
    public bool ShowTimeText { set => timeText.enabled = value; }
    public bool ShowObstaclesText { set => asteroidsText.enabled = value; }
    public bool ShowPressOnAnyKeyText { set => pressAnyKeyText.enabled = value; }
    public bool ShowPlayAgainButton { set => playAgainButton.gameObject.SetActive(value); }
    public bool ShowGameOver { set => gameOverText.enabled = value; }
}
