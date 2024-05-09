# Super M60 v0.2

Pretty much WIP since I'm not sure about the exact goal of this mod

- Modifies both M60A1 and M60A3 to hypothetical M60A4 upgrade
- M60A3 has improved GPS and FLIR
- M60A1 still has a manual rangefinder but improved optics
- Cupola HMG has LRF*, stabilization, better ammo and optics.
- Better ammo 
- Better armor (+60% vs KE and +80% vs CE)
- Better vehicle dynamics (1250 HP engine, improved transmission and suspension)
- Optional better crew (Loader, Gunner and Commander)
- Limited mod configuration (MelonPreferences.cfg)

## Round types list:
| Name  | Penetration (mm) | Fragment/Spalling Penetration (mm)| Muzzle Velocity (m/s) | Note |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| M900A1 APFSDS-T | 550 | - | 1505 | +35% spalling chance and +15% spalling performance |
| M900A2 APFSDS-T*** | 580 | - | 1600 | Hypothetical round. +50% spalling chance and +100% spalling performance. ERA is only 35% effective. |
| M456A3 HEAT-FS-T*** | 450 | - | 1174 | Hypothetical round. +100% spalling chance. ERA is only 25% effective. |
| M393A3 HEP-T*** | 50* | 60** | 750 |  |
| M2/M8 AP-T/I | 29 | - | 887 | Cupola HMG |


<p>
	<ul> 
		<li>Values for Anti-ERA effects are a total guess</li>
		<li>*These are HE rounds so actual penetration is not actually one listed but the potential</li>
		<li>**These are <i>up to</i> values so not every fragment will perform the same</li>
		<li>***These have slightly less drag</li>
	</ul>
</p>

## KNOWN ISSUES / NOTE
- Cupola HMG LRF only works for M60A3
- Cupola HMG camera works properly once it has been ranged (manually or via LRF) at 100 meters or more
- Armor improvement for the M60 applies to the entire turret and hull due to how the vanilla models are created
