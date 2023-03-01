using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu;
    public Text usernameObject;
    public InputField usernameField;
    public InputField ipField;
    public Text errorMessage;
    public Camera playerCamera = null;

    private bool startDrawUsernames = false;
    public Dictionary<int, string> usernames = new Dictionary<int, string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object...");
            Destroy(this);
        }
    }

    public void setDrawUsernames(bool draw)
    {
        startDrawUsernames = draw;
    }

    public void addPlayer(int playerId, string username)
    {
        usernames.Add(playerId, username);
    }

    private void Update()
    {
        if(startDrawUsernames && playerCamera != null)
        {
            foreach(int id in usernames.Keys)
            {
                Vector3 screenPosition = playerCamera.WorldToScreenPoint(GameManager.players[id].transform.position);
                Text newPlayer = Instantiate(usernameObject, screenPosition, new Quaternion(0, 0, 0, 0));
                newPlayer.text = GameManager.players[id].username;
                newPlayer = transform.SetParent(startMenu.transform, false);
            }
        }
    }

    public void ConnectToServer()
    {
        if (usernameField.text == "")
        {
            errorMessage.text = "Please enter a username.";
        }
        else
        {
            if (ipField.text == "")
            {
                errorMessage.text = "Please enter an IP.";
            } else
            {
                Client.instance.ip = ipField.text;
                startMenu.SetActive(false);
                usernameField.interactable = false;
                Client.instance.ConnectToServer();

                /*
                if(!Client.instance.CheckServer())
                {
                    Debug.Log("Server is closed.");
                } else
                {
                    startMenu.SetActive(false);
                    usernameField.interactable = false;
                    Client.instance.ConnectToServer();
                } */
            }
        }
    }
}