﻿using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using APIPlugin;


namespace AraCardExpansion
{
	public partial class Plugin
	{
		private NewAbility CreateGodabilitywolf()
		{
			AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
			info.powerLevel = 5;
			info.rulebookName = "Call of the Gods[Canine]";
			info.rulebookDescription = "When this Card is played, summon  the Canine God in your Hand.";
			info.metaCategories = new List<AbilityMetaCategory> { AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular };

			byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("AraCardExpansion.dll", ""), "Artwork/spawngod.png"));
			Texture2D tex = new Texture2D(2, 2);
			tex.LoadImage(imgBytes);

			NewAbility godabilitywolf = new NewAbility(info, typeof(Godabilitywolf), tex);
			Godabilitywolf.ability = godabilitywolf.ability;
			return godabilitywolf;
		}
	}

	public class Godabilitywolf : DrawCreatedCard
	{

		public override Ability Ability
		{
			get
			{
				return ability;
			}
		}

		public static Ability ability;

		public override CardInfo CardToDraw
		{
			get
			{
				List<string> list = new List<string> { "caninegod" };
				return CardLoader.GetCardByName(list[UnityEngine.Random.Range(0, list.Count)]);
			}
		}
		public override bool RespondsToResolveOnBoard()
		{
			return true;
		}

		public override IEnumerator OnResolveOnBoard()
		{
			yield return base.PreSuccessfulTriggerSequence();
			yield return base.CreateDrawnCard();
			yield return base.LearnAbility(0f);
			yield break;
		}
	}
}
