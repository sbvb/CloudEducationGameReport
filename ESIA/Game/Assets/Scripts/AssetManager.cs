﻿using UnityEngine;
using System.Collections;
using System.IO;

public class AssetManager : MonoBehaviour {
	// DownloadStrings
	string url = "http://www.hdpaperz.com/wallpaper/original/cute-parrots-hd-wallpaper.jpg";
	string url2 = "https://scontent-a-gru.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/10299980_993432164004064_2500348929135397570_n.jpg?oh=c59c6a3d6eef026468182b26baf86bda&oe=556B0F5F";
	// Folder where data will be saved
	string folder = "Pasta Legal";
	// Queue Information
	int downloadQueue = 0;
	bool downloadFinish = false;
	int totalQueueSize = 2;
	// Force starter
	void Start(){
		//StartDownload ();
	}

	// Start Coroutines that download the files.
	public void StartDownload(){
		StartCoroutine(Downloader("Papagaio.png", folder, url));
		StartCoroutine(Downloader("Paçoca.png", folder, url2));
	}

	void Update(){
		// Run the Next stuff after all downloads are complete
		// It will run only one time
		if (!downloadFinish && downloadQueue < 1) {
						Debug.Log ((totalQueueSize-downloadQueue)*100/totalQueueSize);
						Debug.Log ("Rodando Coiso NOW"); 
						downloadFinish = true;
				} else if (!downloadFinish)
			Debug.Log ((totalQueueSize-downloadQueue)*100/totalQueueSize);
	}

	// Callback function to download a texture from a urlpath into a file mamed filename
	IEnumerator Downloader(string filename,string foldername, string urlPath) {
		// Add 1 item to the downloadQueue
		downloadQueue++;
		// Show what is beeing downloading
		Debug.Log ("Downloading: " + urlPath);
		// http request
		WWW www = new WWW(urlPath);
        yield return www; 		
		// Save File that was downloaded to a director
		Texture2D download = www.texture;
		SaveTextureToFile(download, foldername , filename);	
		// Display where the data was saved
		Debug.Log("File Saved: " + Application.persistentDataPath +"/"+ foldername +"/"+ filename );
		// Remove 1 item form the downloadQueue
		downloadQueue--;
	}

	// File Saver
	// Save a file to a specific folder on the project persistency folder
	void SaveTextureToFile(Texture2D texture, string folder,string fileName){
		(new FileInfo(Application.persistentDataPath +"\\"+ folder )).Directory.Create();
		Directory.CreateDirectory(Application.persistentDataPath +"\\"+ folder);
		File.WriteAllBytes(Application.persistentDataPath +"\\"+ folder +"\\"+ fileName, texture.EncodeToPNG());
	}
	
	// Load a texture from a folder on the aplication data
	public static Texture2D LoadSavedTextureFromFile(string fileName, string folder){	
		byte[] byteVector = File.ReadAllBytes(Application.persistentDataPath +"\\"+ folder +"\\"+fileName);
		Texture2D loadedTexture = new Texture2D(8,8);
		loadedTexture.LoadImage(byteVector);
		return loadedTexture;
	}
	
	public static Sprite spriteCreator(Texture2D texture){
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
	}

}
