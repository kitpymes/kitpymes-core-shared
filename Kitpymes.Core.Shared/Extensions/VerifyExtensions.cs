// -----------------------------------------------------------------------
// <copyright file="VerifyExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Kitpymes.Core.Shared.Util;

/*
    Clase de extensión VerifyExtensions
    Contiene las extensiones de las validaciones
*/

/// <summary>
/// Clase de extensión <c>VerifyExtensions</c>.
/// Contiene las extensiones para validar argumentos.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para las validaciones.</para>
/// </remarks>
public static class VerifyExtensions
{
    #region NullOrEmpty

    /// <summary>
    /// Verifica si un argumento es nulo o vacío.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsNullOrEmpty([NotNullWhen(false)] this object? argument)
        => argument is null
            || (argument is string && string.IsNullOrWhiteSpace(argument.ToString()))
            || (argument is Guid guid && guid == Guid.Empty);

    /// <summary>
    /// Verifica si un argumento es nulo o vacío.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un argumento  es nulo o vacío.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <param name="message">Mensaje a mostrar.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfNullOrEmpty<TArgument>([NotNull] this TArgument? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null, string? message = null)
        => argument.ToThrowIf(message ?? Messages.FieldNullOrEmpty(argumentName), () => argument.ToIsNullOrEmpty());

    #endregion NullOrEmpty

    #region NullOrAny

    /// <summary>
    /// Verifica si una colección es nula o no contiene items.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsNullOrAny([NotNullWhen(false)] this IEnumerable? argument)
    {
        if (argument is null)
        {
            return true;
        }

        switch (argument)
        {
            case string s when s.ToIsNullOrEmpty() || s.Length < 1:
            case Array a when a.Length < 1:
            case ICollection c when c.Count < 1:
                return true;
            default:
                return argument.Cast<object>()?.Any() == false;
        }
    }

    /// <summary>
    /// Verifica si un argumento es nulo o no contiene valores.
    /// </summary>
    /// <exception cref="SharedException">Si un argumento es nulo o no contiene valores.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static IEnumerable<TArgument> ToThrowIfNullOrAny<TArgument>([NotNull] this IEnumerable<TArgument>? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
        => argument.ToThrowIf(Messages.FieldNullOrAny(argumentName), () => argument.ToIsNullOrAny());

    #endregion NullOrAny

