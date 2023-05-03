using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Anti_Team
{
    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Author => "Treeshold (aka Darcy Gaming) | That alias is back, huh!";

        public override string Name => "Generator-AntiTeam";

        public static bool CIBanned = false;

        public static bool NTFBanned = false;

        public override void OnEnabled()
        {
            CIBanned = Config.BanCIFromDisablingGenerators;
            NTFBanned = Config.BanMTFFromDisablingGenerators;

            Exiled.Events.Handlers.Player.StoppingGenerator += Player_StoppingGenerator;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.StoppingGenerator -= Player_StoppingGenerator;
        }

        public override void OnReloaded()
        {
            OnDisabled();
            OnEnabled();
        }

        private void Player_StoppingGenerator(Exiled.Events.EventArgs.Player.StoppingGeneratorEventArgs ev)
        {
            bool isCI = ev.Player.Role.Team == PlayerRoles.Team.ClassD || ev.Player.Role.Team == PlayerRoles.Team.ChaosInsurgency;
            bool isNTF = ev.Player.Role.Team == PlayerRoles.Team.Scientists || ev.Player.Role.Team == PlayerRoles.Team.FoundationForces;

            if ((isCI && CIBanned) || (isNTF && NTFBanned))
                ev.IsAllowed = false;
        }
    }
}
