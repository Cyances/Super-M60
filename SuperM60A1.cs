using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using SuperM60;
using GHPC.Camera;
using GHPC.Equipment.Optics;
using GHPC.Player;
using GHPC.Vehicle;
using GHPC.Weapons;
using MelonLoader;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using GHPC;
using NWH.VehiclePhysics;
using GHPC.Equipment;
using GHPC.State;
using GHPC.Utility;
using System.Collections;
using GHPC.AI;
using HarmonyLib;
using GHPC.Effects;
using System.Xml.Linq;
using Reticle;


namespace SuperM60
{
    public static class Super_M60A1
    {

        static GameObject tts_flir, tts_gps;

        static string[] gas_valid_ammo = new string[] { "M900A1 APFSDS-T", "M393A3 HEP-T" };

        static MelonPreferences_Entry<bool> useM900A2_m60a1, betterLoader_m60a1, betterCommander_m60a1, betterGunner_m60a1;
        static MelonPreferences_Entry<int> firstammoCount_m60a1, secondammoCount_m60a1, thirdammoCount_m60a1;

        public static GameObject m60a3tts_ui;

        public static void Config(MelonPreferences_Category cfg)
        {
            useM900A2_m60a1 = cfg.CreateEntry<bool>("M60A1 M900A2", false);
            useM900A2_m60a1.Description = "Slightly better penetration, and better ERA defeat";

            firstammoCount_m60a1 = cfg.CreateEntry<int>("M60A1 M900 Round Count", 30);
            firstammoCount_m60a1.Description = "How many rounds per type each upgraded M60A1 should carry. Maximum of 63 rounds total. Bring in at least one primary round.";
            secondammoCount_m60a1 = cfg.CreateEntry<int>("M60A1 M456A3 Round Count", 20);
            thirdammoCount_m60a1 = cfg.CreateEntry<int>("M60A1 M393A3 Round Count", 13);

            betterLoader_m60a1 = cfg.CreateEntry<bool>("M60A1 Better Loader", false);
            betterLoader_m60a1.Description = "M60A1 Crew Proficiency";
            betterCommander_m60a1 = cfg.CreateEntry<bool>("M60A1 Better Commander", false);
            betterGunner_m60a1 = cfg.CreateEntry<bool>("M60A1 Better Gunner", false);
        }

        // fix for GAS reticle
        public static void Update()
        {
            if (SuperM60Mod.gameManager == null) return;

            CameraSlot cam = SuperM60Mod.camManager._currentCamSlot;

            if (cam == null) return;
            if (cam.name != "Aux sight M105D" && cam.name != "Aux sight (GAS)") return;

            AmmoType current_ammo = SuperM60Mod.playerManager.CurrentPlayerWeapon.FCS.CurrentAmmoType;
            if (!gas_valid_ammo.Contains(current_ammo.Name)) return;

            GameObject reticle = cam.transform.GetChild(0).gameObject;

            if (!reticle.activeSelf)
            {
                reticle.SetActive(true);
            }
        }

        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject armour in GameObject.FindGameObjectsWithTag("Penetrable"))
            {
                if (armour == null) continue;

                VariableArmor m60a1VA = armour.GetComponent<VariableArmor>();

                if (m60a1VA == null) continue;
                if (m60a1VA.Unit == null) continue;
                if (m60a1VA.Unit.FriendlyName == "M60A1 RISE (Passive)")
                {
                    if (m60a1VA.Name == "turret")
                    {
                        m60a1VA._armorType = AmmoArmor.armor_codex_composite_turret;
                        MelonLogger.Msg(m60a1VA.ArmorType);
                    }

                    if (m60a1VA.Name == "hull")
                    {
                        m60a1VA._armorType = AmmoArmor.armor_codex_composite_hull;
                        MelonLogger.Msg(m60a1VA.ArmorType);
                    }
                }
            }
            ////UniformArmor pieces
            foreach (GameObject armour in GameObject.FindGameObjectsWithTag("Penetrable"))
            {
                if (armour == null) continue;

                UniformArmor m60a1UA = armour.GetComponent<UniformArmor>();
                if (m60a1UA == null) continue;
                if (m60a1UA.Unit == null) continue;
                if (m60a1UA.Unit.FriendlyName == "M60A1 RISE (Passive)")
                {
                    if (m60a1UA.Name == "return roller")
                    {
                        m60a1UA.PrimaryHeatRha = 50f;
                        m60a1UA.PrimarySabotRha = 50f;
                    }

                    if (m60a1UA.Name == "drive sprocket")
                    {
                        m60a1UA.PrimaryHeatRha = 60f;
                        m60a1UA.PrimarySabotRha = 60f;
                    }

                    if (m60a1UA.Name == "driver hatch")
                    {
                        m60a1UA.PrimaryHeatRha = 80f;
                        m60a1UA.PrimarySabotRha = 80f;
                    }

                    if (m60a1UA.Name == "engine deck")
                    {
                        m60a1UA.PrimaryHeatRha = 80f;
                        m60a1UA.PrimarySabotRha = 80f;
                    }

                    if (m60a1UA.Name == "sponson storage")
                    {
                        m60a1UA.PrimaryHeatRha = 25.4f;
                        m60a1UA.PrimarySabotRha = 25.4f;
                    }

                    if (m60a1UA.Name == "filter box")
                    {
                        m60a1UA.PrimaryHeatRha = 25.4f;
                        m60a1UA.PrimarySabotRha = 25.4f;
                    }

                    if (m60a1UA.Name == "firewall")
                    {
                        m60a1UA.PrimaryHeatRha = 38.1f;
                        m60a1UA.PrimarySabotRha = 38.1f;
                    }

                    if (m60a1UA.Name == "sponson")
                    {
                        m60a1UA.PrimaryHeatRha = 12.7f;
                        m60a1UA.PrimarySabotRha = 12.7f;
                    }

                    if (m60a1UA.Name == "turret ring")
                    {
                        m60a1UA.PrimaryHeatRha = 100f;
                        m60a1UA.PrimarySabotRha = 100f;
                    }

                    if (m60a1UA.Name == "driver's viewport")
                    {
                        m60a1UA.PrimaryHeatRha = 38.1f;
                        m60a1UA.PrimarySabotRha = 38.1f;
                    }
                }
            }

