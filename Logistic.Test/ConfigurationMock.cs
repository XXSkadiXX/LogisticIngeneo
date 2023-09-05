namespace Logistic.Test
{
    public static class ConfigurationMock
    {
        public static Dictionary<string, string> ConfigAppSetting()
        {
            Dictionary<string, string> appSettingTest = new Dictionary<string, string>
            {
                { "Tokens:Key", "pu~],i&KzlCg=^rPqn$szH{Vi"},
                { "Tokens:Issuer", "localhost"},
                { "Tokens:Audience", "users"},

                { "Settings:TerrestrialLogisticsDiscount", "5"},
                { "Settings:MaritimeLogisticDiscount", "3"},
                { "Settings:DiscountAmount", "10"},
            };

            return appSettingTest;
        }
    }
}
