// -----------------------------------------------------------------------
// <copyright file="Messages.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util;

using Kitpymes.Core.Shared.Extensions;

/*
    Configuración de los mensajes comunes
    En esta clase se pueden agregar todos los mensajes comunes necesarios para el sistema
*/

/// <summary>
/// Configuración de los mensajes comunes <c>Messages</c>.
/// Contiene los mensajes comunes al sistema.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todos los mensajes comunes necesarios para el sistema.</para>
/// </remarks>
public static class Messages
{
    /// <summary>
    /// Mensaje para campos que sean nulos o vacios.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"La campo es nulo o vacio" | "La campo {0} es nulo o vacio".</returns>
    public static string FieldNullOrEmpty(string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgNullOrEmpty : SharedResource.MsgNullOrEmptyWithFieldName.ToFormat(fieldName);

    /// <summary>
    /// Mensaje para listas que sean nulas o no contengan valores.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo es nulo o no contiene valores" | "El campo {0} es nulo o no contiene valores".</returns>
    public static string FieldNullOrAny(string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgNullOrAny : SharedResource.MsgNullOrAnyWithFieldName.ToFormat(fieldName);

    /// <summary>
    /// Mensaje para campos que son requeridos.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo es requerido" | "El campo {0} es requerido".</returns>
    public static string FieldRequired(string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgRequired : SharedResource.MsgRequiredWithFieldName.ToFormat(fieldName);

    /// <summary>
    /// Mensaje para campos con formatos inválidos.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo tiene un formato inválido" | "El campo {0} tiene un formato inválido".</returns>
    public static string FieldInvalidFormat(string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgInvalidFormat : SharedResource.MsgInvalidFormatWithFieldName.ToFormat(fieldName);

    /// <summary>
    /// Mensaje para cuando el valor es mayor que el mínimo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo debe ser mayor que {0}" | "El campo {0} debe ser mayor que {1}".</returns>
    public static string FieldGreater(long min, string? fieldName = null)
    => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgGreater.ToFormat(min) : SharedResource.MsgGreaterWithFieldName.ToFormat(fieldName, min);

    /// <summary>
    /// Mensaje para cuando el valor es mayor o igual que el mínimo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo debe ser mayor o igual que {0}" | "El campo {0} debe ser mayor o igual que {1}".</returns>
    public static string FieldGreaterOrEqual(long min, string? fieldName = null)
    => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgGreaterOrEqual.ToFormat(min) : SharedResource.MsgGreaterOrEqualWithFieldName.ToFormat(fieldName, min);

    /// <summary>
    /// Mensaje para cuando el valor es menor que el máximo configurado.
    /// </summary>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo debe ser menor a {0}" | "El campo {0} debe ser menor que {1}".</returns>
    public static string FieldLess(long max, string? fieldName = null)
    => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgLess.ToFormat(max) : SharedResource.MsgLessWithFieldName.ToFormat(fieldName, max);

    /// <summary>
    /// Mensaje para cuando el valor es menor o igual que el máximo configurado.
    /// </summary>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo debe ser menor o igual a {0}" | "El campo {0} debe ser menor o igual que {1}".</returns>
    public static string FieldLessOrEqual(long max, string? fieldName = null)
    => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgLessOrEqual.ToFormat(max) : SharedResource.MsgLessOrEqualWithFieldName.ToFormat(fieldName, max);

    /// <summary>
    /// Mensaje para comparar campos.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <param name="fieldNameCompare">Nombre del campo a comparar.</param>
    /// <returns>"Los campos comparados no coinciden" | "El campo {0} no coincide" | "El campo {0} no coincide con el campo {1}".</returns>
    public static string FieldEqual(string? fieldName = null, string? fieldNameCompare = null)
    {
        if (!fieldName.ToIsNullOrEmpty() && !fieldNameCompare.ToIsNullOrEmpty())
        {
            return SharedResource.MsgEqualWithFieldsName.ToFormat(fieldName, fieldNameCompare);
        }
        else if (!fieldName.ToIsNullOrEmpty())
        {
            return SharedResource.MsgEqualWithFieldName.ToFormat(fieldName);
        }

        return SharedResource.MsgEqual;
    }

    /// <summary>
    /// Mensaje para cuando el valor es mayor que el mínimo configurado y menor que el máximo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo se encuentra contenido entre {0} y {1}" | "El campo {0} se encuentra contenido entre {1} y {2}".</returns>
    public static string FieldInsideRange(long min, long max, string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgInsideRange.ToFormat(min, max) : SharedResource.MsgInsideRangeWithFieldName.ToFormat(fieldName, min, max);

    /// <summary>
    /// Mensaje para cuando el valor es mayor o igual que el mínimo configurado y menor o igual que el máximo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo se encuentra contenido o es igual que {0} y {1}" | "El campo {0} se encuentra contenido o es igual que {1} y {2}".</returns>
    public static string FieldInsideOrEqualRange(long min, long max, string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgInsideOrEqualRange.ToFormat(min, max) : SharedResource.MsgInsideOrEqualRangeWithFieldName.ToFormat(fieldName, min, max);

    /// <summary>
    /// Mensaje para cuando el valor es menor que el mínimo configurado y mayor que el máximo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo es menor que {0} o mayor que {1}" | "El campo {0} es menor que {0} o mayor que {1}".</returns>
    public static string FieldOutRange(long min, long max, string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgOutRange.ToFormat(min, max) : SharedResource.MsgOutRangeWithFieldName.ToFormat(fieldName, min, max);

    /// <summary>
    /// Mensaje para cuando el valor es menor o igual que el mínimo configurado y mayor o igual que el máximo configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="max">Valor máximo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>"El campo es menor o igual que {0} o mayor o igual que {1}" | "El campo {0} es menor o igual que {0} o mayor o igual que {1}".</returns>
    public static string FieldOutOrEqualRange(long min, long max, string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgOutOrEqualRange.ToFormat(min, max) : SharedResource.MsgOutOrEqualRangeWithFieldName.ToFormat(fieldName, min, max);

    /// <summary>
    /// Mensaje para cuando el path de un directorio no existe.
    /// </summary>
    /// <param name="value">Ruta del directorio.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string FieldDirectory(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgDirectory
                : SharedResource.MsgDirectoryWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgDirectoryWithValue.ToFormat(value)
                : SharedResource.MsgDirectoryWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje para cuando el path de un archivo no existe.
    /// </summary>
    /// <param name="value">Ruta del archivo.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string FieldFile(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgFile
                : SharedResource.MsgFileWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgFileWithValue.ToFormat(value)
                : SharedResource.MsgFileWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje por defecto cuando el valor ingresado no tiene el formato correcto de un email.
    /// </summary>
    /// <param name="value">Ruta del directorio.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string Email(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgEmail
                : SharedResource.MsgEmailWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgEmailWithValue.ToFormat(value)
                : SharedResource.MsgEmailWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje por defecto cuando el path de un directorio no existe.
    /// </summary>
    /// <param name="value">Ruta del directorio.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string FieldFileExtension(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgExtension
                : SharedResource.MsgExtensionWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgExtensionWithValue.ToFormat(value)
                : SharedResource.MsgExtensionWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje por defecto cuando el path de un directorio no existe.
    /// </summary>
    /// <param name="value">Ruta del directorio.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string Name(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgName
                : SharedResource.MsgNameWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgNameWithValue.ToFormat(value)
                : SharedResource.MsgNameWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje por defecto para cuando el valor ingresado es mayor que el configurado.
    /// </summary>
    /// <param name="min">Valor mínimo que puede tener.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string Password(long min, string? fieldName = null)
    => fieldName.ToIsNullOrEmpty()
            ? SharedResource.MsgPassword.ToFormat(min)
            : SharedResource.MsgPasswordWithFieldName.ToFormat(min, fieldName);

    /// <summary>
    /// Mensaje por defecto cuando el path de un directorio no existe.
    /// </summary>
    /// <param name="value">Ruta del directorio.</param>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string Subdomain(string? value = null, string? fieldName = null)
    {
        if (value.ToIsNullOrEmpty())
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgSubdomain
                : SharedResource.MsgSubdomainWithFieldName.ToFormat(fieldName);
        }
        else
        {
            return fieldName.ToIsNullOrEmpty()
                ? SharedResource.MsgSubdomainWithValue.ToFormat(value)
                : SharedResource.MsgSubdomainWithValueAndFieldName.ToFormat(value, fieldName);
        }
    }

    /// <summary>
    /// Mensaje por defecto para cuando el valor ingresado es mayor que el configurado.
    /// </summary>
    /// <param name="fieldName">Nombre del campo.</param>
    /// <returns>Mensaje de error.</returns>
    public static string NotFound(string? fieldName = null)
        => fieldName.ToIsNullOrEmpty() ? SharedResource.MsgNotFound : SharedResource.MsgNotFoundWithFieldName.ToFormat(fieldName);
}
