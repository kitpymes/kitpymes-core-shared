// -----------------------------------------------------------------------
// <copyright file="AppEnvironment.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

using System;
using System.Linq;

/// <summary>
/// Ambientes de ejecución de la aplicación.
/// </summary>
/// <remarks>
/// Sustituye a <c>Environment</c>, que se llamaba igual que <see cref="System.Environment"/>.
/// El nombre (<see cref="Name"/>) es el que se compara con el EnvironmentName del host.
/// </remarks>
public sealed class AppEnvironment
{
    /// <summary>Desarrollo.</summary>
    public static readonly AppEnvironment Development = new (1, nameof(Development), "DEV");

    /// <summary>Pruebas.</summary>
    public static readonly AppEnvironment Testing = new (2, nameof(Testing), "TEST");

    /// <summary>Preproducción.</summary>
    public static readonly AppEnvironment Staging = new (3, nameof(Staging), "STAG");

    /// <summary>Producción.</summary>
    public static readonly AppEnvironment Production = new (4, nameof(Production), "PROD");

    /// <summary>Obtiene todos los ambientes.</summary>
    public static AppEnvironment[] All { get; } = new[] { Development, Testing, Staging, Production };

    /// <summary>Obtiene el identificador.</summary>
    public int Id { get; }

    /// <summary>Obtiene el nombre, tal como lo usa el host (EnvironmentName).</summary>
    public string Name { get; }

    /// <summary>Obtiene el nombre corto.</summary>
    public string ShortName { get; }

    /// <summary>
    /// Busca un ambiente por su nombre, sin distinguir mayúsculas.
    /// </summary>
    /// <param name="name">Nombre del ambiente.</param>
    /// <returns>El ambiente, o null si no existe.</returns>
    public static AppEnvironment? FromName(string? name)
        => All.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <inheritdoc/>
    public override string ToString() => Name;

    private AppEnvironment(int id, string name, string shortName)
        => (Id, Name, ShortName) = (id, name, shortName);
}
