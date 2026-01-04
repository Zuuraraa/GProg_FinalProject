using UnityEngine;

public class SimpleTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        ShowTutorial();
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}