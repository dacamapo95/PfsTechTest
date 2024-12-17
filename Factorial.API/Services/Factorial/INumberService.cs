using Factorial.API.Infrastructure;

namespace Factorial.API.Services.Factorial;

public interface INumberService
{
    /// <summary>
    /// Calcula el factorial del valor dado.
    /// </summary>
    /// <param name="value">El valor para el cual calcular el factorial.</param>
    /// <returns>Resultado con factorial del valor dado.</returns>
    Result<long> CalculateFactorial(int value);

    /// <summary>
    /// Ordena los números dados.
    /// </summary>
    /// <param name="numbers">Números a ordenar.</param>
    /// <returns>Resultado con array ordenado.</returns>
    int[] Sort(params int[] numbers);
}