﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class GameLoader : MonoBehaviour {

	// References for the elements
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;

	public GameObject player;
	public GameObject background;

	// Reference for the internal elements
	Image button1img;
	Image button2img;
	Image button3img;
	Text  button1txt;
	Text  button2txt;
	Text  button3txt;

	SpriteRenderer playerimg;
	SpriteRenderer backgroundimg;
	
	// Use this for initialization
	public void LoadAssetsFromFile() {
		// get the blob data
		GameObject gobj = GameObject.Find("DataBlob");
		datablob db = gobj.GetComponent<datablob>();
		JSONNode N = db.jsonnode; 
		// set assets folder
		string folder = "GAME"+N["gameID"];

		// button loader
		button1img = button1.GetComponent<Image> ();
		button1txt = button1.GetComponentInChildren<Text> ();
 		button2img = button2.GetComponent<Image> ();
		button2txt = button2.GetComponentInChildren<Text> ();
		button3img = button3.GetComponent<Image> ();
		button3txt = button3.GetComponentInChildren<Text> ();
		// load button data
		button1img.sprite = AssetManager.spriteCreator(AssetManager.LoadSavedTextureFromFile(N["answerList"][0]["imageFile"]["filename"],folder));
		button2img.sprite = AssetManager.spriteCreator(AssetManager.LoadSavedTextureFromFile(N["answerList"][1]["imageFile"]["filename"],folder));
		button3img.sprite = AssetManager.spriteCreator(AssetManager.LoadSavedTextureFromFile(N["answerList"][2]["imageFile"]["filename"],folder));
		button1txt.text = "";
		button2txt.text = "";
		button3txt.text = "";

		//player loader
		playerimg = player.GetComponent<SpriteRenderer> ();
		playerimg.sprite = AssetManager.spriteCreator(AssetManager.LoadSavedTextureFromFile(N["playerAsset"][0]["imageFile"]["filename"],folder));

		//background loader
		backgroundimg = background.GetComponent<SpriteRenderer> ();
		backgroundimg.sprite = AssetManager.spriteCreator(AssetManager.LoadSavedTextureFromFile(N["backgroundAsset"][0]["imageFile"]["filename"],folder));
		float boundx = backgroundimg.sprite.bounds.size.x;
		float boundy = backgroundimg.sprite.bounds.size.y;
		float scale = Mathf.Min(20/boundx,20/boundy);
		background.transform.localScale = new Vector3(scale, scale, 1F);

	}

	public void DisableButtons(){
		button1.SetActive (false);
		button2.SetActive (false);
		button3.SetActive (false);
	}
}
