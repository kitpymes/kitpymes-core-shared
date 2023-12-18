// -----------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System.Globalization;
using System.Text;

/*
  Clase de extensión StringBuilderExtensions
  Contiene las extensiones de las cadenas StringBuilder
*/

/// <summary>
/// Clase de extensión <c>StringBuilderExtensions</c>.
/// Contiene las extensiones de las cadenas StringBuilder.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para las cadenas StringBuilder.</para>
/// </remarks>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Agrega un string con un formato.
    /// </summary>
    /// <param name="stringBuilder">Lista de strings.</param>
    /// <param name="format">Formato.</param>
    /// <returns>Agrega un nuevo valor | ArgumentException: stringBuilder es nulo.</returns>
    public static StringBuilder ToAppend(this StringBuilder stringBuilder, string format)
    => stringBuilder.ToThrowIfNullOrEmpty().AppendFormat(CultureInfo.InvariantCulture, format.ToThrowIfNullOrEmpty());
}
