using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeRoomButton : MonoBehaviour
{
    [SerializeField] private int scene; //the index of the scene in build settings to move to
    public int Scene { get { return scene; } set { scene = value; } }
    [SerializeField] private bool reloadScene; //whether this should just reload the current scene or not.
    private Button button;

    public void ChangeScene()
    {
        if (reloadScene)
        {
            scene = SceneManager.GetActiveScene().buildIndex;
        }

        SceneManager.LoadScene(scene);
    }
}

