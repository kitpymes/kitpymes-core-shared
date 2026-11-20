// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Collections.Generic;
using System.Reflection;
using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Scrutor;

/*
  Clase de extensión ServiceCollectionExtensions
  Contiene las extensiones de la colección de servicios
*/

/// <summary>
/// Clase de extensión <c>ServiceCollectionExtensions</c>.
/// Contiene las extensiones de la colección de servicios.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la colección  de descriptores de los servicios.</para>
/// </remarks>
public static partial class ServiceCollectionExtensions
{
    #region ToEnvironment

    #region IServiceCollection

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     IHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static IHostEnvironment? ToHostEnvironment(this IServiceCollection services)
    => services.ToEnvironment<IHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     IWebHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static IWebHostEnvironment? ToWebHostEnvironment(this IServiceCollection services)
    => services.ToEnvironment<IWebHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static TEnviromentType? ToEnvironment<TEnviromentType>(this IServiceCollection services)
        where TEnviromentType : IHostEnvironment
    => services.ToService<TEnviromentType>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo |
    ///     SharedException: si no encuentra el servicio.
    /// </returns>
    public static TEnviromentType? ToRequiredEnvironment<TEnviromentType>(this IServiceCollection services)
        where TEnviromentType : IHostEnvironment
    => services.ToRequiredService<TEnviromentType>();

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Development.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsDevelopment(this IServiceCollection services)
    => services.ToIsEnvironment(AppEnvironment.Development);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Testing.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsTesting(this IServiceCollection services)
    => services.ToIsEnvironment(AppEnvironment.Testing);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Staging.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsStaging(this IServiceCollection services)
    => services.ToIsEnvironment(AppEnvironment.Staging);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Production.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsProduction(this IServiceCollection services)
    => services.ToIsEnvironment(AppEnvironment.Production);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de producción.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <param name="environment">Tipo de ambiente de ejecución.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsEnvironment(this IServiceCollection services, AppEnvironment environment)
    => services.ToHostEnvironment()?.EnvironmentName == environment.Name
        || services.ToWebHostEnvironment()?.EnvironmentName == environment.Name;

    #endregion IServiceCollection

    #region IServiceProvider

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     IHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static IHostEnvironment? ToHostEnvironment(this IServiceProvider services)
    => services.ToEnvironment<IHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     IWebHostEnvironment: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static IWebHostEnvironment? ToWebHostEnvironment(this IServiceProvider services)
    => services.ToEnvironment<IWebHostEnvironment>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo.
    /// </returns>
    public static TEnviromentType? ToEnvironment<TEnviromentType>(this IServiceProvider services)
        where TEnviromentType : IHostEnvironment
    => services.GetService<TEnviromentType>();

    /// <summary>
    /// Obtiene los datos de ejecución del ambiente.
    /// </summary>
    /// <typeparam name="TEnviromentType">Tipo del servicio a buscar: IWebHostEnvironment | IHostEnvironment.</typeparam>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>
    ///     TEnviromentType: si el servicio existe |
    ///     nulo: si el servicio de tipo TService no existe |
    ///     SharedException: si el parametro services es nulo |
    ///     SharedException: si no encuentra el servicio.
    /// </returns>
    public static TEnviromentType? ToRequiredEnvironment<TEnviromentType>(this IServiceProvider services)
        where TEnviromentType : IHostEnvironment
    => services.ToEnvironment<TEnviromentType>().ToThrowIfNullOrEmpty(Messages.NotFound(typeof(TEnviromentType).Name));

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Development.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsDevelopment(this IServiceProvider services)
    => services.ToIsEnvironment(AppEnvironment.Development);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Testing.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsTesting(this IServiceProvider services)
    => services.ToIsEnvironment(AppEnvironment.Testing);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Staging.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsStaging(this IServiceProvider services)
    => services.ToIsEnvironment(AppEnvironment.Staging);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de Production.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsProduction(this IServiceProvider services)
    => services.ToIsEnvironment(AppEnvironment.Production);

    /// <summary>
    /// Obtiene un valor que indica si la aplicación se esta ejecutando en un ambiente de producción.
    /// </summary>
    /// <param name="services">Define un mecanismo para recuperar un objeto del proveedor de servicios.</param>
    /// <param name="environment">Tipo de ambiente de ejecución.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsEnvironment(this IServiceProvider services, AppEnvironment environment)
    => services.ToHostEnvironment()?.EnvironmentName == environment.Name
        || services.ToWebHostEnvironment()?.EnvironmentName == environment.Name;

    #endregion IServiceProvider

    #endregion ToEnvironment

    #region ToExists

