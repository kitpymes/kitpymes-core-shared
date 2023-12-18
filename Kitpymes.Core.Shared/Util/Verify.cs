// -----------------------------------------------------------------------
// <copyright file="Verify.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

using System;
using System.Diagnostics.CodeAnalysis;

/*
    Clase de validaciones de Verify
    Contiene las validaciones mas utilizadas
*/

/// <summary>
/// Clase de validaciones <c>Verify</c>.
/// Contiene las validaciones mas utilizadas.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todos los metodos para hacer validaciones.</para>
/// </remarks>
public static class Verify
{
    /// <summary>
    /// Lanza una excepción de tipo TException.
    /// </summary>
    /// <typeparam name="TException">Tipo e excepción.</typeparam>
    /// <param name="message">Mensaje a mostrar en la TException.</param>
    /// <param name="condition">Condición a evaluar.</param>
    [DoesNotReturn]
    public static void ToThrowIf<TException>(string message, Func<bool> condition)
        where TException : Exception
#pragma warning disable CS8763 // Un método marcado como [DoesNotReturn] no debe devolver nada.
    {
        var exception = Activator.CreateInstance(typeof(TException), new object[] { message }) as TException;

        if (exception is not null && condition())
        {
            throw exception;
        }
    }
#pragma warning restore CS8763 // Un método marcado como [DoesNotReturn] no debe devolver nada.
}
