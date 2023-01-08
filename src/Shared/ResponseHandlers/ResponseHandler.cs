using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Shared.ResponseHandlers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseHandlers
{
    public static class ResponseHandler
    {
        public static T OK<T>(T response) where T : ResponseHandlerDto, new()
        {
            response.Status = HttpStatusCode.OK;
            return response;
        }

        public static T Created<T>(T response) where T : ResponseHandlerDto
        {
            response.Status = HttpStatusCode.Created;
            return response;
        }

        public static T NoContent<T>() where T : ResponseHandlerDto, new() =>
            new() { Status = HttpStatusCode.NoContent };

        public static T BadRequest<T>(string mensagem, string tag = "Request") where T : ResponseHandlerDto, new()
        {
            return new()
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                Errors = new List<BadRequestDto>()
                {
                    new BadRequestDto(tag, mensagem) 
                }
            };
        }

        public static T BadRequest<T>(ValidationResult result) where T : ResponseHandlerDto, new()
        {
            return new() 
            { 
                Status = System.Net.HttpStatusCode.BadRequest,
                Errors = result?.Errors
                            .Select(erro => 
                                new BadRequestDto(erro.PropertyName, erro.ErrorMessage)) ?? new List<BadRequestDto>()
            };
        }

        public static IResult ResponseResult<TResponse>(
            TResponse response)
                where TResponse : ResponseHandlerDto =>
                    ResponseResult(
                        response, x => x, x => x.Errors);

        public static IResult ResponseResult<TResponse>(
            TResponse response,
            Func<TResponse, object> mapSuccessRequestBody)
                where TResponse : ResponseHandlerDto =>
                    ResponseResult(
                        response, mapSuccessRequestBody, x => x.Errors);

        public static IResult ResponseResult<TResponse>(
            TResponse response,
            Func<TResponse, object> mapSuccessRequestBody,
            Func<TResponse, object> mapFailureRequestBody)
                where TResponse : ResponseHandlerDto
        {
            var httpStatus = response.Status ?? default;
            response.Status = null;
            var requestSucceeded = Succeeded(httpStatus);
            var mapReqBody = requestSucceeded
                ? mapSuccessRequestBody
                : mapFailureRequestBody;

            var reqBody = mapReqBody?.Invoke(response);
            var hasReqBody = reqBody != null;
            var result = hasReqBody && httpStatus != HttpStatusCode.NoContent
                ? Results.Json(
                            reqBody, 
                            statusCode: (int)httpStatus)
                : Results.StatusCode(
                            (int)httpStatus);

            return result;
        }

        private static bool Succeeded(HttpStatusCode status) =>
            status == HttpStatusCode.OK || status == HttpStatusCode.NoContent || status == HttpStatusCode.Created;
    }
}
