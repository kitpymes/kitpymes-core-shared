// -----------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/*
    Clase de extensión IntegerExtensions
    Contiene las extensiones del delegado Action
*/

/// <summary>
/// Clase de extensión <c>IntegerExtensions</c>.
/// Contiene las extensiones de los números enteros.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para los números enteros.</para>
/// </remarks>
public static class IntegerExtensions
{
    /// <summary>
    /// Convierte un número entero en string.
    /// </summary>
    /// <param name="input">El número a convertir en string.</param>
    /// <param name="formatProvider">El formato que se aplica a la conversión.</param>
    /// <returns>El número convertido en string.</returns>
    [return: NotNull]
    public static string ToStringFormat(this int input, IFormatProvider? formatProvider = null)
    => input.ToString(formatProvider ?? CultureInfo.CurrentCulture);

    /// <summary>
    /// Convierte un integer de tipo {TEnum} en un enum.
    /// </summary>
    /// <typeparam name="TEnum">Tipo de enum.</typeparam>
    /// <param name="key">Id del enum.</param>
    /// <param name="defaultValue">Valor por defecto que se le asigna si el valor no pertenece al tipo {TEnum}.</param>
    /// <returns>Enum de tipo {TEnum}.</returns>
    public static TEnum ToEnum<TEnum>(this int? key, TEnum defaultValue = default)
        where TEnum : struct, Enum
    {
        if (key.ToIsNullOrEmpty() || !Enum.IsDefined(typeof(TEnum), key))
        {
            return defaultValue;
        }

        return (TEnum)Enum.ToObject(typeof(TEnum), key);
    }
}