            MelonLogger.Msg("Composite armor loaded");

            foreach (GameObject vic_go in SuperM60Mod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.FriendlyName != "M60A1 RISE (Passive)") continue;
                if (vic.GetComponent<Util.AlreadyConvertedSM60>() != null) continue;

                vic._friendlyName = "M60A1A4";

                WeaponsManager weaponsManager = vic.GetComponent<WeaponsManager>();
                //WeaponSystemInfo mainGunInfo = weaponsManager.Weapons[0];
                //WeaponSystem mainGun = mainGunInfo.Weapon;
                WeaponSystem mainGun = weaponsManager.Weapons[0].Weapon;
                WeaponSystem roofGun = weaponsManager.Weapons[1].Weapon;

                LoadoutManager loadoutManager = vic.GetComponent<LoadoutManager>();
                AmmoType.AmmoClip[] ammo_clip_types = new AmmoType.AmmoClip[] { };
                int total_racks = 5;

                loadoutManager.LoadedAmmoTypes = new AmmoClipCodexScriptable[] { useM900A2_m60a1.Value ? AmmoArmor.clip_codex_m900a2 : AmmoArmor.clip_codex_m900a1, AmmoArmor.clip_codex_m456a3, AmmoArmor.clip_codex_m393a3 };
                //ammo_clip_types = new AmmoType.AmmoClip[] { ap.ClipType, clip_codex_m456a3.ClipType, clip_m394 };
                ammo_clip_types = new AmmoType.AmmoClip[] { useM900A2_m60a1.Value ? AmmoArmor.clip_m900a2 : AmmoArmor.clip_m900a1, AmmoArmor.clip_m456a3, AmmoArmor.clip_m393a3 };

                loadoutManager.TotalAmmoCounts = new int[] { firstammoCount_m60a1.Value, secondammoCount_m60a1.Value, thirdammoCount_m60a1.Value };
                FieldInfo total_ammo_types = typeof(LoadoutManager).GetField("_totalAmmoTypes", BindingFlags.NonPublic | BindingFlags.Instance);
                total_ammo_types.SetValue(loadoutManager, 3);


                for (int i = 0; i < total_racks; i++)
                {
                    GHPC.Weapons.AmmoRack rack = loadoutManager.RackLoadouts[i].Rack;
                    rack.ClipTypes = ammo_clip_types;
                    Util.EmptyRack(rack);
                }

                loadoutManager.SpawnCurrentLoadout();

                PropertyInfo roundInBreech = typeof(AmmoFeed).GetProperty("AmmoTypeInBreech");
                roundInBreech.SetValue(mainGun.Feed, null);

