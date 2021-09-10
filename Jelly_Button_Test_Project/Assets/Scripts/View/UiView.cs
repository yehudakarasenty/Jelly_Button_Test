using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IUiView
{
    private IUiController mController;

    [SerializeField]
    private Text currentScoreText;

    [SerializeField]
    private Text bestScoreText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text asteroidsText;

    [SerializeField]
    private Text pressAnyKeyText;

    [SerializeField]
    private Text gameOverText;

    private void Awake()
    {
        mController = SingleManager.Get<IUiController>();
        mController.SetView(this);
    }

    public string CurrentScoreText { set => currentScoreText.text = value; }
    public string BestScoreText { set => bestScoreText.text = value; }
    public string TimeText { set => timeText.text = value; }
    public string ObstaclesText { set => asteroidsText.text = value; }
    public bool ShowCurrentScoreText { set => currentScoreText.enabled = value; }
    public bool ShowBestScoreText { set => bestScoreText.enabled = value; }
    public bool ShowTimeText { set => timeText.enabled = value; }
    public bool ShowObstaclesText { set => asteroidsText.enabled = value; }
    public bool ShowPressOnAnyKeyText { set => pressAnyKeyText.enabled = value; }
    public bool ShowGameOver 
    { 
        set 
        {
            gameOverText.enabled = value;
        } 
    }
}
