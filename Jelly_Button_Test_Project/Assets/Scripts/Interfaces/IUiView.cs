public interface IUiView 
{
    string CurrentScoreText { set; }
    string HighestScoreText { set; }
    string TimeText { set; }
    string ObstaclesText { set; }
    bool ShowCurrentScoreText { set; }
    bool ShowHighestScoreText { set; }
    bool ShowTimeText { set; }
    bool ShowObstaclesText { set; }
    bool ShowPressOnAnyKeyText { set; }
    bool ShowGameOver { set; }
    bool ShowPlayAgainButton { set; }
    bool ShowHighestScoreCongratText { set; }
}
