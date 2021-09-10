public interface IUiView 
{
    string CurrentScoreText { set; }
    string BestScoreText { set; }
    string TimeText { set; }
    string ObstaclesText { set; }
    bool ShowCurrentScoreText { set; }
    bool ShowBestScoreText { set; }
    bool ShowTimeText { set; }
    bool ShowObstaclesText { set; }
    bool ShowPressOnAnyKeyText { set; }
    bool ShowGameOver { set; }
}
