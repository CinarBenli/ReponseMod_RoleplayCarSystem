using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReponseMod_RoleplayCarSystem
{
    public class Class1 : RocketPlugin<Config>
    {
        protected override void Load()
        {
            base.Load();

            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");
            Console.WriteLine("ReponseCode RoleplayCar = AKTİF");

            VehicleManager.onExitVehicleRequested += Exit;
            VehicleManager.onSwapSeatRequested += seat;
            VehicleManager.onEnterVehicleRequested += Enter;
        }



        private void Enter(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);


            var c = Configuration.Instance;
            if (vehicle.physicsSpeed <= c.MinGettingInTheVehicleSpeed)
            {

                StartCoroutine(EnterVehicle(pl,vehicle));
            }
            else
            {
                ChatManager.serverSendMessage(DefaultTranslations.Translate("WarningSpeed"), Color.white, null, pl.SteamPlayer(), (EChatMode)4, c.ServerLogo, true);
            }
        }

        public IEnumerator<WaitForSeconds> EnterVehicle(UnturnedPlayer pl, InteractableVehicle vh)
        {
        
                yield return new WaitForSeconds(Configuration.Instance.EnterVehicleTime);
                VehicleManager.ServerForcePassengerIntoVehicle(pl.Player, vh);



        }
        private void seat(Player player, InteractableVehicle vehicle, ref bool shouldAllow, byte fromSeatIndex, ref byte toSeatIndex)
        {
            var c = Configuration.Instance;
            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);
            if (vehicle.physicsSpeed <= c.SeatChangingMinSpeed)
            {
                if (toSeatIndex == 1)
                {
                    if (vehicle.lockedOwner == pl.CSteamID)
                    {
                        shouldAllow = true;

                    }
                    else
                    {
                        ChatManager.serverSendMessage(DefaultTranslations.Translate("WarningSeat"), Color.white, null, pl.SteamPlayer(), (EChatMode)4, c.ServerLogo, true);
                        shouldAllow = false;

                    }
                }
            }
            else
            {
                shouldAllow = false;
                ChatManager.serverSendMessage(DefaultTranslations.Translate("WarningSpeed"), Color.white, null, pl.SteamPlayer(), (EChatMode)4, c.ServerLogo, true);
            }
        }

        private void Exit(Player player, InteractableVehicle vehicle, ref bool shouldAllow, ref Vector3 pendingLocation, ref float pendingYaw)
        {
            var c = Configuration.Instance;

            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);

            if (vehicle.physicsSpeed <= c.MinLandingSpeed)
            {
                if (vehicle.isLocked)
                {
                    if (vehicle.lockedOwner == pl.CSteamID)
                    {
                        shouldAllow = true;
                    }
                    else
                    {
                        shouldAllow = false;
                        ChatManager.serverSendMessage(DefaultTranslations.Translate("WarningExit"), Color.white, null, pl.SteamPlayer(), (EChatMode)4, c.ServerLogo, true);
                    }
                }
                else
                {
                    shouldAllow = true;
                }
            }
            else
            {
                shouldAllow = false;
                ChatManager.serverSendMessage(DefaultTranslations.Translate("WarningSpeed"), Color.white, null, pl.SteamPlayer(), (EChatMode)4, c.ServerLogo, true);

            }


        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"WarningExit","<color=red>WARNİNG |</color>You Can't Get Off Because The Car Is Packed"},
            {"WarningSpeed","<color=red>WARNİNG |</color>You Can't Perform This Operation Because The Car Is Fast."},
            {"WarningSeat","<color=red>WARNİNG |</color>You Can't Get in the Driver's Seat Because The Car Isn't Yours."},
            {"GettingIntoTheCar","<color=red>WARNİNG |</color> You'll Be In the Car In 5 Seconds."}


        };

        protected override void Unload()
        {

            VehicleManager.onExitVehicleRequested -= Exit;
            VehicleManager.onSwapSeatRequested -= seat;
            VehicleManager.onEnterVehicleRequested -= Enter;
            base.Unload();
        }
    }


}
