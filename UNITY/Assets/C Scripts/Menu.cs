using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScene(string name){
        SceneManager.LoadScene(name);
    }
}
