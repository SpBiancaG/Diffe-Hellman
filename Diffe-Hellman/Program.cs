using System;

public class DiffieHellmanExample
{
    public static void Main()
    {
        // Citirea și validarea numărului prim q
        Console.Write("Introduceți numărul prim q: ");
        int q = int.Parse(Console.ReadLine());

        if (!IsPrime(q))
        {
            Console.WriteLine("Numărul introdus nu este prim.");
            return;
        }

        // Găsirea radacinii primitive g
        int g = FindPrimitiveRoot(q);
        if (g == -1)
        {
            Console.WriteLine("Nu s-a găsit o radacină primitivă pentru q.");
            return;
        }

        Console.WriteLine("Radacina primitiva pentru q este: " + g);

        // Introducerea cheilor secrete de la tastatură
        Console.Write("Introduceți cheia secretă pentru utilizatorul A (mai mică decât q): ");
        int xA = int.Parse(Console.ReadLine());

        Console.Write("Introduceți cheia secretă pentru utilizatorul B (mai mică decât q): ");
        int xB = int.Parse(Console.ReadLine());

        // Calcularea cheilor publice
        int yA = CalculatePublicKey(g, xA, q);
        int yB = CalculatePublicKey(g, xB, q);

        Console.WriteLine("Cheia publică generată pentru utilizatorul A: " + yA);
        Console.WriteLine("Cheia publică generată pentru utilizatorul B: " + yB);

        // Schimbul de chei publice

        // Calcularea cheilor comune
        int kA = CalculateSharedKey(yB, xA, q);
        int kB = CalculateSharedKey(yA, xB, q);

        Console.WriteLine("Cheia comună secretă calculată pentru utilizatorul A: " + kA);
        Console.WriteLine("Cheia comună secretă calculată pentru utilizatorul B: " + kB);
    }

    // Verifică dacă un număr este prim
    private static bool IsPrime(int number)
    {
        if (number < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    // Găsește o radacină primitivă pentru un număr prim
    private static int FindPrimitiveRoot(int prime)
    {
        for (int i = 2; i < prime; i++)
        {
            bool[] set = new bool[prime - 1];
            int temp = i;

            for (int j = 0; j < prime - 1; j++)
            {
                if (set[temp - 1])
                    break;

                set[temp - 1] = true;
                temp = (temp * i) % prime;
            }

            if (Array.TrueForAll(set, val => val))
                return i;
        }

        return -1;
    }

    // Calculează cheia publică
    private static int CalculatePublicKey(int g, int privateKey, int prime)
    {
        return ModuloPower(g, privateKey, prime);
    }

    // Calculează cheia comună secretă
    private static int CalculateSharedKey(int publicKey, int privateKey, int prime)
    {
        return ModuloPower(publicKey, privateKey, prime);
    }

    // Calculează a^b mod n
    private static int ModuloPower(int a, int b, int n)
    {
        long result = 1;
        long x = a % n;

        for (int i = 0; i < sizeof(int) * 8; i++)
        {
            if ((b & (1 << i)) != 0)
                result = (result * x) % n;

            x = (x * x) % n;
        }

        return (int)result;
    }
}