                MethodInfo refreshBreech = typeof(AmmoFeed).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
                refreshBreech.Invoke(mainGun.Feed, new object[] { });

                MethodInfo registerAllBallistics = typeof(LoadoutManager).GetMethod("RegisterAllBallistics", BindingFlags.Instance | BindingFlags.NonPublic);
                registerAllBallistics.Invoke(loadoutManager, new object[] { });


                WeaponSystemInfo coaxGunInfo = weaponsManager.Weapons[1];
                WeaponSystem coaxGun = coaxGunInfo.Weapon;


                coaxGunInfo.Name = "M2HB RWS";
                //coaxGun.WeaponSound.LoopEventPath = null;
                //coaxGun.WeaponSound.LoopEvent.eventBuffer

                //coaxGun.WeaponSound.SingleShotByDefault = true;
                //coaxGun.WeaponSound.SingleShotEventPaths[0] = "event:/Weapons/MG_m85_400rmp";
                coaxGun.SetCycleTime(0.1f);
                coaxGun.BaseDeviationAngle = 0.025f;// 0.05

                coaxGun.Feed.AmmoTypeInBreech = null;
                coaxGun.Feed._totalCycleTime = 0.1f;
                coaxGun.Feed.ReadyRack.ClipTypes[0] = AmmoArmor.clip_m8api;
                coaxGun.Feed.ReadyRack.Awake();
                coaxGun.Feed.Start();

                /*if (!ReticleMesh.cachedReticles.ContainsKey("M1A1_generic") || m60a3tts_ui == null)
                {
                    foreach (Vehicle obj in Resources.FindObjectsOfTypeAll(typeof(Vehicle)))
                    {
                        if (obj.gameObject.name == "M60A3")
                        {
                            //USSR Vehicles/T80B/   T80B_rig/ HULL/TURRET/          gun/---MAIN GUN SCRIPTS---/2A46-2/1G42 gunner's sight/GPS/1G42 Canvas/
                            //USSR Vehicles/T80B/   T80B_rig/ HULL/TURRET/          gun/---MAIN GUN SCRIPTS---/2A46-2/1G42 gunner's sight/GPS/Reticle Mesh/
                            //US Vehicles/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/               Sights/   GPS/camera/
                            //US Vehicles/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/               Sights   /GPS/Reticle Mesh/
                            //US Vehicles/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR/Reticle Mesh/

                            //USSR Vehicles/T64A/---T64A_MESH---/HULL/TURRET/Main gun/---MAIN GUN SCRIPTS---/2A46/TPD-2-49 gunner's sight/
                            //US Vehicles/M60A1/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/
                            //0.645 2.6451 0.8761
                            m60a3tts_ui = GameObject.Instantiate(obj.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS/camera").gameObject);
                            m60a3tts_ui.SetActive(false);
                            obj.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS/Reticle Mesh").GetComponent<ReticleMesh>().Load();
                            obj.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR/Reticle Mesh").GetComponent<ReticleMesh>().Load();
                            break;
                        }
                    }
                }


                FireControlSystem fcs = vic.GetComponentInChildren<FireControlSystem>();
                UsableOptic day_optic = Util.GetDayOptic(fcs);
                UsableOptic night_optic = day_optic.slot.LinkedNightSight.PairedOptic;

                Transform tts = vic.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights");
                tts.localPosition = new Vector3(0.645f, 2.6451f, 0.8761f);
                GameObject hud = GameObject.Instantiate(m60a3tts_ui, tts.Find("GPS"));
                hud.SetActive(true);
                //hud.transform.Find("GameObject/ammo text APFSDS (TMP)").gameObject.SetActive(false);



                fcs._autoModeOnLase = true;
                fcs._horizontalLeadOnly = true;
                fcs._manualModeOnRangeSet = true;
                fcs._originalMainOpticAlign = OpticAlignment.BoresightStabilized;
                fcs._originalSuperelevationMode = true;
                fcs._originalSuperleadMode = true;
                fcs.AutoModeTriggers = new GHPC.Equipment.FcsManualModeCancelTrigger[] { GHPC.Equipment.FcsManualModeCancelTrigger.Lase };
                fcs.DynamicLead = true;
                fcs.FireGateAngle = 0.1f;
                fcs.ForceReticleToZeroRange = true;
                fcs.SuperelevateFireGating = true;
                fcs.HasManualMode = true;
                fcs.IgnoreHorizontalForFireGating = true;
                fcs.LaserAim = LaserAimMode.ImpactPoint;
                fcs.MaxLaserRange = 4000f;
                fcs.ManualModeLead = GHPC.Equipment.FcsManualModeLead.DisableLeadCompensation;
                fcs.ManualModeSuperelevation = GHPC.Equipment.FcsManualModeSuperelevation.DisableSuperelevation;
                fcs.RecordTraverseRateBuffer = true;
                fcs.SuperelevateWeapon = true;
                fcs.SuperleadWeapon = true;
                fcs.TraverseBufferSeconds = 1f;
                fcs.ZeroReticleRangeInAutoMode = true;
                fcs.ManualModeOpticAlign = OpticAlignment.BoresightStabilized;

                GameObject.Destroy(fcs.OpticalRangefinder.gameObject);
                day_optic.slot.ExclusiveItems = new GameObject[] { };*/


