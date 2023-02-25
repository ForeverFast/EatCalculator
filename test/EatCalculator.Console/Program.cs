// See https://aka.ms/new-console-template for more information
using EatCalculator.UI.Shared.Lib.EntityAdapter;
using System.Reflection;

Console.WriteLine("Hello, World!");
Adapters.Scan(new Assembly[] { Assembly.GetExecutingAssembly(), typeof(Adapters).Assembly });