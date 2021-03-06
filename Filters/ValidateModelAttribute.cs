﻿using CustomFramework.WebApiUtils.Contracts;
using CustomFramework.WebApiUtils.Contracts.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CustomFramework.WebApiUtils.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<ValidateModelAttribute> _logger;

        public ValidateModelAttribute(ILocalizationService localizationService, ILogger<ValidateModelAttribute> logger)
        {
            _localizationService = localizationService;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiResponse = new ApiResponse(_localizationService, _logger)
                    .ModelStateError(context.ModelState);

                context.Result = new BadRequestObjectResult(apiResponse);
            }
        }
    }
}