    #region Greater

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor que el valor mínimo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsGreater([NotNullWhen(true)] this object? argument, long min)
    {
        argument.ToThrowIfNullOrEmpty();

        switch (argument)
        {
            case Enum _ when (int)argument > min:
            case string s when s.Length > min:
            case sbyte sb when sb > min:
            case short sh when sh > min:
            case int inte when inte > min:
            case long lo when lo > min:
            case byte by when by > min:
            case ushort us when us > min:
            case uint ui when ui > min:
            case ulong ul when Convert.ToInt64(ul) > min:
            case char ch when ch > min:
            case float fl when fl > min:
            case double d when d > min:
            case decimal de when de > min:
            case Array a when a.Length > min:
            case ICollection c when c.Count > min:
            case IEnumerable e when e.Cast<object>().Count() > min:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor o igual que el valor mínimo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsGreaterOrEqual([NotNullWhen(true)] this object? argument, long min)
    {
        argument.ToThrowIfNullOrEmpty();

        switch (argument)
        {
            case Enum _ when (int)argument >= min:
            case string s when s.Length >= min:
            case sbyte sb when sb >= min:
            case short sh when sh >= min:
            case int inte when inte >= min:
            case long lo when lo >= min:
            case byte by when by >= min:
            case ushort us when us >= min:
            case uint ui when ui >= min:
            case ulong ul when Convert.ToInt64(ul) >= min:
            case char ch when ch >= min:
            case float fl when fl >= min:
            case double d when d >= min:
            case decimal de when de >= min:
            case Array a when a.Length >= min:
            case ICollection c when c.Count >= min:
            case IEnumerable e when e.Cast<object>().Count() >= min:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor que el valor mínimo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor que el valor mínimo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfGreater<TArgument>([NotNull] this TArgument? argument, long min, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldGreater(min, argumentName), () => argument.ToIsGreater(min));

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor o igual que el valor mínimo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres de un string o cantidad de valores de una colección es mayor o igual que el valor mínimo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfGreaterOrEqual<TArgument>([NotNull] this TArgument? argument, long min, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldGreaterOrEqual(min, argumentName), () => argument.ToIsGreaterOrEqual(min));

    #endregion Greater

    #region Less

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es menor que el valor máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsLess([NotNullWhen(true)] this object? argument, long max)
    {
        argument.ToThrowIfNullOrEmpty();

        switch (argument)
        {
            case Enum _ when (int)argument < max:
            case string s when s.Length < max:
            case sbyte sb when sb < max:
            case short sh when sh < max:
            case int inte when inte < max:
            case long lo when lo < max:
            case byte by when by < max:
            case ushort us when us < max:
            case uint ui when ui < max:
            case ulong ul when Convert.ToInt64(ul) < max:
            case char ch when ch < max:
            case float fl when fl < max:
            case double d when d < max:
            case decimal de when de < max:
            case Array a when a.Length < max:
            case ICollection c when c.Count < max:
            case IEnumerable e when e.Cast<object>().Count() < max:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es menor o igual que el valor máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsLessOrEqual([NotNullWhen(true)] this object? argument, long max)
    {
        argument.ToThrowIfNullOrEmpty();

        switch (argument)
        {
            case Enum _ when (int)argument <= max:
            case string s when s.Length <= max:
            case sbyte sb when sb <= max:
            case short sh when sh <= max:
            case int inte when inte <= max:
            case long lo when lo <= max:
            case byte by when by <= max:
            case ushort us when us <= max:
            case uint ui when ui <= max:
            case ulong ul when Convert.ToInt64(ul) <= max:
            case char ch when ch <= max:
            case float fl when fl <= max:
            case double d when d <= max:
            case decimal de when de <= max:
            case Array a when a.Length <= max:
            case ICollection c when c.Count <= max:
            case IEnumerable e when e.Cast<object>().Count() <= max:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un número o cantidad de caracteres de un string o cantidad de valores de una colección es menor que el valor máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres de un string o cantidad de valores de una colección es menor que el valor máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfLess<TArgument>([NotNull] this TArgument? argument, long max, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldLess(max, argumentName), () => argument.ToIsLess(max));

    /// <summary>
    /// Verifica si un valor es menor o igual que el valor máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres de un string o cantidad de valores de una colección es menor o igual que el valor máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfLessOrEqual<TArgument>([NotNull] this TArgument? argument, long max, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldLessOrEqual(max, argumentName), () => argument.ToIsLessOrEqual(max));

    #endregion Less

    #region Equal

    /// <summary>
    /// Verifica si dos argumentos son iguales.
    /// </summary>
    /// <param name="argument">El primer argumento a comparar.</param>
    /// <param name="argumentCompare">El segundo argumento a comparar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsEqual([NotNullWhen(true)] this object? argument, object? argumentCompare)
        => Equals(argument, argumentCompare);

    /// <summary>
    /// Verifica si dos argumentos son iguales.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del argumento a comparar.</typeparam>
    /// <exception cref="SharedException">Si los argumentos no son iguales.</exception>
    /// <param name="argument">El primer argumento a comparar.</param>
    /// <param name="argumentCompare">El segundo argumento a comparar.</param>
    /// <param name="argumentName">Nombre del primer argumento.</param>
    /// <param name="argumentCompareName">Nombre del segundo argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfEqual<TArgument>(
        [NotNull] this TArgument? argument,
        TArgument argumentCompare,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null,
        [CallerArgumentExpression(nameof(argumentCompare))] string? argumentCompareName = null)
    => argument.ToThrowIf(Messages.FieldEqual(argumentName, argumentCompareName), () => argument.ToIsEqual(argumentCompare));

    /// <summary>
    /// Verifica si dos argumentos son iguales.
    /// </summary>
    /// <param name="argument">El primer argumento a comparar.</param>
    /// <param name="argumentCompare">El segundo argumento a comparar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsNotEqual([NotNullWhen(true)] this object? argument, object? argumentCompare)
        => !Equals(argument, argumentCompare);

    /// <summary>
    /// Verifica si dos argumentos son iguales.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del argumento a comparar.</typeparam>
    /// <exception cref="SharedException">Si los argumentos no son iguales.</exception>
    /// <param name="argument">El primer argumento a comparar.</param>
    /// <param name="argumentCompare">El segundo argumento a comparar.</param>
    /// <param name="argumentName">Nombre del primer argumento.</param>
    /// <param name="argumentCompareName">Nombre del segundo argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfNotEqual<TArgument>(
        [NotNull] this TArgument? argument,
        TArgument argumentCompare,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null,
        [CallerArgumentExpression(nameof(argumentCompare))] string? argumentCompareName = null)
    => argument.ToThrowIf(Messages.FieldNotEqual(argumentName, argumentCompareName), () => argument.ToIsNotEqual(argumentCompare));

    #endregion Equal

    #region InsideRange

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores que el mínimo permitido y sean menores que el máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsInsideRange([NotNullWhen(true)] this object? argument, long min, long max)
    => argument.ToIsGreater(min) && argument.ToIsLess(max);

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores o iguales que el mínimo permitido y sean menores o iguales que el máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsInsideOrEqualRange([NotNullWhen(true)] this object? argument, long min, long max)
    => argument.ToIsGreaterOrEqual(min) && argument.ToIsLessOrEqual(max);

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores que el mínimo permitido y sean menores que el máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores que el mínimo permitido y sean menores que el máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfInsideRange<TArgument>(
        [NotNull] this TArgument? argument,
        long min,
        long max,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInsideRange(min, max, argumentName), () => argument.ToIsInsideRange(min, max));

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores o iguales que el mínimo permitido y sean menores o iguales que el máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres o cantidad de valores de una colección sean mayores o iguales que el mínimo permitido y sean menores o iguales que el máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo o igual permitido.</param>
    /// <param name="max">El valor máximo o igual permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfInsideOrEqualRange<TArgument>(
        [NotNull] this TArgument? argument,
        long min,
        long max,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInsideOrEqualRange(min, max, argumentName), () => argument.ToIsInsideOrEqualRange(min, max));

    #endregion InsideRange

    #region OutRange

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean menores que el mínimo permitido y sean mayores que el máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsOutRange([NotNullWhen(true)] this object? argument, long min, long max)
    => argument.ToIsGreater(max) || argument.ToIsLess(min);

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean menores o iguales que el mínimo permitido y sean mayores o iguales que el máximo permitido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsOutOrEqualRange([NotNullWhen(true)] this object? argument, long min, long max)
    => argument.ToIsGreaterOrEqual(max) || argument.ToIsLessOrEqual(min);

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean menores que el mínimo permitido y sean mayores que el máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres o cantidad de valores de una colección sean menores que el mínimo permitido y sean mayores que el máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo permitido.</param>
    /// <param name="max">El valor máximo permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfOutRange<TArgument>(
        [NotNull] this TArgument? argument,
        long min,
        long max,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldOutRange(min, max, argumentName), () => argument.ToIsOutRange(min, max));

    /// <summary>
    /// Verifica si un número o cantidad de caracteres o cantidad de valores de una colección sean menores o iguales que el mínimo permitido y sean mayores o iguales que el máximo permitido.
    /// </summary>
    /// <typeparam name="TArgument">Tipo del valor a verificar.</typeparam>
    /// <exception cref="SharedException">Si un número o cantidad de caracteres o cantidad de valores de una colección sean menores o iguales que el mínimo permitido y sean mayores o iguales que el máximo permitido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="min">El valor mínimo o igual permitido.</param>
    /// <param name="max">El valor máximo o igual permitido.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static TArgument ToThrowIfOutOrEqualRange<TArgument>(
        [NotNull] this TArgument? argument,
        long min,
        long max,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldOutOrEqualRange(min, max, argumentName), () => argument.ToIsOutOrEqualRange(min, max));

    #endregion OutRange

    #region RegexMatch

    /// <summary>
    /// Verifica si un argumento coincide con la expresión regular.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="regex">Expresión regular.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsRegexMatch([NotNullWhen(true)] this string? argument, string regex)
    => !argument.ToIsNullOrEmpty() && Regex.IsMatch(argument, regex, RegexOptions.CultureInvariant);

    /// <summary>
    /// Verifica si un argumento coincide con la expresión regular.
    /// </summary>
    /// <exception cref="SharedException">Si un argumento no coincide con la expresión regular.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="regex">Expresión regular.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfRegexNotMatch([NotNull] this string? argument, string regex, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsRegexMatch(regex));

    #endregion RegexMatch

    #region Directory

    /// <summary>
    /// Verifica si existe el path de un directorio.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsDirectory([NotNullWhen(true)] this string? argument) => Directory.Exists(argument);

    /// <summary>
    /// Verifica si existe el path de un directorio.
    /// </summary>
    /// <exception cref="SharedException">Si no existe el path de un directorio.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string? ToThrowIfDirectoryNotExists([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldDirectory(argument, argumentName), () => !argument.ToIsDirectory());

    #endregion Directory

    #region File

    /// <summary>
    /// Verifica si existe el path de un archivo.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsFile([NotNullWhen(true)] this string? argument) => File.Exists(argument);

    /// <summary>
    /// Verifica si existe el path de un archivo.
    /// </summary>
    /// <exception cref="SharedException">Si no existe el path de un archivo.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <param name="message">Mensaje a mostrar.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfFileNotExists([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null, string? message = null)
    => argument.ToThrowIf(message ?? Messages.FieldFile(argument, argumentName), () => !argument.ToIsFile());

    #endregion File

    #region FileExtension

    /// <summary>
    /// Verifica si existe la extensión de un archivo.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsFileExtension([NotNullWhen(true)] this string? argument)
    => Path.GetExtension(argument)?.Length > 0;

    /// <summary>
    /// Verifica si existe la extensión de un archivo.
    /// </summary>
    /// <exception cref="SharedException">Si no existe la extensión de un archivo.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotFileExtension([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldFileExtension(argument, argumentName), () => !argument.ToIsFileExtension());

    #endregion FileExtension

    #region Name

    /// <summary>
    /// Verifica si el argumento contiene un nombre valido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsName([NotNullWhen(true)] this string? argument) => argument.ToIsRegexMatch(Regexp.ForName);

    /// <summary>
    /// Verifica si el argumento contiene un nombre valido.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento contiene un nombre invalido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotName([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsName());

    #endregion Name

    #region Email

    /// <summary>
    /// Verifica si el argumento contiene un email valido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsEmail([NotNullWhen(true)] this string? argument) => argument.ToIsRegexMatch(Regexp.ForEmail);

    /// <summary>
    /// Verifica si el argumento contiene un email valido.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento contiene un email invalido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotEmail([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsEmail());

    #endregion Email

    #region Subdomain

    /// <summary>
    /// Verifica si el argumento contiene un subdominio valido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsSubdomain([NotNullWhen(true)] this string? argument) => argument.ToIsRegexMatch(Regexp.ForSubdomain);

    /// <summary>
    /// Verifica si el argumento contiene un subdominio valido.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento contiene un subdominio invalido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotSubdomain([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsSubdomain());

    #endregion Subdomain

    #region Domain

    /// <summary>
    /// Verifica si el argumento contiene un dominio valido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsDomain([NotNullWhen(true)] this string? argument) => argument.ToIsRegexMatch(Regexp.ForDomain);

    /// <summary>
    /// Verifica si el argumento contiene un dominio valido.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento contiene un dominio invalido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotDomain([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsSubdomain());

    #endregion Domain

    #region Hostname

    /// <summary>
    /// Verifica si el argumento contiene un hostname valido.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsHostname([NotNullWhen(true)] this string? argument) => argument.ToIsRegexMatch(Regexp.ForHostname);

    /// <summary>
    /// Verifica si el argumento contiene un hostname valido.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento contiene un hostname invalido.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotHostname([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsSubdomain());

    #endregion Hostname

    #region Digit

    /// <summary>
    /// Verifica si el argumento contiene algún caracter decimal.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsContainsDigit([NotNullWhen(true)] this string? argument)
    => argument?.Any(char.IsDigit) == true;

    /// <summary>
    /// Verifica si el argumento contiene algún caracter decimal.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento no contiene algún caracter decimal.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotContainsDigit([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsContainsDigit());

    #endregion Digit

    #region UniqueChars

    /// <summary>
    /// Verifica si el argumento contiene caracteres repetidos.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsContainsUniqueChars([NotNullWhen(true)] this string? argument)
    {
        if (argument is null)
        {
            return false;
        }

        var list = new HashSet<char>(argument.Length);

        foreach (char val in argument)
        {
            if (list.Contains(val))
            {
                return false;
            }

            list.Add(val);
        }

        return true;
    }

    /// <summary>
    /// Verifica si el argumento contiene caracteres repetidos.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento nop contiene caracteres repetidos.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotContainsUniqueChars([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsContainsUniqueChars());

    #endregion UniqueChars

    #region EspecialChars

    /// <summary>
    /// Verifica si el argumento contiene algún caracter especial.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsContainsEspecialChars([NotNullWhen(true)] this string? argument)
    => argument?.Any(x => !char.IsLetterOrDigit(x)) == true;

    /// <summary>
    /// Verifica si el argumento contiene algún caracter especial.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento no contiene algún caracter especial.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotContainsEspecialChars([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsContainsEspecialChars());

    #endregion EspecialChars

    #region Lowercase

    /// <summary>
    /// Verifica si el argumento contiene algún caracter en minúscula.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsLowercase([NotNullWhen(true)] this string? argument)
    => argument?.Any(char.IsLower) == true;

    /// <summary>
    /// Verifica si el argumento contiene algún caracter en minúscula.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento no contiene algún caracter en minúscula.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotLowercase([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsLowercase());

    #endregion Lowercase

    #region Uppercase

    /// <summary>
    /// Verifica si el argumento contiene algún caracter en mayúscula.
    /// </summary>
    /// <param name="argument">Argumento a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsUppercase([NotNullWhen(true)] this string? argument)
    => argument?.Any(char.IsUpper) == true;

    /// <summary>
    /// Verifica si el argumento contiene algún caracter en mayúscula.
    /// </summary>
    /// <exception cref="SharedException">Si el argumento no contiene algún caracter en mayúscula.</exception>
    /// <param name="argument">Argumento a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>argument | SharedException.</returns>
    public static string ToThrowIfNotUppercase([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
    => argument.ToThrowIf(Messages.FieldInvalidFormat(argumentName), () => !argument.ToIsUppercase());

    #endregion Uppercase

    #region ThrowIf

    /// <summary>
    /// Valida si una o varias condiciones son verdaderas.
    /// </summary>
    /// <typeparam name="TValue?">Tipo del valor a verificar.</typeparam>
    /// <param name="argument">El valor a verificar.</param>
    /// <param name="message">Mensaje a mostrar en la TException.</param>
    /// <param name="condition">Función a evaluar.</param>
    /// <returns>TValue | SharedException: si la evaluación del predicado es verdadero.</returns>
    public static TValue ToThrowIf<TValue>([NotNull] this TValue? argument, string message, Func<bool> condition)
    {
        Verify.ToThrowIf<SharedException>(message, condition);

        return argument;
    }

    #endregion ThrowIf
}
