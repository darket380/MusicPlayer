using System.IO;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainScr : MonoBehaviour {

	public Text MusicList;
	public InputField NumberMusic;
	public InputField AdressMusic;
	public AudioSource audio_left;
	public AudioSource audio_right;
	DirectoryInfo di;
	FileInfo [] UserFiles;
	public bool TextAdressShow;
	public string Filesadress;
	WWW www;
	void Start()
	{
		Filesadress = Application.dataPath + "/UserSounds";
		SearchAudio();
	}
	public void SearchAudio(){
		MusicList.text = "";
		di = new DirectoryInfo(Filesadress);
		UserFiles = di.GetFiles("*",SearchOption.TopDirectoryOnly);
		for (int i=0 ; i<UserFiles.Length;i++)
		{
			MusicList.text += "\n("+i+"):"+UserFiles[i].Name;
		}
	}
	public void GetAudioStream(string url,bool b)
	{
		if(b == true){
			Debug.Log("url = " + url);
			www = new WWW(url);
			StartCoroutine(WaitRightForAudioClip());
		}else{
			Debug.Log("url = " + url);
			www = new WWW(url);
			StartCoroutine(WaitLeftForAudioClip());
		}
	}
	
	public IEnumerator WaitLeftForAudioClip()
	{
		yield return www;
		audio_left.clip = www.GetAudioClip(false,true);
		audio_left.Play();
	}
	public IEnumerator WaitRightForAudioClip()
	{
		yield return www;
		audio_right.clip = www.GetAudioClip(false,true);
		audio_right.Play();
	}
	void Update()
	{

	}
	public void ClickArrow()
	{
		if(TextAdressShow == true) {
			AdressMusic.gameObject.SetActive(false);
			TextAdressShow = false;
		}else{
			AdressMusic.gameObject.SetActive(true);
			TextAdressShow = true;
		}
	}
	public void ClickGoButton()
	{
		if(TextAdressShow == true) {
			Filesadress = AdressMusic.text;
			SearchAudio();
		}
	}
	public void LeftAudioStart()
	{
		GetAudioStream("file:///" +Application.dataPath  + "/UserSounds/" + UserFiles[Convert.ToInt32(NumberMusic.text)].Name,false);
	}
	public void RightAudioStart()
	{
		GetAudioStream("file:///" +Application.dataPath  + "/UserSounds/" + UserFiles[Convert.ToInt32(NumberMusic.text)].Name,true);
	}
}