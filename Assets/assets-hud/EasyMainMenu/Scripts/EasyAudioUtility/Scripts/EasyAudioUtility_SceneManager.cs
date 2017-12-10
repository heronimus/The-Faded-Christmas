﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAudioUtility_SceneManager : MonoBehaviour {

	public EasyAudioUtility_SMHelper[] manager;

	// Use this for initialization
	void Start () {
		
	}

    public void FadeVolume(float to)
    {

        for(int i =0; i < GetComponent<EasyAudioUtility>().helper.Length; i++)
        {
            //detect which ever source is playing
            if(GetComponent<EasyAudioUtility>().helper[i].source.isPlaying)
            {
                //lerp it's volume to 'to'
                StartCoroutine(LerpAudioVolume(to, 0.5f, GetComponent<EasyAudioUtility>().helper[i].source));
            }

        }
    }

    /// <summary>
    /// Here we simply fade in-out of the current 
    // playing audio to the audio clip defined in this class
    /// </summary>
    /// <param name="sName">Pass the Scene Name you are going into</param>
    public void onSceneChange(string sName){
        Debug.Log(sName + " is opend");

        for (int i = 0 ; i < manager.Length; i++){
			if(sName == manager[i].SceneName)
            {
                Debug.Log(sName + " is found");

                //looping in all the sounds available

                for (int j = 0; j <  GetComponent<EasyAudioUtility>().helper.Length; j++)
                {
                    //if we have a sound name defined in this scene manager
                    if(GetComponent<EasyAudioUtility>().helper[j].name == manager[i].name)
                    {
                        Debug.Log(sName + " audio replaced");

                        //replace the clip with the one defined
                        GetComponent<EasyAudioUtility>().helper[j].source.clip = manager[i].clip;
                        //and play it back again
                        GetComponent<EasyAudioUtility>().Play(manager[i].name);

                        //lerp audio back to 1
                        FadeVolume(1);

                    }
                }

                //we exit as soon as we found the match
                return;
            }
		}

	}

    IEnumerator LerpAudioVolume(float to, float time, AudioSource source)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            source.volume = Mathf.Lerp(source.volume, to, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}

[System.Serializable]
public class EasyAudioUtility_SMHelper{

	public string SceneName;
	public string name;
	public AudioClip clip;

}
