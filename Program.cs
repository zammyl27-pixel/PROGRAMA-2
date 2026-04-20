using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Ingrese la expresión:");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Expresión inválida");
            return;
        }

        // Validación de paréntesis
        if (!ParentesisBalanceados(input))
        {
            Console.WriteLine("Expresión incorrecta: paréntesis desbalanceados");
            return;
        }

        // Tokenización
        var identificadores = new HashSet<string>();
        var constantes = new HashSet<string>();
        var operadores = new List<char>();

        Tokenizar(input, identificadores, constantes, operadores);

        // Validación básica de sintaxis (operadores mal puestos)
        if (!ValidarSintaxisBasica(input))
        {
            Console.WriteLine("Expresión incorrecta: error de sintaxis");
        }
        else
        {
            Console.WriteLine("Expresión correcta");
        }

        // Mostrar resultados
        Console.WriteLine("\nIdentificadores:");
        foreach (var id in identificadores)
            Console.WriteLine(id);

        Console.WriteLine("\nConstantes:");
        foreach (var c in constantes)
            Console.WriteLine(c);

        Console.WriteLine("\nOperandos (todos):");
        foreach (var id in identificadores)
            Console.WriteLine(id);
        foreach (var c in constantes)
            Console.WriteLine(c);
    }

    static bool ParentesisBalanceados(string expr)
    {
        int balance = 0;

        foreach (char c in expr)
        {
            if (c == '(') balance++;
            if (c == ')') balance--;

            if (balance < 0) return false;
        }

        return balance == 0;
    }

    static void Tokenizar(string expr, HashSet<string> ids, HashSet<string> consts, List<char> ops)
    {
        // Identificadores (letras)
        foreach (Match m in Regex.Matches(expr, @"\b[A-Za-z]+\b"))
        {
            ids.Add(m.Value);
        }

        // Constantes (números)
        foreach (Match m in Regex.Matches(expr, @"\b\d+\b"))
        {
            consts.Add(m.Value);
        }

        // Operadores
        foreach (char c in expr)
        {
            if ("+-*/%".Contains(c))
            {
                ops.Add(c);
            }
        }
    }

    static bool ValidarSintaxisBasica(string expr)
    {
        // Reglas simples:
        // No puede haber dos operadores seguidos
        // No puede empezar o terminar con operador
        char prev = '\0';

        expr = expr.Replace(" ", "");

        if (expr.Length == 0) return false;

        if ("+-*/%".Contains(expr[0]) || "+-*/%".Contains(expr[^1]))
            return false;

        for (int i = 0; i < expr.Length; i++)
        {
            char current = expr[i];

            if ("+-*/%".Contains(current))
            {
                if ("+-*/%".Contains(prev))
                    return false;
            }

            prev = current;
        }

        return true;
    }
}