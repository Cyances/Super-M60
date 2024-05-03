using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.AI;
using GHPC.Camera;
using GHPC.Player;
using GHPC.State;
using SuperM60;
using MelonLoader;
using NWH.VehiclePhysics;
using UnityEngine;

[assembly: MelonInfo(typeof(SuperM60Mod), "Super M60", "1.0.0", "Cyance")]
[assembly: MelonGame("Radian Simulations LLC", "GHPC")]

namespace SuperM60
{
    public class SuperM60Mod : MelonMod
    {
        public static GameObject[] vic_gos;
        public static GameObject gameManager;
        public static CameraManager camManager;
        public static PlayerInput playerManager;

        public IEnumerator GetVics(GameState _)
        {
            vic_gos = GameObject.FindGameObjectsWithTag("Vehicle");

            yield break;
        }

        public override void OnInitializeMelon()
        {
            MelonPreferences_Category cfg = MelonPreferences.CreateCategory("SuperM60Config");
            Super_M60A3.Config(cfg);
            Super_M60A1.Config(cfg);
            AmmoArmor.Config(cfg);
        }
        public override void OnUpdate()
        {
            Super_M60A3.Update();
        }

        public override void OnSceneWasLoaded(int idx, string scene_name)
        {
            if (scene_name == "MainMenu2_Scene" || scene_name == "LOADER_MENU" || scene_name == "LOADER_INITIAL" || scene_name == "t64_menu" || scene_name == "MainMenu2-1_Scene") return;

            gameManager = GameObject.Find("_APP_GHPC_");
            camManager = gameManager.GetComponent<CameraManager>();
            playerManager = gameManager.GetComponent<PlayerInput>();

            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(GetVics), GameStatePriority.Medium);
            AmmoArmor.Init();
            Super_M60A1.Init();
            Super_M60A3.Init();
        }
    }
}
