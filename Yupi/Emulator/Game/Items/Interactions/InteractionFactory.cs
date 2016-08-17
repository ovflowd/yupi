using System;
using Yupi.Emulator.Game.Items.Interactions.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interactions.Controllers;

namespace Yupi
{
	public class InteractionFactory
	{
		public static IFurniInteractor Create(Interaction interactionType) {
			switch (interactionType)
			{
			case Interaction.Gate:
				return new InteractorGate();

			case Interaction.GuildGate:
				return new InteractorGroupGate();

			case Interaction.ScoreBoard:
				return new InteractorScoreboard();

			case Interaction.VendingMachine:
				return new InteractorVendor();

			case Interaction.Alert:
				return new InteractorAlert();

			case Interaction.OneWayGate:
				return new InteractorOneWayGate();

			case Interaction.LoveShuffler:
				return new InteractorLoveShuffler();

			case Interaction.HabboWheel:
				return new InteractorHabboWheel();

			case Interaction.Dice:
				return new InteractorDice();

			case Interaction.Bottle:
				return new InteractorSpinningBottle();

			case Interaction.Hopper:
				return new InteractorHopper();

			case Interaction.Teleport:
				return new InteractorTeleport();

			case Interaction.Football:
				return new InteractorFootball();

			case Interaction.FootballCounterGreen:
			case Interaction.FootballCounterYellow:
			case Interaction.FootballCounterBlue:
			case Interaction.FootballCounterRed:
				return new InteractorScoreCounter();

			case Interaction.BanzaiScoreBlue:
			case Interaction.BanzaiScoreRed:
			case Interaction.BanzaiScoreYellow:
			case Interaction.BanzaiScoreGreen:
				return new InteractorBanzaiScoreCounter();

			case Interaction.BanzaiCounter:
				return new InteractorBanzaiTimer();

			case Interaction.FreezeTimer:
				return new InteractorFreezeTimer();

			case Interaction.FreezeYellowCounter:
			case Interaction.FreezeRedCounter:
			case Interaction.FreezeBlueCounter:
			case Interaction.FreezeGreenCounter:
				return new InteractorFreezeScoreCounter();

			case Interaction.FreezeTileBlock:
			case Interaction.FreezeTile:
				return new InteractorFreezeTile();

			case Interaction.JukeBox:
				return new InteractorJukebox();

			case Interaction.PuzzleBox:
				return new InteractorPuzzleBox();

			case Interaction.Mannequin:
				return new InteractorMannequin();

			case Interaction.Fireworks:
				return new InteractorFireworks();

			case Interaction.GroupForumTerminal:
				return new InteractorGroupForumTerminal();

			case Interaction.VikingCotie:
				return new InteractorVikingCotie();

			case Interaction.Cannon:
				return new InteractorCannon();

			case Interaction.FxBox:
				return new InteractorFxBox();

			case Interaction.HcGate:
				return new InteractorHcGate();

			case Interaction.QuickTeleport:
				return new InteractorQuickTeleport();

			case Interaction.CrackableEgg:
				return new InteractorCrackableEgg();

			case Interaction.FloorSwitch1:
			case Interaction.FloorSwitch2:
				return new InteractorSwitch();

			case Interaction.WalkInternalLink:
				return new InteractorWalkInternalLink();

			default:
				return new InteractorGenericSwitch();
			}
		}
	}
}

