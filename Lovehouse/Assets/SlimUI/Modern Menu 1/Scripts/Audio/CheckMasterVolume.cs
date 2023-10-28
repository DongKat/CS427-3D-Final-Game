using UnityEngine;
using System.Collections;

namespace SlimUI.ModernMenu{
	public class CheckMasterVolume : MonoBehaviour {
		public void  Start (){
			// remember volume level from last time
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
		}

		public void UpdateVolume (){
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
		}
	}
}