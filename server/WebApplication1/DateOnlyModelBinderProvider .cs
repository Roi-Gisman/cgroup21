using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1
{
    public class DateOnlyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(DateOnly)
                ? new BinderTypeModelBinder(typeof(DateOnlyModelBinder))
                : null;
        }
    }
}
