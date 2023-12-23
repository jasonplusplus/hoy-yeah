using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using System.IO;
using BepInEx.Logging;

namespace HoyYeah.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class HornPatch
    {

        private static List<string> audios = new List<string> { "Clown1.mp3", "ClownFar.mp3" };

        private static List<AudioClip> clips = new List<AudioClip>();

        private static string uPath = Path.Combine(Paths.PluginPath + "\\JaySw333zy-HoyYeahMod\\");

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void hornAudioPatch(GrabbableObject __instance)
        {
            for (int i = 0; i < audios.Count; i++)
            {
                LoadAudioClip(uPath + audios[i]);
            }
            if (__instance != null && __instance.GetComponent<NoisemakerProp>() != null && __instance.itemProperties.name == "Clown Horn")
            {
                // clown horn found
                __instance.GetComponent<NoisemakerProp>().noiseSFX[0] = clips[0];
                __instance.GetComponent<NoisemakerProp>().noiseSFXFar[0] = clips[1];
                //val.LogMessage(__instance.GetComponent<NoisemakerProp>().noiseSFX[0]).name);
                //val.LogMessage(__instance.GetComponent<NoisemakerProp>().noiseSFXFar[0]).name);
            }
        }
        private static void LoadAudioClip(string filepath)
        {
            //ManualLogSource val = Logger.CreateLogSource("MetalPipeSFX.HornMoan");
            UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip(filepath, (AudioType)13);
            audioClip.SendWebRequest();
            while (!audioClip.isDone)
            {
            }
            if (audioClip.error != null)
            {
               // val.LogError((object)("Error loading sounds: " + filepath + "\n" + audioClip.error));
            }
            AudioClip content = DownloadHandlerAudioClip.GetContent(audioClip);
            if (content && (int)content.loadState == 2)
            {
                //val.LogInfo((object)("Loaded " + filepath));
                content.name = Path.GetFileName(filepath);
                clips.Add(content);
            }
        }
    }
}