    /// <summary>
    /// Verifica si existe un servicio.
    /// </summary>
    /// <typeparam name="TService">Tipo del servicio a buscar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <returns>Verdadero o falso.</returns>
    public static bool ToExists<TService>(this IServiceCollection services)
    => services.ToService<TService>() != null;

    #endregion ToExists

    #region ToService

    #region IServiceCollection

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo del servicio a buscar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static TService? ToService<TService>(this IServiceCollection services)
    => services.BuildServiceProvider().GetService<TService>();

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     object: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static object? ToService(this IServiceCollection services, Type type)
    => services.BuildServiceProvider().GetService(type);

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo o si no encuentra el servicio.
    /// </returns>
    public static TService ToRequiredService<TService>(this IServiceCollection services)
    => (TService)services.ToRequiredService(typeof(TService));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo o si no encuentra el servicio.
    /// </returns>
    public static object ToRequiredService(this IServiceCollection services, Type type)
    => services.ToService(type).ToThrowIfNullOrEmpty(Messages.NotFound(type.Name));

    /// <summary>
    /// Registra los servicios que la clase coincidan con la interfaz, utilizando el patrón (ClassName coincide con IClassName).
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="implementation">Tipo de implementación.</param>
    /// <param name="assemblies">Los assemblies se los servicios que queremos agregar a la colección.</param>
    /// <param name="lifetime">Especifica la vida útil de un servicio.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection ToServiceMatchingClass(
        this IServiceCollection services,
        Type implementation,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        assemblies.ToThrowIfNullOrEmptyCollection();

        services.Scan(scan => scan
           .FromAssemblies(assemblies)
           .AddClasses(classes => classes.AssignableTo(implementation))
           .UsingRegistrationStrategy(RegistrationStrategy.Skip)
           .AsMatchingInterface()
           .WithLifetime(lifetime));

