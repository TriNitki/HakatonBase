﻿namespace Pkg.UseCases;

/// <summary>
/// Расширение перечисления <see cref="ResultStatus"/>
/// </summary>
public static class ResultStatusExtensions
{
    /// <summary>
    /// Успешно
    /// </summary>
    /// <param name="status">Статус</param>
    /// <returns><see langword="true"/> если успешно, иначе <see langword="false"/></returns>
    public static bool IsSuccess(this ResultStatus status)
        => (int)status is >= 200 and < 300;
}
