using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutPanel : MonoBehaviour
{
    [SerializeField] private GameObject _outPanel;
    [SerializeField] private GameObject _greyPanel;
    [SerializeField] private GameObject mainBTN;
    [SerializeField] private GameObject restartBTN;
    private bool _isClicked = false;
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(!_isClicked)
            {
            _outPanel.SetActive(true);
            _greyPanel.SetActive(true);
            _isClicked = true;
            }
            else
            {
                _outPanel.SetActive(false);
                _greyPanel.SetActive(false);
                _isClicked = false;
            }
        }
    }

    public void MainBTNClick()
    {
        //SceneManager.LoadScene();
    }
    public void RestartBTNClick()
    {
        //SceneManager.LoadScene();
    }
}
