using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MyBooks.Data.Views;

namespace MyBooks.Exceptions
{
	public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError => {
				appError.Run(async context => {
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var feature = context.Features.Get<IExceptionHandlerFeature>();
					var request = context.Features.Get<IHttpRequestFeature>();

					if (feature != null) {
                        await context.Response.WriteAsync(new ErrorViews()
                        {
							StatusCode = context.Response.StatusCode,
                            Message = feature.Error.Message,
							Path = request.Path,
						}.ToString());
					}
				});
			});
		}
	}
}

