using Rocket.API;

namespace ReponseMod_RoleplayCarSystem
{
    public class Config : IRocketPluginConfiguration
    {
        public string ServerLogo;
        public int SeatChangingMinSpeed;
        public int MinLandingSpeed;
        public int MinGettingInTheVehicleSpeed;
        public int SecondsToGetInTheVehicle;
        public int EnterVehicleTime;

        public void LoadDefaults()
        {
            ServerLogo = "HTTP";
            SeatChangingMinSpeed = 5;
            MinLandingSpeed = 5;
            MinGettingInTheVehicleSpeed = 5;
            SecondsToGetInTheVehicle = 5;

        }
    }
}