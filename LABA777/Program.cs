//IDE changes
//Змінено з браузера

//Змінено з IDE

//New1

//NEW2

using System;
using System.IO;
using System.Linq;

namespace task_7
{
    public enum WeatherType
    {
        noDefine,
        rain,
        short_rain,
        thunderstorm,
        snow,
        fog,
        sunny,
        darkly
    }
    static class MyValues
    {
        public static int monthDay = 30;
        public static string path = @"C:\Users\пк\Desktop\Lab7\Day.txt";
    }

    class WeatherParametersDay
    {

        public WeatherType WeatherType;
        public double AverageTemperatureDay;
        public double AverageTemperatureNight;
        public double AverageAtmosphericPressure;
        public double Precipitation;

        public WeatherParametersDay(double averageTemperatureDay, double averageTemperatureNight, double averageAtmosphericPressure, double precipitation, int weatherType)
        {
            WeatherType = (WeatherType)weatherType;
            AverageTemperatureDay = averageTemperatureDay;
            AverageTemperatureNight = averageTemperatureNight;
            AverageAtmosphericPressure = averageAtmosphericPressure;
            Precipitation = precipitation;
        }
    }


    class WeatherDays
    {
        private WeatherParametersDay[] WeatherArray;

        public WeatherDays(WeatherParametersDay[] WeatherArray1)
        {
            WeatherArray = WeatherArray1;
        }

        private int CountDays(params WeatherType[] weatherType)
        {
            int daysInMonth = 0;
            foreach (WeatherParametersDay day in WeatherArray)
                if (weatherType.Contains(day.WeatherType))
                {
                    daysInMonth += 1;
                }

            return daysInMonth;
        }
        public int CountFogDays()
        {
            return CountDays(WeatherType.fog);
        }

        public int CountRainDays()
        {
            return CountDays(WeatherType.snow, WeatherType.rain, WeatherType.short_rain);
        }


        public double AverageTemperatureDay(int countdays)
        {
            double tempSum = 0;
            foreach (WeatherParametersDay day in WeatherArray)
            {
                tempSum += day.AverageTemperatureDay;
            }

            double avgTemp = tempSum / countdays;

            return avgTemp;
        }

        public double AverageTemperatureNightMonth(int countdays)
        {
            double tempSum1 = 0;
            foreach (WeatherParametersDay night in WeatherArray)
            {
                tempSum1 += night.AverageTemperatureNight;
            }

            double avgTemp = tempSum1 / countdays;

            return avgTemp;
        }

    }

    class Program
    {
        private static void PrintArray(int[,] daysConsoleArray)
        {
            for (int i = 0; i < daysConsoleArray.GetLength(0); i++)
            {
                for (int j = 0; j < daysConsoleArray.GetLength(1); j++)
                {
                    Console.Write(daysConsoleArray[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        
        private static int[,] ReadingFromFile(ref int[,] daysFileArray)
        {
            string[] lines = File.ReadAllLines(MyValues.path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Split(' ').Length != 30)
                {
                    Console.WriteLine($"Неправильна кількість елементів у рядку: {i + 1}! ");
                    Environment.Exit(0);
                }

                string[] temporary = lines[i].Split(' ');
                for (int j=0; j<MyValues.monthDay; j++)
                {
                    try
                    {
                        daysFileArray[i, j] = Convert.ToInt32(temporary[j]);
                    }
                    catch
                    {
                        Console.WriteLine("Якісь дані у файлі записані не належним чином!");
                        Environment.Exit(0);
                    }
                }              
            }                       
            PrintArray(daysFileArray);
            return CheckData(daysFileArray);
        }

        private static int[,] ReadingFromConsole(ref int[,] daysConsoleArray, int days)
        {
            Console.WriteLine($"Введіть дані за місяць листопад: \n1 рядок - середню температуру вдень, \n2 рядок - середню тепературу вночі, \n3 рядок - середній атмосферний тиск, \n4 рядок - кількість опадів, \n5 рядок - тип погоди (цифру від 0 до 7).");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Рядок {i + 1}: ");
                string[] line = Console.ReadLine().Split(' ');
                if (line.Length != days)
                {
                    Console.WriteLine("Дані введено некоректно!");
                    Environment.Exit(0);
                }

                string[] temporary = line;

                for (int j = 0; j < line.Length; j++)
                    try
                    {
                        daysConsoleArray[i, j] = Convert.ToInt32(line[j]);
                    }
                    catch
                    {
                        Console.WriteLine("Якісь дані у файлі записані не належним чином!");
                        Environment.Exit(0);
                    }
            }

            Console.WriteLine();
            Console.WriteLine("Ваш масив значень: ");
            PrintArray(daysConsoleArray);
            return CheckData(daysConsoleArray);
        }

        private static int[,] CheckData(int[,] daysArray)
        {
            for (int i = 0; i < daysArray.GetLength(1); i++)
            {
                if (daysArray[4, i] < 0 || daysArray[4, i] > 7 || daysArray[3, i] < 0 || daysArray[2, i] < 730 || daysArray[2, i] > 760)
                {
                    Console.WriteLine();
                    Console.WriteLine("Перевірте, будь ласка, Ваші дані та спробуйте ще. Майте на увазі: \nТиск повинен бути у межах від 730 до 760, \nКількість опадів повинна бути додатнім числом, \nА тип погоди - це числа від 0 до 7.");
                    Environment.Exit(0);
                }
            }
            return daysArray;
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[,] daysArray = new int[5, 0];
            int days1 = 0;

            while (true)
            {
                try
                {
                    Console.Write("Яким чином Ви хочете надати дані? \n1 - З клавіатури, \n2 - З файлу. \nВаша цифра: ");
                    int key = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("");
                    if (key == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Виберіть кількість днів у місяці листопаді, у які хочете ввести дані:");
                        int days = Convert.ToInt32(Console.ReadLine());
                        days1 = days;

                        int[,] daysArray1 = new int[5, days];
                        daysArray = daysArray1;

                        ReadingFromConsole(ref daysArray, days);
                    }
                    if (key == 2)
                    {
                        days1 = MyValues.monthDay;
                        int[,] daysArray2 = new int[5, MyValues.monthDay];
                        daysArray = daysArray2;
                        ReadingFromFile(ref daysArray2);
                    }
                    if (key != 1 && key != 2)
                    {
                        Console.WriteLine("Введіть цифру 1 або 2!");
                        Environment.Exit(0);
                    }
                    break;
                }

                catch
                {
                    Console.WriteLine("Перевірте правильність вводу!");
                    Console.WriteLine();
                    continue;
                }
            }


            WeatherParametersDay[] weatherParametersDays = new WeatherParametersDay[daysArray.GetLength(1)];

            for (int i = 0; i < daysArray.GetLength(1); i++)
            {
                weatherParametersDays[i] = new WeatherParametersDay(daysArray[0, i], daysArray[1, i], daysArray[2, i], daysArray[3, i], daysArray[4, i]);
            }

            WeatherDays weatherDays = new WeatherDays(weatherParametersDays); //об'єкт класу

            Console.WriteLine($"\nКількість днів, коли був туман: {weatherDays.CountFogDays()}\nКількість днів, у які були опади: {weatherDays.CountRainDays()}");
            Console.WriteLine($"Середня температура вдень у місяці: {weatherDays.AverageTemperatureDay(days1)}");
            Console.WriteLine($"Середня температура вночі у місяці: {weatherDays.AverageTemperatureNightMonth(days1)}");

            Console.ReadKey();
        }
    }
}