        return services;
    }

    /// <summary>
    /// Registra los servicios que la clase coincidan con la interfaz, utilizando el patrón (ClassName coincide con IClassName).
    /// </summary>
    /// <typeparam name="TImplementation">Tipo de clase de la implementación.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="assemblies">Los assemblies se los servicios que queremos agregar a la colección.</param>
    /// <param name="lifetime">Especifica la vida útil de un servicio.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection ToServiceMatchingClass<TImplementation>(
        this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TImplementation : class
    => services.ToServiceMatchingClass(typeof(TImplementation), assemblies, lifetime);

    /// <summary>
    /// Registra los servicios que la clase coincidan con la interfaz, utilizando el patrón (ClassName coincide con IClassName).
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="assemblies">Los assemblies se los servicios que queremos agregar a la colección.</param>
    /// <param name="lifetime">Especifica la vida útil de un servicio.</param>
    /// <returns>IServiceCollection | SharedException: if assemblies is null or not contains elements.</returns>
    public static IServiceCollection ToServiceMatchingInterface(
        this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        assemblies.ToThrowIfNullOrEmptyCollection();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface((service, filter) =>
                filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
            .WithLifetime(lifetime));

        return services;
    }

    /// <summary>
    /// Registra los servicios que la clase coincidan con la interfaz, utilizando el patrón (ClassName coincide con IClassName).
    /// </summary>
    /// <typeparam name="TInterface">Tipo de interface de la implementación.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="assemblies">Los assemblies se los servicios que queremos agregar a la colección.</param>
    /// <param name="lifetime">Especifica la vida útil de un servicio.</param>
    /// <returns>IServiceCollection | SharedException: if assemblies is null or not contains elements.</returns>
    public static IServiceCollection ToServiceMatchingInterface<TInterface>(
        this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        assemblies.ToThrowIfNullOrEmptyCollection();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface((service, filter) =>
                filter.Where(implementation => implementation.Name.Equals(typeof(TInterface).Name, StringComparison.OrdinalIgnoreCase)))
            .WithLifetime(lifetime));

        return services;
    }

    #endregion IServiceCollection

    #region IServiceProvider

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo del servicio a buscar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static TService? ToService<TService>(this IServiceProvider services)
    => services.GetService<TService>();

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     object: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo |
    ///     null: si no encuentra el servicio de tipo TService.
    /// </returns>
    public static object? ToService(this IServiceProvider services, Type type)
    => services.GetService(type);

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo o si no encuentra el servicio.
    /// </returns>
    public static TService ToRequiredService<TService>(this IServiceProvider services)
    => (TService)services.ToRequiredService(typeof(TService));

    /// <summary>
    /// Obtiene el servicio del tipo solicitado.
    /// </summary>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="type">Tipo de servicio a obtener.</param>
    /// <returns>
    ///     TService: si encuentra el servicio |
    ///     SharedException: si el parametro services es nulo o si no encuentra el servicio.
    /// </returns>
    public static object ToRequiredService(this IServiceProvider services, Type type)
    => services.ToService(type).ToThrowIfNullOrEmpty(Messages.NotFound(type.Name));

    #endregion IServiceProvider

    #endregion ToService

    #region ToConfiguration

    /// <summary>
    /// Agrega los servicios de configuración de archivos json.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="directoryPath">Ruta del directorio donde se encuentran los atchivos json.</param>
    /// <param name="files">Los archivos json.</param>
    /// <returns>La colección de servicios.</returns>
    public static IServiceCollection ToConfiguration(
        this IServiceCollection services,
        string directoryPath,
        params (string jsonFileName, bool optional, bool reloadOnChange)[] files)
    {
        directoryPath.ToThrowIfDirectoryNotExists();
        files.ToThrowIfNullOrEmptyCollection();

        var configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(directoryPath)
              .ToJsonFiles(directoryPath, files)
              .AddEnvironmentVariables()
              .Build();

        services.ToConfiguration(configurationBuilder);

        return services;
    }

    /// <summary>
    /// Agrega los servicios de configuración de las claves y su respectivo valor (key-value).
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="configuration">Las claves y sus valores (key-value).</param>
    /// <returns>La colección de servicios.</returns>
    public static IServiceCollection ToConfiguration(
        this IServiceCollection services,
        params (string key, string? value)[] configuration)
    {
        configuration.ToThrowIfNullOrEmptyCollection();

        var list = new Dictionary<string, string?>();

        foreach (var (key, value) in configuration)
        {
            list.Add(key, value);
        }

        var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(list)
            .Build();

        services.ToConfiguration(configurationBuilder);

        return services;
    }

    /// <summary>
    /// Agrega los servicios de configuración.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="configuration">Representa un conjunto de propiedades de configuración de la aplicación clave / valor.</param>
    /// <returns>La colección de servicios.</returns>
    public static IServiceCollection ToConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ToThrowIfNullOrEmpty().TryAddSingleton(configuration);

        return services;
    }

    #endregion ToConfiguration

    #region ToSettings

    /// <summary>
    /// Configura las clases que coinciden con los archivos de configuración.
    /// </summary>
    /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
    /// <typeparam name="TConfigureSettings">Tipo de clase para configuraciones custom (Usada para obtener datos de la db en el momento de inicio del sistema).</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="defaultSettings">Configuración por defecto.</param>
    /// <returns>TSettings | null.</returns>
    public static TSettings? ToSettings<TSettings, TConfigureSettings>(this IServiceCollection services, Action<TSettings>? defaultSettings = null)
        where TSettings : class, new()
        where TConfigureSettings : class, IConfigureOptions<TSettings>
    {
        if (defaultSettings.ToIsNullOrEmpty())
        {
            services.ToSettings((TSettings)null!);
        }
        else
        {
            services.ToSettings(defaultSettings.ToConfigureOrDefault());
        }

        var result = services
            .AddSingleton<IConfigureOptions<TSettings>, TConfigureSettings>()
            .ToService<IOptions<TSettings>>()?.Value;

        return result;
    }

    /// <summary>
    /// Configura las clases que coinciden con los archivos de configuración.
    /// </summary>
    /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
    /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
    /// <param name="defaultSettings">Configuración por defecto.</param>
    /// <returns>IServiceCollection.</returns>
    public static TSettings ToSettings<TSettings>(this IServiceCollection services, TSettings? defaultSettings = null)
        where TSettings : class, new()
    {
        TSettings? settings;

        if (!services.ToExists<TSettings>())
        {
            services.AddOptions<TSettings>();

            var section = services.ToService<IConfiguration>()?.GetSection(typeof(TSettings).Name);

            if (section.Exists())
            {
                services.Configure<TSettings>(section);

                var service = services.ToService<IOptions<TSettings>>();

                settings = service?.Value;

                if (!defaultSettings.ToIsNullOrEmpty())
                {
                    settings = settings?.ToMapUpdate(defaultSettings);
                }
            }
            else
            {
                settings = defaultSettings ?? new TSettings();
            }

            if (settings != null)
            {
                services.AddSingleton(settings);
            }
        }

        var result = services.ToService<TSettings>().ToThrowIfNullOrEmpty(typeof(TSettings).Name);

        return result;
    }

    private static IConfigurationBuilder ToJsonFiles(
        this IConfigurationBuilder builder,
        string directoryPath,
        params (string jsonFileName, bool optional, bool reloadOnChange)[] files)
    {
        foreach (var (jsonFileName, optional, reloadOnChange) in files)
        {
            var filePath = $"{directoryPath}/{jsonFileName}.json";

            builder.AddJsonFile(filePath.ToThrowIfFileNotExists(), optional, reloadOnChange);
        }

        return builder;
    }

    #endregion ToSettings
}