                /*Transform tts_Optics = vic.transform.Find("M60A3TTS_rig/hull/turret");
                GameObject.Instantiate(tts_gps, tts_Optics.transform.Find("tts sight cover"));
                GameObject.Instantiate(tts_flir, tts_Optics.transform.Find("tts sight cover"));*/


                // Better Thermals
                //BluFor/1stPltn/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS/
                //BluFor/1stPltn/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR/
                //BluFor/1stPltn/M60A3/M60A3TTS_rig/hull/turret/main gun mantlet/Gun Scripts/Aux sight M105D/
                //US Vehicles/M60A3/M60A3TTS_rig/hull/turret/cupola/cupola mantlet/M85 gunsight/ //4.5 def
                var gpsOptic = vic_go.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS/").gameObject.transform;
                //var flirOptic = vic_go.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR/").gameObject.transform;
                var gasOptic = vic_go.transform.Find("M60A3TTS_rig/hull/turret/main gun mantlet/Gun Scripts/Aux sight M105D/").gameObject.transform;
                var roofOptic = vic_go.transform.Find("M60A3TTS_rig/hull/turret/cupola/cupola mantlet/M85 gunsight/").gameObject.transform;

                UsableOptic horizontalGps = gpsOptic.GetComponent<UsableOptic>();
                //UsableOptic horizontalFlir = flirOptic.GetComponent<UsableOptic>();
                UsableOptic roofUO = roofOptic.GetComponent<UsableOptic>();

                CameraSlot daysightPlus = gpsOptic.GetComponent<CameraSlot>();
                //CameraSlot flirPlus = flirOptic.GetComponent<CameraSlot>();
                CameraSlot gasPlus = gasOptic.GetComponent<CameraSlot>();
                CameraSlot roofCS = roofOptic.GetComponent<CameraSlot>();


                horizontalGps.RotateAzimuth = true;
                //horizontalFlir.RotateAzimuth = true;

                List<float> fovs = new List<float>();
                for (float i = 14.5f; i >= 1f; i -= 1.5f)
                {
                    fovs.Add(i);
                }

                daysightPlus.DefaultFov = 4.5f;//4.2
                daysightPlus.OtherFovs = new float[] { 2, 1 };//fovs.ToArray<float>();
                daysightPlus.BaseBlur = 0;
                daysightPlus.VibrationBlurScale = 0;
                daysightPlus.VibrationShakeMultiplier = 0.2f;//1.2

                /*flirPlus.DefaultFov = 16;//8.4
                flirPlus.OtherFovs = fovs.ToArray<float>();//2.8
                flirPlus.BaseBlur = 0;
                flirPlus.VibrationBlurScale = 0;
                flirPlus.VibrationShakeMultiplier = 0.2f;//1.2
                GameObject.Destroy(flirOptic.transform.Find("Canvas Scanlines").gameObject);*/

                gasPlus.DefaultFov = 6.5f;//4.5f
                gasPlus.OtherFovs = new float[] { 4.5f, 2.5f, 1.5f };
                gasPlus.VibrationBlurScale = 0.1f;//0.2
                gasPlus.VibrationShakeMultiplier = 0.2f;//0.5

                mainGun.FCS.ComputerNeedsPower = false;
                //mainGun.FCS.CurrentStabMode = StabilizationMode.WorldPoint;//WorldPoint

                VehicleController vicVC = vic_go.GetComponent<VehicleController>();
                NwhChassis vicNwhC = vic_go.GetComponent<NwhChassis>();
                UnitAI vicUAI = vic.GetComponentInChildren<UnitAI>();
                DriverAIController vicDAIC = vic.GetComponent<DriverAIController>();

