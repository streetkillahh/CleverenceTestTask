using System;
using System.Text;

public class StringCompression
{
    public static string Compress(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        StringBuilder compressed = new StringBuilder();
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                // Добавление предыдущего символа и его количества
                compressed.Append(input[i - 1]);
                if (count > 1)
                    compressed.Append(count);

                count = 1;
            }
        }

        // Добавление последнего символа и его количества
        compressed.Append(input[^1]);
        if (count > 1)
            compressed.Append(count);

        return compressed.ToString();
    }

    public static string Decompress(string compressed)
    {
        if (string.IsNullOrEmpty(compressed))
            return compressed;

        StringBuilder decompressed = new StringBuilder();
        int number = 0;

        foreach (char c in compressed)
        {
            if (char.IsDigit(c))
            {
                // Формирование числа из последовательности цифр
                number = number * 10 + (c - '0');
            }
            else
            {
                // Если до этого было число, добавляется предыдущий символ нужное количество раз
                if (number > 0)
                {
                    decompressed.Append(new string(decompressed[^1], number - 1));
                    number = 0;
                }

                // Добавление текущего символа
                decompressed.Append(c);
            }
        }

        // Обработка случая, если строка заканчивается числом
        if (number > 0)
        {
            decompressed.Append(new string(decompressed[^1], number - 1));
        }

        return decompressed.ToString();
    }
}