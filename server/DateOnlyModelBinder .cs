using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1
{
    public class DateOnlyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
            if (DateOnly.TryParse(value, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.FieldName,
                    "Invalid date format. Use YYYY-MM-DD");
            }

            return Task.CompletedTask;
        }
    }
}
