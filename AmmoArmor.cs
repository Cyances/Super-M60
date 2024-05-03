using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.Effects;
using GHPC.Equipment;
using GHPC.Weapons;
using MelonLoader;
using UnityEngine;

namespace SuperM60
{
    public class AmmoArmor
    {
        public static AmmoClipCodexScriptable clip_codex_m900a1;
        public static AmmoType.AmmoClip clip_m900a1;
        public static AmmoCodexScriptable ammo_codex_m900a1;
        public static AmmoType ammo_m900a1;


        public static AmmoClipCodexScriptable clip_codex_m900a2;
        public static AmmoType.AmmoClip clip_m900a2;
        public static AmmoCodexScriptable ammo_codex_m900a2;
        public static AmmoType ammo_m900a2;

        public static AmmoClipCodexScriptable clip_codex_m393a3;
        public static AmmoType.AmmoClip clip_m393a3;
        public static AmmoCodexScriptable ammo_codex_m393a3;
        public static AmmoType ammo_m393a3;

        public static AmmoClipCodexScriptable clip_codex_m456a3;
        public static AmmoType.AmmoClip clip_m456a3;
        public static AmmoCodexScriptable ammo_codex_m456a3;
        public static AmmoType ammo_m456a3;

        public static AmmoType ammo_m833, ammo_m456;

        public static GameObject ammo_m900a1_vis = null;
        public static GameObject ammo_m900a2_vis = null;
        public static GameObject ammo_m393_vis = null;
        public static GameObject ammo_m456a3_vis = null;

        public static ArmorType armor_castarmorsteel_vnl;

        public static ArmorType armor_composite_turret;
        public static ArmorCodexScriptable armor_codex_composite_turret;

        public static ArmorType armor_composite_hull;
        public static ArmorCodexScriptable armor_codex_composite_hull;


        public static AmmoClipCodexScriptable clip_codex_m8api;
        public static AmmoType.AmmoClip clip_m8api;
        public static AmmoCodexScriptable ammo_codex_m8api;
        public static AmmoType ammo_m8api;

        public static AmmoClipCodexScriptable clip_codex_m2apt;
        public static AmmoType.AmmoClip clip_m2apt;
        public static AmmoCodexScriptable ammo_codex_m2apt;
        public static AmmoType ammo_m2apt;

        public static AmmoType ammo_m8vnl;

        public static MelonPreferences_Entry<int> hepFragments;
        public static void Config(MelonPreferences_Category cfg)
        {
            hepFragments = cfg.CreateEntry<int>("HEP Fragments", 600);
            hepFragments.Description = "How many fragments are generated when the below round explodes. NOTE: Higher number, means higher performance hit. Be careful in using higher number.";
        }

