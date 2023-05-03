using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Anti_Team.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class UpdateGenStopBans : ICommand
    {
        public string Command { get; } = "tg_changeTeamBanStatus";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Requires <b>tg.ChangeBans</b> permission. Syntax: tg_stopGen [CI/(NTF)/(MTF)] [0/1] where arg 1 is team name (case-insensetive) and arg 2 is whether they are banned from" +
            " disabling generators or not.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("tg.ChangeBans"))
            {
                response = "Lacking <b>tg.ChangeBans</b> permission";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = "Current status:\n" +
                    $"CI Banned? => {(Plugin.CIBanned ? "YES" : "NO")}\n" +
                    $"NTF Banned? => {(Plugin.NTFBanned ? "YES" : "NO")}";
                return true;
            }
            if (arguments.Count != 2)
            {
                response = "Need 2 arguments. Syntax:\n" +
                    "tg_changeTeamBanStatus [        CI/(NTF/MTF)        ] [0 => Allowed/1 => Banned]\n" +
                    "Where                  |Team Name (case-insensitive)| |        Allowed?        |";
                return false;
            }

            string teamName = arguments.At(0).ToLower();
            if (teamName == "ci")
            {
                if (arguments.At(1) == "0")
                {
                    Plugin.CIBanned = false;
                }
                else if (arguments.At(1) == "1")
                {
                    Plugin.CIBanned = true;
                }
                else
                {
                    response = "unexpected value at arg 2 (expected 1 or 0)";
                    return false;
                }
                response = "Success! Updated!";
                return true;
            }
            else if (teamName == "ntf" || teamName == "mtf")
            {
                if (arguments.At(1) == "0")
                {
                    Plugin.NTFBanned = false;
                }
                else if (arguments.At(1) == "1")
                {
                    Plugin.NTFBanned = true;
                }
                else
                {
                    response = "unexpected value at arg 2 (expected 1 or 0)";
                    return false;
                }
                response = "Success! Updated!";
                return true;
            }
            else
            {
                response = "unknown team (expected ci, ntf or mtf)";
                return false;
            }
        }
    }
}
