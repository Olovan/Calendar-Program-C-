using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            int day;
            int month;
            int year;
            bool exit = false;
            int dayOfWeek;

            Introduction();

            while (!exit)
            {
                dayOfWeek = 1;
                GetInputFromUser(out day, out month, out year, out exit);

                if (day > 0) //If Input was Valid
                {
                    dayOfWeek += CalculateDayOfWeek(day, month, year);
                    dayOfWeek = dayOfWeek % 7;
                    RenderMonth(day, month, year, dayOfWeek);
                }
            }
            

        }

        static void Introduction()
        {
            Console.WriteLine("Welcome to the Calendar Program. Written by: Micah Smith");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter a date in the format MM/DD/YYYY");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("To escape type \"exit\" or \"quit\"");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void GetInputFromUser(out int day, out int month, out int year, out bool exit)
        {
            Console.WriteLine("Enter a Date");
            string userInput = Console.ReadLine();

            if(userInput.ToLower() == "quit" || userInput.ToLower() == "exit" || userInput.ToLower() == "q" || userInput.ToLower() == "e")
            {
                day = 0;
                month = 0;
                year = 0;
                exit = true;
                return;
            }

            if (userInput.Length != 10) //Check to make sure it's the right size
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date must be in the format MM/DD/YYYY String Size Incorrect");
                Console.ForegroundColor = ConsoleColor.White;
                day = 0;
                month = 0;
                year = 0;
                exit = false;
            }
            else if (userInput[2].ToString() != "/" || userInput[5].ToString() != "/") //Check to make sure they put in slashes
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date must be in the format MM/DD/YYYY Slashes not Detected");
                Console.ForegroundColor = ConsoleColor.White;
                day = 0;
                month = 0;
                year = 0;
                exit = false;
            }
            else if (!int.TryParse(userInput.Substring(0, 2), out month)) //Check month to make sure month is number
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date must be in the format MM/DD/YYYY Month not Integer");
                Console.ForegroundColor = ConsoleColor.White;
                day = 0;
                month = 0;
                year = 0;
                exit = false;
            }
            else if (!int.TryParse(userInput.Substring(3, 2), out day)) //Check day to make sure day is number
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date must be in the format MM/DD/YYYY Day not Integer");
                Console.ForegroundColor = ConsoleColor.White;
                day = 0;
                month = 0;
                year = 0;
                exit = false;
            }
            else if (!int.TryParse(userInput.Substring(6, 4), out year)) //Check year to make sure year is number
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date must be in the format MM/DD/YYYY Year not Integer");
                Console.ForegroundColor = ConsoleColor.White;
                day = 0;
                month = 0;
                year = 0;
                exit = false;
            }
            else //They Entered a date
            {
                month = int.Parse(userInput.Substring(0, 2));
                day = int.Parse(userInput.Substring(3, 2));
                year = int.Parse(userInput.Substring(6, 4));
                exit = false;
                if (month > 12 || day > 31 || day < 1 || month < 1 || year < 0) //Check to make sure valid day/month/year values were entered
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Date!");
                    Console.ForegroundColor = ConsoleColor.White;
                    day = 0;
                    month = 0;
                    year = 0;
                    exit = false;
                } //NEED TO VERIFY CORRECT NUMBER OF DAYS FOR EACH MONTH
                else //Valid Dates were entered
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Valid Date Detected");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        static int CalculateDayOfWeek (int day, int month, int year)
        {
            int dayOffset;
            dayOffset = (year - 1) * 365; //Apply Offset from Year
            switch (month) //Apply Ofset from Month
            {
                case 1:
                    { 
                        dayOffset += 0;
                        break;
                    }
                case 2:
                    { 
                        dayOffset += 31;
                        break;
                    }
                case 3:
                    {
                        dayOffset += 59;
                        break;
                    }
                case 4:
                    {
                        dayOffset += 90;
                        break;
                    }
                case 5:
                    {
                        dayOffset += 120;
                        break;
                    }
                case 6:
                    {
                        dayOffset += 151;
                        break;
                    }
                case 7:
                    {
                        dayOffset += 181;
                        break;
                    }
                case 8:
                    {
                        dayOffset += 212;
                        break;
                    }
                case 9:
                    {
                        dayOffset += 243;
                        break;
                    }
                case 10:
                    {
                        dayOffset += 273;
                        break;
                    }
                case 11:
                    {
                        dayOffset += 304;
                        break;
                    }
                case 12:
                    {
                        dayOffset += 334;
                        break;
                    }
            }

            dayOffset += day - 1; //Apply day to offset

            dayOffset += CalculateLeapYears(year, month);
                
            return dayOffset;
        }

        static int CalculateLeapYears (int year, int month)
        {
            int leapyears = 0;
            leapyears += year / 4;
            leapyears -= year / 100;
            leapyears += year / 400;

            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) //If it's a leapyear
            {
                if (month < 3) //Remove the extra day from this year's leapyear if it isn't past Febuary yet
                {
                    leapyears -= 1;
                }
            }

            return leapyears;
        }

        static void DisplayDayOfWeek (int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0:
                    {
                        Console.WriteLine("It's a Sunday");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("It's a Monday");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("It's a Tuesday");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("It's a Wednesday");
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("It's a Thursday");
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("It's a Friday");
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("It's a Saturday");
                        break;
                    }
            }
        }

        static void RenderMonth (int day, int month, int year, int dayOfWeek)
        {
            int daysPerMonth = CalculateDaysInMonth(day, month, year);
            RenderMonthName(month);
            RenderWeekdayHeaders();
            RenderMonthDays(CalculateDayOfWeekOfFirstDay(day, dayOfWeek), daysPerMonth, day);
        }

        static int CalculateDaysInMonth (int day, int month, int year)
        {
            int daysPerMonth = 0;

            switch (month)
            {
                case 1:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 2:
                    {
                        daysPerMonth = 28;
                        break;
                    }
                case 3:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 4:
                    {
                        daysPerMonth = 30;
                        break;
                    }
                case 5:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 6:
                    {
                        daysPerMonth = 30;
                        break;
                    }
                case 7:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 8:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 9:
                    {
                        daysPerMonth = 30;
                        break;
                    }
                case 10:
                    {
                        daysPerMonth = 31;
                        break;
                    }
                case 11:
                    {
                        daysPerMonth = 30;
                        break;
                    }
                case 12:
                    {
                        daysPerMonth = 31;
                        break;
                    }
            }

            if ((month == 2 && year % 4 == 0) && (year % 100 != 0 || year % 400 == 0))
            {
                daysPerMonth += 1;
            }

            return daysPerMonth;
        }

        static void RenderMonthName (int month)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            switch (month)
            {
                case 1:
                    {
                        Console.WriteLine("      January       ");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("      February       ");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("       March        ");
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("       April        ");
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("        May         ");
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("        June         ");
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("        July         ");
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("       August       ");
                        break;
                    }
                case 9:
                    {
                        Console.WriteLine("      September      ");
                        break;
                    }
                case 10:
                    {
                        Console.WriteLine("       October       ");
                        break;
                    }
                case 11:
                    {
                        Console.WriteLine("       November      ");
                        break;
                    }
                case 12:
                    {
                        Console.WriteLine("       December      ");
                        break;
                    }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void RenderMonthDays (int firstDayOfWeek, int daysInMonth, int highlightDay)
        {
            int dayCounter = 1;
            int weekdayCounter = 0;
            //FIRST WEEK
            while(weekdayCounter < 7)
            {
                if(weekdayCounter < firstDayOfWeek)
                {
                    Console.Write("   ");
                    weekdayCounter++;
                } else
                {
                    if (dayCounter != highlightDay)
                    {
                        Console.Write(" {0} ", dayCounter);
                        dayCounter++;
                        weekdayCounter++;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" {0} ", dayCounter);
                        Console.ForegroundColor = ConsoleColor.White;
                        dayCounter++;
                        weekdayCounter++;
                    }
                }
                if(weekdayCounter == 7)
                {
                    Console.Write("\n");
                }
            }

            weekdayCounter = 0;
            //Rest of Month
            while (dayCounter <= daysInMonth)
            {
                if (dayCounter != highlightDay)
                {
                    if (dayCounter < 10)
                    {
                        Console.Write(" {0} ", dayCounter);
                    } else
                    {
                        Console.Write("{0} ", dayCounter);
                    }
                    dayCounter++;
                    weekdayCounter++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (dayCounter < 10)
                    {
                        Console.Write(" {0} ", dayCounter);
                    }
                    else
                    {
                        Console.Write("{0} ", dayCounter);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    dayCounter++;
                    weekdayCounter++;
                }
                if (weekdayCounter == 7)
                {
                    Console.Write("\n");
                    weekdayCounter = 0;
                }
            }

            Console.Write("\n");

        }

        static int CalculateDayOfWeekOfFirstDay(int day, int dayOfWeek)
        {
            int firstDayOfMonthWeekday = (dayOfWeek - (day - 1)) % 7;
            if(firstDayOfMonthWeekday < 0)
            {
                firstDayOfMonthWeekday += 7;
            }
            return firstDayOfMonthWeekday;
        }

        static void RenderWeekdayHeaders ()
        {
            Console.WriteLine(" S  M  T  W  T  F  S ");
        }
    }
}

