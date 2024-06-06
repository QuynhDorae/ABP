using Volo.Abp.Settings;

namespace ProductApp.Settings;

public class ProductAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProductAppSettings.MySetting1));
    }
}
