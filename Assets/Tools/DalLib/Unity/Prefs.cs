using UnityEngine;
using System.Collections;


namespace DaleranGames.Tools
{

    public delegate void PrefenceChange<T>(T val);

    public static class Prefs
    {
        private static string version = "sputnik1";

        public static PrefenceChange<float> MasterVolumeChange;
        public static PrefenceChange<float> SFXVolumeChange;
        public static PrefenceChange<float> MusicVolumeChange;

        static Prefs ()
        {
            if (Version != version)
            {
                //Upgrade Process
                PlayerPrefs.SetString("Version",version);
            }
        }
        
        public static string Version
        {
            get { return PlayerPrefs.GetString("Version","sputnik1"); }

        }

        public static float MasterVolume
        {
            get { return PlayerPrefs.GetFloat("MasterVolume",1f); }
            set
            {
                PlayerPrefs.SetFloat("MasterVolume", Mathf.Clamp(value,0f,1f));
                PlayerPrefs.Save();
                if (MasterVolumeChange != null)
                    MasterVolumeChange(Mathf.Clamp(value, 0f, 1f));
            }
        }

        public static float MusicVolume
        {
            get { return PlayerPrefs.GetFloat("MusicVolume", 1f); }
            set
            {
                PlayerPrefs.SetFloat("MusicVolume", Mathf.Clamp(value, 0f, 1f));
                PlayerPrefs.Save();
                if (MusicVolumeChange != null)
                    MusicVolumeChange(Mathf.Clamp(value, 0f, 1f));
            }
        }

        public static float SFXVolume
        {
            get { return PlayerPrefs.GetFloat("SFXVolume", 1f); }
            set
            {
                PlayerPrefs.SetFloat("SFXVolume", Mathf.Clamp(value, 0f, 1f));
                PlayerPrefs.Save();
                if (SFXVolumeChange != null)
                    SFXVolumeChange(Mathf.Clamp(value, 0f, 1f));
            }
        }
    }

}
