// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.Obsolete.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.ComponentModel;
using Kitpymes.Core.Shared.Util;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable KPY0002 // Los métodos obsoletos usan el tipo obsoleto a propósito.

/// <summary>
/// Métodos obsoletos de <see cref="ServiceCollectionExtensions"/>. Se eliminan en la 3.0.0.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Obsoleto: el nombre tenía una errata y usaba <see cref="Util.Environment"/>. Usar <see cref="ToIsEnvironment(IServiceCollection, AppEnvironment)"/>.
    /// </summary>
    /// <param name="services">Servicios.</param>
    /// <param name="environment">Ambiente.</param>
    /// <returns>true si la aplicación se ejecuta en ese ambiente.</returns>
    [Obsolete("Usa ToIsEnvironment con AppEnvironment. Se elimina en 3.0.0.", DiagnosticId = "KPY0002")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool ToIsEnviroment(this IServiceCollection services, Util.Environment environment)
        => services.ToIsEnvironment(environment.ToAppEnvironment());

    /// <summary>
    /// Obsoleto: el nombre tenía una errata y usaba <see cref="Util.Environment"/>. Usar <see cref="ToIsEnvironment(IServiceProvider, AppEnvironment)"/>.
    /// </summary>
    /// <param name="services">Servicios.</param>
    /// <param name="environment">Ambiente.</param>
    /// <returns>true si la aplicación se ejecuta en ese ambiente.</returns>
    [Obsolete("Usa ToIsEnvironment con AppEnvironment. Se elimina en 3.0.0.", DiagnosticId = "KPY0002")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool ToIsEnviroment(this IServiceProvider services, Util.Environment environment)
        => services.ToIsEnvironment(environment.ToAppEnvironment());
}
