using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Generator_Anti_Team
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Should Chaos Insurgency (and classDs) be banned from disabling generators?")]
        public bool BanCIFromDisablingGenerators { get; set; } = true;

        [Description("Should Mobile Task Force (and scientists) be banned from disabling generators?")]
        public bool BanMTFFromDisablingGenerators { get; set; } = true;
    }
}