                vicDAIC.maxSpeed = 32;//20

                vicVC.engine.maxPower = 935f;//559;
                vicVC.engine.maxRPM = 4500f;//2750 ;
                vicVC.engine.maxRpmChange = 3000f;//2000;

                vicVC.brakes.maxTorque = 121920;//121920

                vicNwhC._maxForwardSpeed = 22f;//13.4
                vicNwhC._maxReverseSpeed = 8f;//4.47


                List<float> fwGears = new List<float>();
                fwGears.Add(6.28f);//5
                fwGears.Add(4.81f);//1.7
                fwGears.Add(2.98f);
                fwGears.Add(1.76f);
                fwGears.Add(1.36f);
                fwGears.Add(1.16f);


                /*List<float> fwGears2 = new List<float>();//CVT test
                for (float i = 6.5f; i >= 1f; i -= 0.1f)
                {
                    fwGears2.Add(i);
                }*/

                List<float> rvGears = new List<float>();
                rvGears.Add(-2.76f);//-8
                rvGears.Add(-8.28f);

                List<float> Gears = new List<float>();
                Gears.Add(-2.76f);
                //Gears.Add(-2.98f);
                Gears.Add(-8.28f);
                Gears.Add(0f);
                Gears.Add(6.28f);
                Gears.Add(4.81f);
                Gears.Add(2.98f);
                Gears.Add(1.76f);
                Gears.Add(1.36f);
                Gears.Add(1.16f);

                vicVC.transmission.forwardGears = fwGears;//
                vicVC.transmission.gearMultiplier = 10.16f;//10.16
                vicVC.transmission.gears = Gears;//
                vicVC.transmission.reverseGears = rvGears;//
                vicVC.transmission.shiftDuration = 0.01f;//0.01
                vicVC.transmission.shiftDurationRandomness = 0f;//0
                vicVC.transmission.shiftPointRandomness = 0.05f;//0.05


                for (int i = 0; i < 12; i++)
                {
                    //m3a2Vc.wheels[i].wheelController.damper.force = 2.3036f;//2.3036
                    vicVC.wheels[i].wheelController.damper.maxForce = 6500;//6500
                    vicVC.wheels[i].wheelController.damper.unitBumpForce = 6500;//6500
                    vicVC.wheels[i].wheelController.damper.unitReboundForce = 9000;//9000

                    //m3a2Vc.wheels[i].wheelController.spring.bottomOutForce = 0f;//0
                    //m60a3Vc.wheels[i].wheelController.spring.force = 24079.51f;//24079.51
                    vicVC.wheels[i].wheelController.spring.length = 0.32f;//0.2809
                    vicVC.wheels[i].wheelController.spring.maxForce = 240000;//240000
                    vicVC.wheels[i].wheelController.spring.maxLength = 0.52f;//0.48

                    vicVC.wheels[i].wheelController.fFriction.forceCoefficient = 1.25f;//1.2
                    vicVC.wheels[i].wheelController.fFriction.slipCoefficient = 1f;//1

                    vicVC.wheels[i].wheelController.sFriction.forceCoefficient = 0.85f;//0.8
                    vicVC.wheels[i].wheelController.sFriction.slipCoefficient = 1f;//1 
                }

                //0 hmg az 1 hmg elev 2 gun eleve 3 gun azi
                vic.AimablePlatforms[0].SpeedPowered = 60;//20
                vic.AimablePlatforms[0].SpeedUnpowered = 60;//20
                vic.AimablePlatforms[0]._stabActive = true;
                vic.AimablePlatforms[0].StabilizerActive = true;
                vic.AimablePlatforms[0]._stabMode = StabilizationMode.Vector;
                vic.AimablePlatforms[1].SpeedPowered = 40;//20
                vic.AimablePlatforms[1].SpeedUnpowered = 40;//20
                vic.AimablePlatforms[1]._stabActive = true;
                vic.AimablePlatforms[1].StabilizerActive = true;
                vic.AimablePlatforms[1]._stabMode = StabilizationMode.Vector;
                vic.AimablePlatforms[1].LocalEulerLimits = new Vector2(-17, 65);//-15 60


                vic.AimablePlatforms[2].SpeedPowered = 40;//30
                vic.AimablePlatforms[2].SpeedUnpowered = 20;//5
                vic.AimablePlatforms[2]._stabMode = StabilizationMode.WorldPoint;//WorldPoint
                vic.AimablePlatforms[3].SpeedPowered = 40;//22.5
                vic.AimablePlatforms[3].SpeedUnpowered = 20;//5
                vic.AimablePlatforms[3]._stabMode = StabilizationMode.WorldPoint;//WorldPoint

