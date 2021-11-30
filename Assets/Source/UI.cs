using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ManAndPig.Model;

internal class UI : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _resultText;
    [SerializeField] private string _winText;
    [SerializeField] private string _loseText1;
    [SerializeField] private string _loseText2;

    public void OpenLosePanel(DieType dieType)
    {
        _losePanel.SetActive(true);

        if (dieType is BombKill)
            _resultText.text = _loseText1;
        else if (dieType is EnemyKill)
            _resultText.text = _loseText2;
    }

    public void OpenWinPanel()
    {
        _losePanel.SetActive(true);
        _resultText.text = _winText;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}