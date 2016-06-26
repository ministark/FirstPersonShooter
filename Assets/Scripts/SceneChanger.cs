﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

	public GameObject canvasBg;
	public GameObject menu1;
	public GameObject menu2;
	public GameObject menu3;
	public GameObject tabWindow; 
	public GameObject consoleWindow; 
	public GameObject error;

	//error validation
	public InputField username;
	public InputField roomname;
	public InputField tBots;
	public InputField ctBots;

	public NetworkManager nm;
	public Button joinRoom;

	public bool singlePlayer = false;

	void Start(){
		joinRoom.enabled = false;
		canvasBg.SetActive (true);
		menu1.SetActive (true);
		consoleWindow.SetActive (false);
	}
		
	void Update(){
		tabWindow.SetActive (Input.GetKey (KeyCode.Tab));
		consoleWindow.SetActive (!Input.GetKey (KeyCode.Tab));

		if (nm.isJoinedLobby == true) joinRoom.enabled = true;
	}

	public void EnableSinglePlayer(){
		singlePlayer = true;
		joinRoom.enabled = true;
		joinRoom.GetComponentInChildren<Text>().text="Create Room";
	}

	public void Menu1ToMenu2(){
		menu1.SetActive (false);
		menu2.SetActive(true);
		consoleWindow.SetActive (true);
	}
	public void Menu2ToMenu1(){
		menu2.SetActive (false);
		menu1.SetActive(true);
		consoleWindow.SetActive (false);
	}

	public void Menu2ToMenu3(){
		if (username.text == "" || roomname.text == "") {
			error.SetActive (true);
			return;
		}
		menu2.SetActive (false);
		menu3.SetActive(true);
	}
	public void Menu3ToMenu2(){
		menu3.SetActive (false);
		menu2.SetActive(true);
	}

	public void CloseError(){
		error.SetActive(false);
	}

	public void Menu3ToPlaySite(){
		int val;
		if(!int.TryParse(ctBots.text,out val) || !int.TryParse(tBots.text,out val)){
			error.SetActive(true);
			return;
		}
		menu3.SetActive (false);
		canvasBg.SetActive (false);

		//toggle
		if (singlePlayer) PhotonNetwork.offlineMode = true;
		else PhotonNetwork.ConnectUsingSettings("0.3");


	}
}
