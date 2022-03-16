using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chow.Presentation.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    public ValidationFilterAttribute()
    {}
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        //Look for the first object which has a type that contains the Dto string
        var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Object is null. Controller {controller}, action: {action}");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }
}