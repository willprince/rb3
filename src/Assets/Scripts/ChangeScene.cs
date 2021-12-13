using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static PermManager;

public class ChangeScene : MonoBehaviour {

    public static ChangeScene cScene;


    public void Start()
    {
        cScene = this;
    }

    public static void loadNextScene(string sceneName) {
        
		SceneManager.LoadScene(sceneName);

        PermManager.pManager.setAreaFlag(1);

        PermManager.pManager.setLastPosition(new Vector2(406,461));
        Debug.Log("Setting scene to: " + PermManager.AreaFlag);
	}
}
