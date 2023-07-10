using StoreApp.WebAPI.Utilities.Formatters;

namespace StoreApp.WebAPI.Extensions
{
    public static class IMvcBuilderExceptions
    {
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config =>
            {
                config.OutputFormatters.Add(new CSVOutputFormatter());
            });
        }
    }
}
