using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoyYeahMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HoyYeahModBase : BaseUnityPlugin
    {
        private const string modGUID = "JaySw333zy.HoyYeahMod";
        private const string modName = "Hoy Yeah Mod";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static HoyYeahModBase Instance;

        internal ManualLogSource mls;

        

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Loading Hoy Yeah Mod");



            harmony.PatchAll();
           
            mls.LogInfo("Hoy Yeah Mod has started XD");
        }
    }

    // ADD HORN PATCH HERE



}
