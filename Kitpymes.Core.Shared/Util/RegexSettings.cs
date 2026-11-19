// -----------------------------------------------------------------------
// <copyright file="RegexSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

using System;

/// <summary>
/// Configuración común de las expresiones regulares de la librería.
/// </summary>
internal static class RegexSettings
{
    /// <summary>
    /// Tiempo máximo de evaluación de una expresión regular.
    /// Sin límite, un patrón o un texto malintencionado pueden dejar el proceso colgado (ReDoS).
    /// </summary>
    internal static readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);
}
