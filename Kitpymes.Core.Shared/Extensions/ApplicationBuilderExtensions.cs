// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

/*
    Clase de extensión ApplicationBuilderExtensions
    Contiene las extensiones de la aplicación IApplicationBuilder
*/

/// <summary>
/// Clase de extensión <c>ApplicationBuilderExtensions</c>.
/// Contiene las extensiones de la aplicación.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para de la aplicación.</para>
/// </remarks>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="application">Define un mecanismo para recuperar un objeto del proveedor de servicios de aplicaciones.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     ArgumentException: si el parametro application es nulo.
    /// </returns>
    public static TEnviromentType? ToEnvironment<TEnviromentType>(this IApplicationBuilder application)
        where TEnviromentType : IHostEnvironment
    => application.ToService<TEnviromentType>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="application">Define un mecanismo para recuperar un objeto del proveedor de servicios de aplicaciones.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     ArgumentException: si el parametro application es nulo |
    ///     ApplicationException: si no encuentra el servicio.
    /// </returns>
    public static TEnviromentType? ToRequiredEnvironment<TEnviromentType>(this IApplicationBuilder application)
        where TEnviromentType : IHostEnvironment
    => application.ToRequiredService<TEnviromentType>();

    /// <summary>
    /// Verifica si existe un servicio.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>
    ///     true: si el servicio de tipo TService existe |
    ///     false: si el servicio de tipo TService no existe |
    ///     ArgumentException: si el parametro application es nulo.
    /// </returns>
    public static bool ToExists<TService>(this IApplicationBuilder application)
    => !application.ToService<TService>().ToIsNullOrEmpty();

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     ArgumentException: si el parametro application es nulo |
    ///     ApplicationException: si no encuentra el servicio.
    /// </returns>
    public static TService ToRequiredService<TService>(this IApplicationBuilder application)
    => application.ToService<TService>().ToThrowIfNullOrEmpty(Messages.NotFound(typeof(TService).Name));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     ArgumentException: si el parametro application es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static TService? ToService<TService>(this IApplicationBuilder application)
    => application.ToThrowIfNullOrEmpty().ApplicationServices.GetService<TService>();
}
