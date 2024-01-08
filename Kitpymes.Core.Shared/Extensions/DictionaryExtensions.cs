// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System.Collections.Generic;
using System.Linq;

/*
    Clase de extensión DictionaryExtensions
    Contiene las extensiones del delegado Action
*/

/// <summary>
/// Clase de extensión <c>DictionaryExtensions</c>.
/// Contiene las extensiones para el diccionario de valores.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para el diccionario de valores.</para>
/// </remarks>
public static class DictionaryExtensions
{
    /// <summary>
    /// Agrega una clave si no existe o agrega un valor a la clave si esta existe.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <param name="value">Valor del diccionario.</param>
    /// <returns>IDictionary{string, IList{TValue}} | SharedException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio o si value es nulo o vacio.</returns>
    public static IDictionary<string, IEnumerable<TValue>> ToAddOrUpdate<TValue>(
        this IDictionary<string, IEnumerable<TValue>>? dictionary,
        string key,
        TValue value)
    {
        dictionary.ToThrowIfNullOrEmpty();
        key.ToThrowIfNullOrEmpty();
        value.ToThrowIfNullOrEmpty();

        if (dictionary.TryGetValue(key, out IEnumerable<TValue>? list))
        {
            if (!list.Contains(value))
            {
                dictionary.Remove(key);
                dictionary.Add(key, new List<TValue> { value });
            }
        }
        else
        {
            dictionary.Add(key, new List<TValue> { value });
        }

        return dictionary;
    }

    /// <summary>
    /// Verifica si existe una clave y su valor en la lista de tipo diccionario.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <param name="value">Valor del diccionario.</param>
    /// <returns>bool | SharedException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio o si value es nulo o vacio.</returns>
    public static bool ToContainsKeyValue(this IDictionary<string, IEnumerable<string>>? dictionary, string key, string value)
    {
        dictionary.ToThrowIfNullOrEmpty();
        key.ToThrowIfNullOrEmpty();
        value.ToThrowIfNullOrEmpty();

        return dictionary.ContainsKey(key) && dictionary[key].Contains(value);
    }

    /// <summary>
    /// Obtiene los valores de una clave.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <returns>IList{string} | null: si la key no existe o no contiene valores | SharedException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio.</returns>
    public static IEnumerable<string>? ToValues(this IDictionary<string, IEnumerable<string>>? dictionary, string key)
    {
        dictionary.ToThrowIfNullOrEmpty();
        key.ToThrowIfNullOrEmpty();

        return dictionary.TryGetValue(key, out IEnumerable<string>? value) ? value : null;
    }
}
