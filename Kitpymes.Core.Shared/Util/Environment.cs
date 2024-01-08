// -----------------------------------------------------------------------
// <copyright file="Environment.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

/*
  Enum de utilidades
  Contiene las utilidades de los Environment
*/

/// <summary>
/// Enum de utilidades <c>Environment</c>.
/// Contiene las utilidades de los Environment.
/// </summary>
/// <remarks>
/// <para>En este enum se pueden agregar todos los tipos de ambientes del Environment.</para>
/// </remarks>
public class Environment
{
    /// <summary>
    /// Obtiene el nombre del entorno de desarrollo.
    /// </summary>
    /// <remarks>El entorno de desarrollo puede habilitar funciones que no deberían exponerse en producción. Debido al costo de rendimiento, la validación del alcance y de la dependencia solo ocurre en desarrollo.</remarks>
    public static readonly Environment Development = new (1, nameof(Development), "DEV");

    /// <summary>
    /// Obtiene el nombre del entorno de pruebas de calidad.
    /// </summary>
    /// <remarks>El entorno de pruebas de calidad se puede utilizar para validar los cambios de la aplicación antes de cambiar el entorno de ensayo.</remarks>
    public static readonly Environment Testing = new (2, nameof(Testing), "TEST");

    /// <summary>
    /// Obtiene el nombre del entorno de ensayo.
    /// </summary>
    /// <remarks>El entorno de ensayo se puede utilizar para validar los cambios de la aplicación antes de cambiar el entorno a producción.</remarks>
    public static readonly Environment Staging = new (3, nameof(Staging), "STAG");

    /// <summary>
    /// Obtiene el nombre del entorno de producción.
    /// </summary>
    /// <remarks>El entorno de producción debe configurarse para maximizar la seguridad, el rendimiento y la solidez de las aplicaciones.</remarks>
    public static readonly Environment Production = new (4, nameof(Production), "PROD");

    /// <summary>
    /// Obtiene el id de la enumeración.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Obtiene el nombre de la enumeración.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Obtiene el nombre corto de la enumeración.
    /// </summary>
    public string? ShortName { get; private set; }

    /// <summary>
    /// Obtiene un string con el nombre de la enumeración.
    /// </summary>
    /// <returns>{Name}.</returns>
    public override string ToString() => Name;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Environment"/>.
    /// </summary>
    /// <param name="id">Id de la enumeración.</param>
    /// <param name="name">Nombre de la enumeración.</param>
    /// <param name="shortName">Nombre corto de la enumeración.</param>
    private Environment(int id, string name, string? shortName = null)
        => (Id, Name, ShortName) = (id, name, shortName);
}
