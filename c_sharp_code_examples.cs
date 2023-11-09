class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(CalculateSum(new int[] { 1, 2, 3, 4, 5 }));
        Console.WriteLine(FindMax(new int[] { 1, 2, 3, 4, 5 }));
        Console.WriteLine(FindMin(new int[] { 1, 2, 3, 4, 5 }));
        Console.WriteLine(ComputeAverage(new int[] { 1, 2, 3, 4, 5 }));
        Console.WriteLine(ConcatenateStrings(new string[] { "Hello", "World" }));
        Console.WriteLine(IsPalindrome("racecar"));
        Console.WriteLine(Factorial(5));
        Console.WriteLine(IsPrime(7));
        PrintArray(GetFibonacciSequence(10));
        Console.WriteLine(ConvertToBinary(10));
        Console.WriteLine(ConvertFromBinary("1010"));
        Console.WriteLine(ReverseString("Hello World"));
        Console.WriteLine(CountWords("Hello World"));
    }

    static int CalculateSum(int[] numbers)
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        return sum;
    }

    static int FindMax(int[] numbers)
    {
        int max = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }
        return max;
    }

    static int FindMin(int[] numbers)
    {
        int min = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] < min)
            {
                min = numbers[i];
            }
        }
        return min;
    }

    static double ComputeAverage(int[] numbers)
    {
        int sum = CalculateSum(numbers);
        return (double)sum / numbers.Length;
    }

    static string ConcatenateStrings(string[] strings)
    {
        string result = string.Empty;
        foreach (string str in strings)
        {
            result += str;
        }
        return result;
    }

    static bool IsPalindrome(string text)
    {
        int min = 0;
        int max = text.Length - 1;
        while (true)
        {
            if (min > max)
            {
                return true;
            }
            char a = text[min];
            char b = text[max];
            if (char.ToLower(a) != char.ToLower(b))
            {
                return false;
            }
            min++;
            max--;
        }
    }

    static int Factorial(int number)
    {
        if (number <= 1)
            return 1;
        return number * Factorial(number - 1);
    }

    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }

    static int[] GetFibonacciSequence(int length)
    {
        int[] sequence = new int[length];
        sequence[0] = 0;
        sequence[1] = 1;

        for (int i = 2; i < length; i++)
        {
            sequence[i] = sequence[i - 1] + sequence[i - 2];
        }

        return sequence;
    }

    static string ConvertToBinary(int number)
    {
        return Convert.ToString(number, 2);
    }

    static int ConvertFromBinary(string binary)
    {
        return Convert.ToInt32(binary, 2);
    }

    static string ReverseString(string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    static int CountWords(string text)
    {
        return text.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    static void PrintArray(int[] array)
    {
        foreach (int value in array)
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();
    }
	
	
	  static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static string RemoveWhitespace(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    static bool TryParseDate(string input, out DateTime date)
    {
        return DateTime.TryParse(input, out date);
    }

    static string ToTitleCase(string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }

    static bool IsSorted(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
            if (array[i - 1] > array[i])
                return false;
        return true;
    }

    static int BinarySearch(int[] array, int value)
    {
        int left = 0, right = array.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (array[mid] == value)
                return mid;
            else if (array[mid] < value)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1; // not found
    }

    static int[] RemoveAtIndex(int[] source, int index)
    {
        int[] dest = new int[source.Length - 1];
        if (index > 0)
            Array.Copy(source, 0, dest, 0, index);

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
    }

    static int[] InsertAtIndex(int[] source, int value, int index)
    {
        int[] dest = new int[source.Length + 1];
        Array.Copy(source, dest, index);
        dest[index] = value;
        Array.Copy(source, index, dest, index + 1, source.Length - index);
        return dest;
    }

    static int[] FilterArray(int[] source, Predicate<int> filter)
    {
        return source.Where(x => filter(x)).ToArray();
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void ShuffleArray<T>(T[] array)
    {
        Random rng = new Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    static string EncryptString(string input, int key)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            letter = (char)(letter + key);
            if (letter > 'z')
            {
                letter = (char)(letter - 26);
            }
            else if (letter < 'a')
            {
                letter = (char)(letter + 26);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }

    static string DecryptString(string input, int key)
    {
        return EncryptString(input, -key);
    }
    
}
