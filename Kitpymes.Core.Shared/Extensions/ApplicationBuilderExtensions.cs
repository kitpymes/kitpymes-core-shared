// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
public static partial class ApplicationBuilderExtensions
{
    #region ToEnvironment

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="application">Define un mecanismo para recuperar un objeto del proveedor de servicios de aplicaciones.</param>
    /// <returns>
    ///     IHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro application es nulo.
    /// </returns>
    public static IHostEnvironment? ToHostEnvironment(this IApplicationBuilder application)
    => application.ToEnvironment<IHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="application">Define un mecanismo para recuperar un objeto del proveedor de servicios de aplicaciones.</param>
    /// <returns>
    ///     IWebHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro application es nulo.
    /// </returns>
    public static IWebHostEnvironment? ToWebHostEnvironment(this IApplicationBuilder application)
    => application.ToEnvironment<IWebHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="application">Define un mecanismo para recuperar un objeto del proveedor de servicios de aplicaciones.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro application es nulo.
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
    ///     SharedException: si el parametro application es nulo |
    ///     SharedException: si no encuentra el servicio.
    /// </returns>
    public static TEnviromentType? ToRequiredEnvironment<TEnviromentType>(this IApplicationBuilder application)
        where TEnviromentType : IHostEnvironment
    => application.ToRequiredService<TEnviromentType>();

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Development.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsDevelopment(this IApplicationBuilder application)
    => application.ToIsEnvironment(AppEnvironment.Development);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Testing.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsTesting(this IApplicationBuilder application)
    => application.ToIsEnvironment(AppEnvironment.Testing);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Staging.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsStaging(this IApplicationBuilder application)
    => application.ToIsEnvironment(AppEnvironment.Staging);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Production.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsProduction(this IApplicationBuilder application)
    => application.ToIsEnvironment(AppEnvironment.Production);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de producción.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <param name="environment">Tipo de ambiente de ejecución.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsEnvironment(this IApplicationBuilder application, AppEnvironment environment)
    => application.ToHostEnvironment()?.EnvironmentName == environment.Name ||
        application.ToWebHostEnvironment()?.EnvironmentName == environment.Name;

    #endregion ToEnvironment

    /// <summary>
    /// Verifica si existe un servicio.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>
    ///     true: si el servicio de tipo TService existe |
    ///     false: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro application es nulo.
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
    ///     SharedException: si el parametro application es nulo |
    ///     SharedException: si no encuentra el servicio.
    /// </returns>
    public static TService ToRequiredService<TService>(this IApplicationBuilder application)
    => (TService)application.ToRequiredService(typeof(TService));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro application es nulo |
    ///     SharedException: si no encuentra el servicio.
    /// </returns>
    public static object ToRequiredService(this IApplicationBuilder application, Type type)
    => application.ToService(type).ToThrowIfNullOrEmpty(Messages.NotFound(type.Name));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro application es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static TService? ToService<TService>(this IApplicationBuilder application)
    => (TService?)application.ToService(typeof(TService));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro application es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static object? ToService(this IApplicationBuilder application, Type type)
    => application.ToThrowIfNullOrEmpty().ApplicationServices.GetService(type);
}