                roofGun.FCS._fixParallaxForVectorMode = true;
                roofGun.FCS.SuperelevateWeapon = true;
                roofGun.FCS.SuperleadWeapon = true;
                roofGun.FCS.LaserAim = LaserAimMode.ImpactPoint;
                roofGun.FCS.MaxLaserRange = 4000;
                roofGun.FCS.DefaultRange = 600;
                roofGun.FCS.RegisteredRangeLimits = new Vector2(100, 4000);
                roofGun.FCS.MarkRangeInvalidBelow = 100;
                roofGun.FCS.CurrentStabMode = StabilizationMode.Vector;
                roofGun.FCS.StabsActive = true;

                roofUO.Alignment = OpticAlignment.FcsRange;
                roofUO.ForceHorizontalReticleAlign = true;
                roofUO.RotateElevation = true;
                roofUO.RotateAzimuth = true;

                roofCS.DefaultFov = 25f;
                roofCS.OtherFovs = new float[] { 16.5f, 6.5f, 3.472f, 1.204f };
                roofCS.VibrationBlurScale = 0;
                roofCS.VibrationShakeMultiplier = 0;

                if (betterLoader_m60a1.Value)
                {
                    //modify rack loadout speed
                    mainGun.Feed._totalReloadTime = 4.75f;
                    mainGun.Feed.SlowReloadMultiplier = 4f;

                    loadoutManager.RackLoadouts[0].Rack._retrievalDelaySeconds = 3;//4
                    loadoutManager.RackLoadouts[0].Rack._storageDelaySeconds = 1.5f;//2
                    loadoutManager.RackLoadouts[1].Rack._retrievalDelaySeconds = 3;//4
                    loadoutManager.RackLoadouts[1].Rack._storageDelaySeconds = 1.5f;//2
                    loadoutManager.RackLoadouts[2].Rack._retrievalDelaySeconds = 3;//4
                    loadoutManager.RackLoadouts[2].Rack._storageDelaySeconds = 1.5f;//2
                    loadoutManager.RackLoadouts[3].Rack._retrievalDelaySeconds = 4;//5.5
                    loadoutManager.RackLoadouts[3].Rack._storageDelaySeconds = 1.5f;//2
                    loadoutManager.RackLoadouts[4].Rack._retrievalDelaySeconds = 4;//6
                    loadoutManager.RackLoadouts[4].Rack._storageDelaySeconds = 1.5f;//2
                }

                if (betterCommander_m60a1.Value)
                {
                    vicUAI.SpotTimeMaxDistance = 3500;
                    vicUAI.TargetSensor._spotTimeMax = 3;
                    vicUAI.TargetSensor._spotTimeMaxDistance = 500;
                    vicUAI.TargetSensor._spotTimeMaxVelocity = 7f;
                    vicUAI.TargetSensor._spotTimeMin = 1;
                    vicUAI.TargetSensor._spotTimeMinDistance = 50;
                    vicUAI.TargetSensor._targetCooldownTime = 1.5f;

                    vicUAI.CommanderAI._identifyTargetDurationRange = new Vector2(1.5f, 2.5f);
                    vicUAI.CommanderAI._sweepCommsCheckDuration = 4;
                }

                if (betterGunner_m60a1.Value)
                {
                    vicUAI.combatSpeedLimit = 25;
                    vicUAI.firingSpeedLimit = 20;

                    vicUAI.AccuracyModifiers.Angle.MaxDistance = 2500;
                    vicUAI.AccuracyModifiers.Angle.MaxRadius = 2.5f;
                    vicUAI.AccuracyModifiers.Angle.MinRadius = 1.5f;
                    vicUAI.AccuracyModifiers.Angle.IncreaseAccuracyPerShot = false;
                }

            }

            yield break;
        }

        public static void Init()
        {

            //BluFor/1stPltn/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS/
            //BluFor/1stPltn/M60A3/M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR/

            /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Vehicle"))
            {
                if (obj.gameObject.name == "M60A3")
                {
                    tts_gps = obj.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/GPS").gameObject;
                    tts_flir = obj.transform.Find("M60A3TTS_rig/hull/turret/Turret Scripts/Sights/FLIR").gameObject;
                }
            }*/

            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Lowest);
        }
    }
}