        public static void Init()
        {
            if (ammo_m393a3 == null)
            {
                foreach (AmmoCodexScriptable s in Resources.FindObjectsOfTypeAll(typeof(AmmoCodexScriptable)))
                {
                    if (s.AmmoType.Name == "M833 APFSDS-T") ammo_m833 = s.AmmoType;
                    if (s.AmmoType.Name == "M456 HEAT-FS-T") ammo_m456 = s.AmmoType;
                }

                var era_optimizations_m456a3 = new List<AmmoType.ArmorOptimization>() { };
                var era_optimizations_m900a2 = new List<AmmoType.ArmorOptimization>() { };

                string[] era_names = new string[] {
                    "kontakt-1 armour",
                    "kontakt-5 armour",
                    "ARAT-1 Armor Codex",
                    "BRAT-M3 Armor Codex",
                    "BRAT-M5 Armor Codex",
                };

                foreach (ArmorCodexScriptable s in Resources.FindObjectsOfTypeAll<ArmorCodexScriptable>())
                {
                    if (era_names.Contains(s.name))
                    {
                        AmmoType.ArmorOptimization optimization_m456a3 = new AmmoType.ArmorOptimization();
                        optimization_m456a3.Armor = s;
                        optimization_m456a3.RhaRatio = 0.25f;
                        era_optimizations_m456a3.Add(optimization_m456a3);

                        AmmoType.ArmorOptimization optimization_m900a2 = new AmmoType.ArmorOptimization();
                        optimization_m900a2.Armor = s;
                        optimization_m900a2.RhaRatio = 0.35f;
                        era_optimizations_m900a2.Add(optimization_m900a2);
                    }

                    if (era_optimizations_m456a3.Count == era_names.Length) break;
                }


                foreach (ArmorCodexScriptable s in Resources.FindObjectsOfTypeAll(typeof(ArmorCodexScriptable)))
                {
                    if (s.ArmorType.Name == "cast armor steel") armor_castarmorsteel_vnl = s.ArmorType;
                }

                // m900a1
                ammo_m900a1 = new AmmoType();
                Util.ShallowCopy(ammo_m900a1, ammo_m833);
                ammo_m900a1.Name = "M900A1 APFSDS-T";
                ammo_m900a1.RhaPenetration = 550f;
                ammo_m900a1.MuzzleVelocity = 1505f;
                ammo_m900a1.Mass = 4.2f;
                ammo_m900a1.SpallMultiplier = 1.35f;
                ammo_m900a1.MinSpallRha = 3f;
                ammo_m900a1.MaxSpallRha = 15f;
                ammo_m900a1.Coeff = 0.07f;//0.08
                ammo_m900a1.CertainRicochetAngle = 9;

                ammo_codex_m900a1 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m900a1.AmmoType = ammo_m900a1;
                ammo_codex_m900a1.name = "ammo_m900a1";

                clip_m900a1 = new AmmoType.AmmoClip();
                clip_m900a1.Capacity = 1;
                clip_m900a1.Name = "M900A1 APFSDS-T";
                clip_m900a1.MinimalPattern = new AmmoCodexScriptable[1];
                clip_m900a1.MinimalPattern[0] = ammo_codex_m900a1;

                clip_codex_m900a1 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m900a1.name = "clip_m900a1";
                clip_codex_m900a1.ClipType = clip_m900a1;

                // m900a2
                ammo_m900a2 = new AmmoType();
                Util.ShallowCopy(ammo_m900a2, ammo_m833);
                ammo_m900a2.Name = "M900A2 APFSDS-T";
                ammo_m900a2.RhaPenetration = 580f;
                ammo_m900a2.MuzzleVelocity = 1600f;
                ammo_m900a2.Mass = 4.5f;
                ammo_m900a2.SpallMultiplier = 1.5f;
                ammo_m900a2.MinSpallRha = 3f;
                ammo_m900a2.MaxSpallRha = 24f;
                ammo_m900a2.CertainRicochetAngle = 9;
                ammo_m900a2.ArmorOptimizations = era_optimizations_m900a2.ToArray<AmmoType.ArmorOptimization>();

                ammo_codex_m900a2 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m900a2.AmmoType = ammo_m900a2;
                ammo_codex_m900a2.name = "ammo_m900a2";

                clip_m900a2 = new AmmoType.AmmoClip();
                clip_m900a2.Capacity = 1;
                clip_m900a2.Name = "M900A2 APFSDS-T";
                clip_m900a2.MinimalPattern = new AmmoCodexScriptable[1];
                clip_m900a2.MinimalPattern[0] = ammo_codex_m900a2;

                clip_codex_m900a2 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m900a2.name = "clip_m900a2";
                clip_codex_m900a2.ClipType = clip_m900a2;

                // m456a3 
                ammo_m456a3 = new AmmoType();
                Util.ShallowCopy(ammo_m456a3, ammo_m456);
                ammo_m456a3.Name = "M456A3 HEAT-FS-T";
                ammo_m456a3.RhaPenetration = 450f;
                ammo_m456a3.MuzzleVelocity = 1174f;
                ammo_m456a3.Mass = 10.2f;
                ammo_m456a3.TntEquivalentKg = 3.29f;
                ammo_m456a3.CertainRicochetAngle = 3.0f;
                ammo_m456a3.MinSpallRha = 3f;
                ammo_m456a3.MaxSpallRha = 21f;
                ammo_m456a3.ShatterOnRicochet = false;
                ammo_m456a3.SpallMultiplier = 2;
                ammo_m456a3.DetonateSpallCount = 150;
                ammo_m456a3.ForcedSpallAngle = 0;
                ammo_m456a3.Coeff = 0.16f;//0.19
                ammo_m456a3.ArmorOptimizations = era_optimizations_m456a3.ToArray<AmmoType.ArmorOptimization>();

                ammo_codex_m456a3 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m456a3.AmmoType = ammo_m456a3;
                ammo_codex_m456a3.name = "ammo_m456a3";

                clip_m456a3 = new AmmoType.AmmoClip();
                clip_m456a3.Capacity = 1;
                clip_m456a3.Name = "M456A3 HEAT-FS-T";
                clip_m456a3.MinimalPattern = new AmmoCodexScriptable[1];
                clip_m456a3.MinimalPattern[0] = ammo_codex_m456a3;

                clip_codex_m456a3 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m456a3.name = "clip_m456a3";
                clip_codex_m456a3.ClipType = clip_m456a3;

                // m393a3
                ammo_m393a3 = new AmmoType();
                Util.ShallowCopy(ammo_m393a3, ammo_m456);
                ammo_m393a3.Name = "M393A3 HEP-T";
                ammo_m393a3.RhaPenetration = 50f;
                ammo_m393a3.MuzzleVelocity = 750f;//1099
                ammo_m393a3.Mass = 11.3f;
                ammo_m393a3.TntEquivalentKg = 5.26f;
                ammo_m393a3.CertainRicochetAngle = 5;
                ammo_m393a3.MinSpallRha = 3f;
                ammo_m393a3.MaxSpallRha = 60f;
                ammo_m393a3.Coeff = 0.26f;
                ammo_m393a3.Category = AmmoType.AmmoCategory.Explosive;
                ammo_m393a3.ShatterOnRicochet = false;
                //ammo_m394.ImpactFuseTime = 0.005f;
                ammo_m393a3.SpallMultiplier = 2;
                ammo_m393a3.DetonateSpallCount = hepFragments.Value;
                ammo_m393a3.ForcedSpallAngle = 0;
                ammo_m393a3.ImpactTypeFuzed = ParticleEffectsManager.EffectVisualType.MainGunImpactHighExplosive;
                ammo_m393a3.ImpactTypeFuzedTerrain = ParticleEffectsManager.EffectVisualType.MainGunImpactExplosiveTerrain;
                ammo_m393a3.ImpactTypeUnfuzed = ParticleEffectsManager.EffectVisualType.MainGunImpactHighExplosive;
                ammo_m393a3.ImpactTypeUnfuzedTerrain = ParticleEffectsManager.EffectVisualType.MainGunImpactExplosiveTerrain;
                ammo_m393a3.ShortName = AmmoType.AmmoShortName.He;
                ammo_m393a3.Coeff = 0.16f;//19
                ammo_m393a3.RhaToFuse = 15f;

                ammo_codex_m393a3 = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m393a3.AmmoType = ammo_m393a3;
                ammo_codex_m393a3.name = "ammo_m393a3";

                clip_m393a3 = new AmmoType.AmmoClip();
                clip_m393a3.Capacity = 1;
                clip_m393a3.Name = "M393A3 HEP-T";
                clip_m393a3.MinimalPattern = new AmmoCodexScriptable[1];
                clip_m393a3.MinimalPattern[0] = ammo_codex_m393a3;

                clip_codex_m393a3 = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m393a3.name = "clip_m393a3";
                clip_codex_m393a3.ClipType = clip_m393a3;


                foreach (AmmoCodexScriptable s in Resources.FindObjectsOfTypeAll(typeof(AmmoCodexScriptable)))
                {
                    if (s.AmmoType.Name == "M8 API") ammo_m8vnl = s.AmmoType;
                }

                foreach (AmmoClipCodexScriptable s in Resources.FindObjectsOfTypeAll(typeof(AmmoClipCodexScriptable)))
                {
                    if (clip_codex_m2apt != null) break;
                }

                //m2
                ammo_m2apt = new AmmoType();
                Util.ShallowCopy(ammo_m2apt, ammo_m8vnl);
                ammo_m2apt.CertainRicochetAngle = 15f;//5f;
                ammo_m2apt.MaxSpallRha = 8f;
                ammo_m2apt.MinSpallRha = 2f;
                ammo_m2apt.MuzzleVelocity = 887;
                ammo_m2apt.Name = "12.7x99mm M2 AP-T";
                ammo_m2apt.NutationPenaltyDistance = 0f;
                ammo_m2apt.MaxNutationPenalty = 0f;
                ammo_m2apt.RhaPenetration = 29f;
                ammo_m2apt.SpallMultiplier = 10f;
                ammo_m2apt.UseTracer = true;

                ammo_codex_m2apt = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m2apt.AmmoType = ammo_m2apt;
                ammo_codex_m2apt.name = "ammo_m2apt";

                clip_m2apt = new AmmoType.AmmoClip();
                clip_m2apt.Capacity = 300;
                clip_m2apt.Name = "M2 AP-T";
                clip_m2apt.MinimalPattern = new AmmoCodexScriptable[1];
                clip_m2apt.MinimalPattern[0] = ammo_codex_m2apt;

                clip_codex_m2apt = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m2apt.name = "clip_m2apt";
                clip_codex_m2apt.ClipType = clip_m2apt;

                //m8
                ammo_m8api = new AmmoType();
                Util.ShallowCopy(ammo_m8api, ammo_m8vnl);
                ammo_m8api.CertainRicochetAngle = 15f;//5f;
                ammo_m8api.MaxSpallRha = 8f;
                ammo_m8api.MinSpallRha = 2f;
                ammo_m8api.MuzzleVelocity = 887;
                ammo_m8api.Name = "12.7x99mm M8 AP-I";
                ammo_m8api.NutationPenaltyDistance = 0f;
                ammo_m8api.MaxNutationPenalty = 0f;
                ammo_m8api.RhaPenetration = 29f;
                ammo_m8api.SpallMultiplier = 20f;
                ammo_m8api.UseTracer = true;

                ammo_codex_m8api = ScriptableObject.CreateInstance<AmmoCodexScriptable>();
                ammo_codex_m8api.AmmoType = ammo_m8api;
                ammo_codex_m8api.name = "ammo_m8api";

                clip_m8api = new AmmoType.AmmoClip();
                clip_m8api.Capacity = 300;
                clip_m8api.Name = "M8 AP-I/T Mix";
                clip_m8api.MinimalPattern = new AmmoCodexScriptable[]
                    {
                        ammo_codex_m2apt,
                        ammo_codex_m8api,
                        ammo_codex_m8api,
                    };
                clip_m8api.MinimalPattern[0] = ammo_codex_m8api;

                clip_codex_m8api = ScriptableObject.CreateInstance<AmmoClipCodexScriptable>();
                clip_codex_m8api.name = "clip_m8api";
                clip_codex_m8api.ClipType = clip_m8api;

                ////Visual models
                ammo_m900a1_vis = GameObject.Instantiate(ammo_m833.VisualModel);
                ammo_m900a1_vis.name = "M900A1 visual";
                ammo_m900a1.VisualModel = ammo_m900a1_vis;
                ammo_m900a1.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_m900a1;
                ammo_m900a1.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_m900a1;

                ammo_m900a2_vis = GameObject.Instantiate(ammo_m833.VisualModel);
                ammo_m900a2_vis.name = "M900A2 visual";
                ammo_m900a2.VisualModel = ammo_m900a2_vis;
                ammo_m900a2.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_m900a2;
                ammo_m900a2.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_m900a2;

                ammo_m456a3_vis = GameObject.Instantiate(ammo_m456.VisualModel);
                ammo_m456a3_vis.name = "M456A3 visual";
                ammo_m456a3.VisualModel = ammo_m456a3_vis;
                ammo_m456a3.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_m456a3;
                ammo_m456a3.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_m456a3;

                ammo_m393_vis = GameObject.Instantiate(ammo_m456.VisualModel);
                ammo_m393_vis.name = "M393A3 visual";
                ammo_m393a3.VisualModel = ammo_m393_vis;
                ammo_m393a3.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_m393a3;
                ammo_m393a3.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_m393a3;


                armor_composite_turret = new ArmorType();
                Util.ShallowCopy(armor_composite_turret, armor_castarmorsteel_vnl);
                armor_composite_turret.RhaeMultiplierCe = 1.8f; //0.95
                armor_composite_turret.RhaeMultiplierKe = 1.6f; //0.95
                armor_composite_turret.Name = "m60 composite turret";

                armor_codex_composite_turret = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
                armor_codex_composite_turret.name = "m60 composite turret codex";
                armor_codex_composite_turret.ArmorType = armor_composite_turret;

                armor_composite_hull = new ArmorType();
                Util.ShallowCopy(armor_composite_hull, armor_castarmorsteel_vnl);
                armor_composite_hull.RhaeMultiplierCe = 1.8f; //0.95
                armor_composite_hull.RhaeMultiplierKe = 1.6f; //0.95
                armor_composite_hull.Name = "m60 composite hull";

                armor_codex_composite_hull = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
                armor_codex_composite_hull.name = "m60 composite hull codex";
                armor_codex_composite_hull.ArmorType = armor_composite_hull;
            }
        }
    }
}
