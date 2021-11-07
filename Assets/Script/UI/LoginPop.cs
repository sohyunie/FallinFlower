using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoginPop : UEPopup
{
    public static LoginPop Instance;
    public static LoginPop ShowPop()
    {
        Instance = UEPopup.GetInstantiateComponent<LoginPop>();
        Instance.ShowPopUp();
        return Instance;
    }

    public void EndEdit()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        Debug.Log(inputText.text);
        string strFile = string.Format(Application.persistentDataPath + "/" + StringConst.ID_PATH, inputText.text);
        FileInfo fileInfo = new FileInfo(strFile);
        if (fileInfo.Exists)
        {
            Debug.Log("이미 있어!");
            DataManager.Instance.LoadJsonData(inputText.text);
        }
        else
        {
            Debug.Log("새로운 아이디!");
            DataManager.Instance.SaveJsonData(new UserData(inputText.text, 0, 1, 21, 11)); // UserData 만들기
        }

        SceneLoadManager.MoveScene(Scene.MainScene);
    }

    [SerializeField] private Text inputText;
}
