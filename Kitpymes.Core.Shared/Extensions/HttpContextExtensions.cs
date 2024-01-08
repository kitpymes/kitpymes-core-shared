// -----------------------------------------------------------------------
// <copyright file="HttpContextExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Http;

/*
    Clase de extensión HttpContextExtensions
    Contiene las extensiones del HttpContext
*/

/// <summary>
/// Clase de extensión <c>HttpContextExtensions</c>.
/// Contiene las extensiones del HttpContext.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para el HttpContext.</para>
/// </remarks>
public static class HttpContextExtensions
{
    /// <summary>
    /// Obtiene la IP de la solicitud HTTP.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
    /// <param name="ip">Valor de la ip a devolver.</param>
    /// <returns>string | null.</returns>
    public static bool ToTryIPv6(this HttpContext httpContext, [MaybeNullWhen(false)] out string? ip)
    {
        ip = httpContext?.ToIPv6();

        return !ip.ToIsNullOrEmpty();
    }

    /// <summary>
    /// Obtiene la IP de la solicitud HTTP.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
    /// <returns>string | null | SharedException: si el parámetro httpContext es nulo.</returns>
    public static string? ToIPv6(this HttpContext httpContext)
    => httpContext.ToThrowIfNullOrEmpty().Connection?.RemoteIpAddress?.MapToIPv6()?.ToString();

    /// <summary>
    /// Devuelve los detalles de una solicitud HTTP.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
    /// <param name="optionalData">Datos opcionales.</param>
    /// <returns>Los detalles de una solicitud HTTP.</returns>
    public static object ToDetails(this HttpContext httpContext, object? optionalData = null)
    {
        var validHttpContext = httpContext.ToThrowIfNullOrEmpty();

        dynamic details = new System.Dynamic.ExpandoObject();

        details.User = validHttpContext.User.ToUserName() ?? Environment.UserName ?? "Anonymous";

        if (validHttpContext.ToTryIPv6(out var ip))
        {
            details.IP = ip;
        }

        if (!validHttpContext.Request.ToIsNullOrEmpty())
        {
            if (validHttpContext.Request.ToTryHeader("User-Agent", out var userAgents))
            {
                details.UserAgent = userAgents;
            }

            if (validHttpContext.Request.ToTryContentType(out var contentType))
            {
                details.ContentType = contentType;
            }

            details.Request = validHttpContext.Request.ToPath();
        }

        if (!optionalData.ToIsNullOrEmpty())
        {
            details.OptionalData = optionalData;
        }

        return details;
    }
}
