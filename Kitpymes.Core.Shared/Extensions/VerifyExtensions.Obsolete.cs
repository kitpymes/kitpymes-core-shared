// -----------------------------------------------------------------------
// <copyright file="VerifyExtensions.Obsolete.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// Métodos obsoletos de <see cref="VerifyExtensions"/>. Se eliminan en la 3.0.0.
/// </summary>
public static partial class VerifyExtensions
{
    /// <summary>
    /// Obsoleto: el nombre decía lo contrario de lo que hace. Usar <see cref="ToIsNullOrEmptyCollection"/>.
    /// </summary>
    /// <param name="argument">Colección a verificar.</param>
    /// <returns>true si es nula o no tiene elementos; si no, false.</returns>
    [Obsolete("Usa ToIsNullOrEmptyCollection. Se elimina en 3.0.0.", DiagnosticId = "KPY0001")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool ToIsNullOrAny([NotNullWhen(false)] this IEnumerable? argument)
        => argument.ToIsNullOrEmptyCollection();

    /// <summary>
    /// Obsoleto: el nombre decía lo contrario de lo que hace. Usar <see cref="ToThrowIfNullOrEmptyCollection{TArgument}"/>.
    /// </summary>
    /// <typeparam name="TArgument">Tipo de los elementos.</typeparam>
    /// <param name="argument">Colección a verificar.</param>
    /// <param name="argumentName">Nombre del argumento.</param>
    /// <returns>La misma colección.</returns>
    [Obsolete("Usa ToThrowIfNullOrEmptyCollection. Se elimina en 3.0.0.", DiagnosticId = "KPY0001")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static IEnumerable<TArgument> ToThrowIfNullOrAny<TArgument>([NotNull] this IEnumerable<TArgument>? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
        => argument.ToThrowIfNullOrEmptyCollection(argumentName);
}
