using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    //==================Variables====================
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button RegisterButton;
    public TempLoginObject log = new TempLoginObject();
    public string mode = "";
    //===============================================


    //================Start=&=Update=================
    // Start is called before the first frame update
    void Start()
    {
      switch (mode){
            case "Login":
                LoginButton.onClick.AddListener(() => InputLogin());
                break;
            case "Register":
                RegisterButton.onClick.AddListener(() => InputRegister());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (log.getSuccess())
        {
            //send username to PlayerMovement somehow
            SceneManager.LoadScene("ProgressSelectScreen");
        }
    }
    //================================================


    //===============Database=Connection===============
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost/Metroid/MetroidLogin.php", form))
        {
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Connection successful
                //Check the resulting echo:
                Debug.Log("Result: " + www.downloadHandler.text);

                //Successful login returns "Success"
                if (www.downloadHandler.text == "Success")
                {
                    log.setSuccess(true);
                    Debug.Log("True, success is: " + log.getSuccess());
                    log.setUsername(UsernameInput.text);
                    Debug.Log("The verified username is: " + log.getUsername());
                    //Saving.LoadPlayer(UsernameInput.text);
                }

                //Otherwise, it's a failure
                else
                {
                    //Not valid
                    Debug.Log("False, success is: " + log.getSuccess());
                    Debug.Log("Username should be empty: " + log.getUsername() + ".");
                }
            }

        }
    }

    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Metroid/MetroidRegister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Connection successful
                //Check the resulting echo:
                Debug.Log("Result" + www.downloadHandler.text);

                //Successful register
                if (www.downloadHandler.text == "Success")
                {
                    log. setSuccess(true);
                    Debug.Log("True, success is: " + log.getSuccess());
                    log.setUsername(UsernameInput.text);
                    Debug.Log("The verified username is: " + log.getUsername());
                }

                //Otherwise, it's a failure
                else
                {
                    //Not valid
                    Debug.Log("False, success is: " + log.getSuccess());
                    Debug.Log("Username should be empty: " + log.getUsername() + ".");
                }
            }

        }
    }


    //==================================================


    //===========Database=InputField=Methods============
    void InputLogin()
    {
        StartCoroutine(Login(UsernameInput.text, PasswordInput.text));
    }

    void InputRegister()
    {
        StartCoroutine(Register(UsernameInput.text, PasswordInput.text));
    }
    //==================================================


    //==================Other=Methods====================

    //===================================================
}
