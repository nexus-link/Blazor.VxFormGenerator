using Microsoft.Extensions.DependencyInjection;
using VxFormGenerator.Core.Repository;

namespace VxFormGenerator.Core
{
    public static class FormGeneratorServiceServiceCollectionExtension
    {
        public static void AddVxFormGenerator(IServiceCollection services, Layout.VxFormLayoutOptions vxFormLayoutOptions = null, IFormGeneratorComponentsRepository repository = null, IFormGeneratorOptions options = null)
        {

            if (vxFormLayoutOptions == null)
                throw new Exception("No layout options provided, please refer to the documentation.");

            if (repository == null)
                throw new Exception("No repository provided, please refer to the documentation.");

            if (options == null)
                throw new Exception("No options provided, please refer to the documentation.");


            services.AddSingleton(typeof(IFormGeneratorComponentsRepository), repository);
            services.AddSingleton(typeof(IFormGeneratorOptions), options);
            services.AddSingleton(typeof(Layout.VxFormLayoutOptions), vxFormLayoutOptions);
        }
    }

}
