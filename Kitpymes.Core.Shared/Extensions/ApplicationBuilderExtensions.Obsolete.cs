// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.Obsolete.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.ComponentModel;
using Kitpymes.Core.Shared.Util;
using Microsoft.AspNetCore.Builder;

#pragma warning disable KPY0002 // Los métodos obsoletos usan el tipo obsoleto a propósito.

/// <summary>
/// Métodos obsoletos de <see cref="ApplicationBuilderExtensions"/>. Se eliminan en la 3.0.0.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Obsoleto: el nombre tenía una errata y usaba <see cref="Util.Environment"/>. Usar <see cref="ToIsEnvironment(IApplicationBuilder, AppEnvironment)"/>.
    /// </summary>
    /// <param name="application">Constructor de la aplicación.</param>
    /// <param name="environment">Ambiente.</param>
    /// <returns>true si la aplicación se ejecuta en ese ambiente.</returns>
    [Obsolete("Usa ToIsEnvironment con AppEnvironment. Se elimina en 3.0.0.", DiagnosticId = "KPY0002")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool ToIsEnviroment(this IApplicationBuilder application, Util.Environment environment)
        => application.ToIsEnvironment(environment.ToAppEnvironment());
}
