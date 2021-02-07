using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.ModelVerify
{
    /// <summary>
    /// ViewModel verification
    /// </summary>
    public class ModelVerifyNotifyFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder validationResult = new StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        validationResult.Append(!string.IsNullOrWhiteSpace(error.ErrorMessage) ? error.ErrorMessage + "·" : error.Exception?.Message + "·");
                    }
                }

                context.Result = new ObjectResult(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    success = true,
                    message = validationResult.ToString()
                });
            }
        }
    }
}
