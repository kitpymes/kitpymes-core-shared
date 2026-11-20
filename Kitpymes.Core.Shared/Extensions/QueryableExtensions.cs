// -----------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

/*
  Clase de extensión QueryableExtensions
  Contiene las extensiones de IQueryable
*/

/// <summary>
/// Clase de extensión <c>QueryableExtensions</c>.
/// Contiene las extensiones de IQueryable.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para IQueryable.</para>
/// </remarks>
public static class QueryableExtensions
{
    /// <summary>
    /// Filtra una secuencia de valores basada en un predicado.
    /// </summary>
    /// <typeparam name="T">Tipo de la lista.</typeparam>
    /// <param name="queryable">Instancia IQueryable.</param>
    /// <param name="where">Predicado a evaluar.</param>
    /// <returns>La lista con el filtro.</returns>
    public static IQueryable<T> ToWhere<T>(this IQueryable<T> queryable, Expression<Func<T, bool>>? where)
        where T : class
    {
        if (!where.ToIsNullOrEmpty())
        {
            queryable = queryable.Where(where);
        }

        return queryable;
    }

    /// <summary>
    /// Filtra una secuencia de valores basada en un predicado.
    /// </summary>
    /// <typeparam name="T">Tipo de la lista.</typeparam>
    /// <param name="queryable">Instancia IQueryable.</param>
    /// <param name="condition">Si se cumple la condición se ejecuta la expresion.</param>
    /// <param name="where">Predicado a evaluar.</param>
    /// <returns>La lista con el filtro.</returns>
    public static IQueryable<T> ToWhereIf<T>(this IQueryable<T> queryable, Func<bool> condition, Expression<Func<T, bool>>? where)
        where T : class
    {
        if (condition())
        {
            queryable = queryable.ToWhere(where);
        }

        return queryable;
    }

    /// <summary>
    /// Paginado de una lista.
    /// </summary>
    /// <typeparam name="T">Tipo de la lista.</typeparam>
    /// <param name="queryable">Instancia IQueryable.</param>
    /// <param name="properties">Nombre de los campos por el que se requiere ordenar separados por (.) punto, ej, "age.lastname.firstname".</param>
    /// <param name="ascending">Orden de la lista ascendente o descendente.</param>
    /// <param name="index">Número de página, empezando en 1.</param>
    /// <param name="size">Cantidad de elementos por página.</param>
    /// <returns>La página pedida: como máximo <paramref name="size"/> elementos.</returns>
    /// <exception cref="Util.SharedException">Si <paramref name="index"/> o <paramref name="size"/> son menores que 1.</exception>
    public static IQueryable<T> ToPaged<T>(
        this IQueryable<T> queryable,
        string? properties = null,
        bool ascending = true,
        int index = 1,
        int size = 20)
    {
        // Sólo se comprueba null: preguntar si tiene elementos lanzaría una consulta extra contra la base de datos.
        if (queryable is null)
        {
            return queryable!;
        }

        index.ToThrowIfLess(1);
        size.ToThrowIfLess(1);

        if (!properties.ToIsNullOrEmpty())
        {
            queryable = queryable.ToOrder(properties, ascending);
        }

        // Skip y Take no modifican la consulta: devuelven una nueva, que hay que guardar.
        return queryable.Skip((index - 1) * size).Take(size);
    }

    private static IQueryable<T> ToOrder<T>(
        this IQueryable<T> queryable,
        string properties,
        bool ascending)
    {
        // Sólo se comprueba null: comprobar si tiene elementos lanzaría la consulta y fallaría con una lista vacía.
        queryable.ToThrowIfNullOrEmpty();
        properties.ToThrowIfNullOrEmpty();

        var propertiesOrder = properties.Split('.');

        Type? propertyType = typeof(T);

        propertyType = propertiesOrder.Aggregate(propertyType, (Type? propertyTypeCurrent, string propertyName) =>
            propertyTypeCurrent?.GetProperty(propertyName)?.PropertyType);

        if (propertyType == typeof(sbyte))
        {
            return queryable.ToOrder<T, sbyte>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(short))
        {
            return queryable.ToOrder<T, short>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(int))
        {
            return queryable.ToOrder<T, int>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(long))
        {
            return queryable.ToOrder<T, long>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(byte))
        {
            return queryable.ToOrder<T, byte>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(ushort))
        {
            return queryable.ToOrder<T, ushort>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(uint))
        {
            return queryable.ToOrder<T, uint>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(ulong))
        {
            return queryable.ToOrder<T, ulong>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(char))
        {
            return queryable.ToOrder<T, char>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(float))
        {
            return queryable.ToOrder<T, float>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(double))
        {
            return queryable.ToOrder<T, double>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(decimal))
        {
            return queryable.ToOrder<T, decimal>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(bool))
        {
            return queryable.ToOrder<T, bool>(propertiesOrder, ascending);
        }

        if (propertyType == typeof(string))
        {
            return queryable.ToOrder<T, string>(propertiesOrder, ascending);
        }

        return queryable.ToOrder<T, object>(propertiesOrder, ascending);
    }

    private static IQueryable<T> ToOrder<T, TKey>(
        this IQueryable<T> queryable,
        IEnumerable<string> properties,
        bool ascending)
    {
        var parameters = Expression.Parameter(typeof(T));

        var body = properties.Aggregate<string, Expression>(parameters, Expression.Property);

        var expression = Expression.Lambda<Func<T, TKey>>(body, parameters).Compile();

        return ascending ? queryable.AsEnumerable().OrderBy(expression).AsQueryable() : queryable.AsEnumerable().OrderByDescending(expression).AsQueryable();
    }
}
