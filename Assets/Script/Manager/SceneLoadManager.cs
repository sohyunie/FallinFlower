using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    SplashScene,
    MainScene,
};

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    // static이 있을 때는 언제든 사용 가능, 없으면 씬로드 살아있지 않을 때는 사용불가능
    public static void MoveScene(Scene scene)
    {
        Debug.Log("씬이동");
        SceneManager.LoadScene((int)scene);
    }
}
