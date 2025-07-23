using UnityEngine;

public class Potal : Collidable
{
    public string[] sceneNames;
    public static Potal instance;
    public string Scen;

    public void Awake()
    {
        instance = this;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            //Teleport the player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
