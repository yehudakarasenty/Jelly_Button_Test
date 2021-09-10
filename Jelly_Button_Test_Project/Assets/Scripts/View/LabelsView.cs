using UnityEngine;
using UnityEngine.UI;

public class LabelsView : MonoBehaviour, ILabelsView
{
    private ILabelsController mController;

    [SerializeField]
    private Text currentScoreText;

    [SerializeField]
    private Text bestScoreText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text asteroidsText;

    private void Start()
    {
        mController = SingleManager.Get<ILabelsController>();
        mController.SetView(this);
    }

    public string CurrentScoreText { set => currentScoreText.text = value; }
    public string BestScoreText { set => bestScoreText.text = value; }
    public string TimeText { set => timeText.text = value; }
    public string AsteroidsText { set => asteroidsText.text = value; }
}
