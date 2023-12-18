// -----------------------------------------------------------------------
// <copyright file="SharedException.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

using System;

/*
    Clase de exception generica SharedException
    Contiene una exception generica
*/

/// <summary>
/// Clase de exception generica <c>SharedException</c>.
/// Contiene una exception generica.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todos los metodos para la excepción generica.</para>
/// </remarks>
public class SharedException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SharedException"/>.
    /// </summary>
    public SharedException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SharedException"/>.
    /// </summary>
    /// <param name="message">Mensaje.</param>
    public SharedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SharedException"/>.
    /// </summary>
    /// <param name="message">Mensaje.</param>
    /// <param name="innerException">Inner exception.</param>
    public SharedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
