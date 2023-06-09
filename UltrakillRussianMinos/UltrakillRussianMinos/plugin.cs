﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine;
using HarmonyLib;
namespace UltrakillRussianMinos
{
    [BepInPlugin("com.gooeyessential.rus", "Russian Minos", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle assets = AssetBundle.LoadFromMemory(Resource1.minosvl);
        //Yo. The russian letters aren't supported by ultrakill subtitles font
        private static SubtitledAudioSource.SubtitleDataLine MakeLine(string subtitle, float time)
        {
            return new SubtitledAudioSource.SubtitleDataLine
            {
                subtitle = subtitle,
                time = time
            };
        }
        void Awake()
        {
            Harmony harmony = new Harmony("com.gooeyessential.rus");
            harmony.PatchAll();


        }

        [HarmonyPatch(typeof(MinosPrime), nameof(MinosPrime.PlayVoice))]
        class MinosPatch
        {
            public static void Prefix(MinosPrime __instance, AudioClip[] voice)
            {
                return;
            }
        }
        [HarmonyPatch(typeof(MinosPrime), "Start")]
        class MinosPatch1
        {
            public static void Postfix(MinosPrime __instance, ref AudioSource ___aud)
            {
                ___aud.volume *= 5;
                __instance.dropkickVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("judgement.mp3")
                   };
                __instance.comboVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("prepare thyself.mp3")
                };
                __instance.boxingVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("thy end is now.mp3")
                };
                __instance.riderKickVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("die.mp3")
                };
                __instance.dropAttackVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("crush.mp3")
                };
                __instance.hurtVoice = new AudioClip[]
                {
                    assets.LoadAsset<AudioClip>("hurt1.mp3"),
                    assets.LoadAsset<AudioClip>("hurt2.mp3")
                };
                __instance.phaseChangeVoice = assets.LoadAsset<AudioClip>("weak.mp3");

            }
        }
        [HarmonyPatch(typeof(StockMapInfo), "Awake")]
        class Patch2
        {

            private static void Postfix(StockMapInfo __instance)
            {
                foreach (AudioSource audioSource in Resources.FindObjectsOfTypeAll<AudioSource>())
                {
                    if (audioSource.clip)
                    {
                        List<SubtitledAudioSource.SubtitleDataLine> list = new List<SubtitledAudioSource.SubtitleDataLine>();
                        Debug.Log(audioSource.clip.name);
                        if (audioSource.clip.name == "mp_intro2")
                        {
                            audioSource.clip = assets.LoadAsset<AudioClip>("minosIntro.mp3");
                        }
                        else if (audioSource.clip.name == "mp_outro")
                        {
                            audioSource.clip = assets.LoadAsset<AudioClip>("outro.mp3");
                        }
                        else if (audioSource.clip.name == "mp_deathscream")
                        {
                            audioSource.clip = assets.LoadAsset<AudioClip>("death scream.mp3");
                        }
                        else if (audioSource.clip.name == "mp_useless")
                        {
                            audioSource.clip = assets.LoadAsset<AudioClip>("useless.mp3");
                        }
                    }
                }
            }
        }
    }
}
