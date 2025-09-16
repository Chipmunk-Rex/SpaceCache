using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearBtnRe : MonoBehaviour
{
    public void ClickHomeBtn()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ClickResetBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
