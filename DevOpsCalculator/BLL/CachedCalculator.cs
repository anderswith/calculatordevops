using System.Runtime.CompilerServices;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL.Repositories.interfaces;

namespace DevOpsCalculator.BLL;

public class CachedCalculator : ICalculator, ICachedCalculator
{
    private readonly ICalculatorRepository _repository;
    private readonly SimpleCalculator _calculator;
    private readonly Dictionary<string, object> _cache = new();

    public CachedCalculator(ICalculatorRepository repository)
    {
        _repository = repository;
        _calculator = new SimpleCalculator(_repository);
    }
    public int Add(int a, int b)
    {
        Console.WriteLine($"Attempting to add {a} and {b}");
    
        var result = _calculator.Add(a, b);
        Console.WriteLine($"Raw result from _calculator.Add: {result}");

        var calc = StoreInCache(result, a, b);
        Console.WriteLine($"Stored in cache: {calc.GetKey()} with result {calc.Result}");

        return calc.Result;
    }

    public int Subtract(int a, int b)
    {
        var calc = StoreInCache(_calculator.Subtract(a, b), a, b);
        return calc.Result;
    }

    public int Multiply(int a, int b)
    {
        var calc = StoreInCache(_calculator.Multiply(a, b), a, b);
        return calc.Result;
    }

    public int Divide(int a, int b)
    {
        var calc = StoreInCache(_calculator.Divide(a, b), a, b);
        return calc.Result;
    }

    public int Factorial(int n)
    {
        var calc = StoreInCache(_calculator.Factorial(n), n);
        return calc.Result;
    }

    public bool IsPrime(int candidate)
    {
        var calc = StoreInCache(_calculator.IsPrime(candidate), candidate);
        return calc.Result;
    }
    public Calculation<T>? GetCachedResult<T>(int a, int? b = null, [CallerMemberName] string operation = "")
    {
        var calc = new Calculation<T>(default, operation, a, b);
        var cacheKey = calc.GetKey();  // Generate the cache key

        Console.WriteLine($"Searching cache for key: {cacheKey}");

        // Try to get the value from the cache using TryGetValue for better error handling
        if (_cache.TryGetValue(cacheKey, out var cachedObject))
        {
            // Check if the cached object is of the correct type before accessing it
            if (cachedObject is Calculation<T> cachedCalculation)
            {
                Console.WriteLine($"Cache hit: {cacheKey} = {cachedCalculation.Result}");
                return cachedCalculation;
            }
            else
            {
                // Log the issue if the cache item is of a different type
                Console.WriteLine($"Cache key {cacheKey} found, but it was not of type Calculation<{typeof(T).Name}>.");
            }
        }
        else
        {
            // Cache miss log
            Console.WriteLine($"Cache miss for key: {cacheKey}");
        }

        return null;
    }

    public Calculation<T> StoreInCache<T>(T result, int a, int? b = null, [CallerMemberName] string operation = "")
    {
        var calc = new Calculation<T>(result, operation, a, b);
        var key = calc.GetKey();  // Get the key
        Console.WriteLine($"Storing in cache: Key = {key}, Value = {result}");
    
        _cache.Add(key, (object)calc);  // Store the calculation object in the cache
        return calc;
    }


    public class Calculation
    {
        private int A { get; }
        private int? B { get; }
        private string Operation { get; }

        protected Calculation(string operation, int a, int? b = null)
        {
            A = a;
            B = b;
            Operation = operation;
        }


        public string GetKey()
        {
            Console.WriteLine($"Generating cache key for A={A}, Operation={Operation}, B={B?.ToString() ?? "null"}");
            return string.Concat(A, Operation, B?.ToString() ?? "null");
        }

    }

    public class Calculation<T> : Calculation
    {
        public T Result { get; set; }

        public Calculation(T result, string operation, int a, int? b = null)
            : base(operation, a, b)
        {
            Result = result;
        }
    }
}