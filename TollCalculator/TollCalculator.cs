using System;
using System.Globalization;
using TollCalculator;
using TollFeeCalculator;
using TollCalculator.Models;


namespace TollFeeCalculator
{
    public class TollCalculator
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            int currentHourFee = 0;
            foreach (DateTime date in dates)
            {
                
                int nextFee = GetTollFee(date, vehicle);  //aktuella
                if (date>=intervalStart.AddHours(1))  //This is when we should update total fee and reset the running max fee
                {
                    intervalStart = date; //Ny period
                    totalFee += currentHourFee;
                    currentHourFee = 0;
                }
                currentHourFee = Math.Max(currentHourFee, nextFee); //Första eller maximala

                //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                //long minutes = diffInMillies/1000/60;

                //if (minutes <= 60)
                //{
                //    if (totalFee > 0) totalFee -= tempFee;
                //    if (nextFee >= tempFee) tempFee = nextFee;
                    
                //}
                //else
                //{
                //    totalFee += nextFee;
                //}
            }
            totalFee += currentHourFee;
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();
            return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;
           
            int cost = 0;
            switch (hour)
            {
                case 6:
                    cost = (minute <= 29) ? 8 : 13;
                    break;
                case 7:
                    cost = 18;
                    break;
                case 8:
                    cost = (minute <= 29) ? 13 : 8;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    cost = 8;
                    break;
                case 14:
                    cost = (minute <= 29) ? 8 : 13;
                    break;
                case 15:
                    cost = (minute <= 29) ? 13 : 18;
                    break;
                case 16:
                    cost = 18;
                    break;
                case 17:
                    cost = 13;
                    break;
                case 18:
                    cost = (minute <= 29) ? 8 : 0;
                    break;
                default: 
                    cost= 0;
                    break;
            }
            return cost;
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2024)
            {
                if (month == 1 && day == 1 || //Nyårsdagen
                    month == 3 && (day == 27 || day == 28) || //Skärtorsdag + långfredag
                    month == 4 && (day == 1 || day == 30) || //an.dag påsk + vbmafton
                    month == 5 && (day == 1 || day == 9 || day == 10) || //1 maj + kristi himf+klämdag
                    month == 6 && (day == 6 || day == 7 || day == 21) || //Nationaldag + klämdag + midsommarafton
                    month == 7 ||  //Semestermånad
                    month == 11 && day == 2 ||  //Alla helgons dag
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))  //Jul och nyår
                {
                    return true;
                }
            }
            return false;
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}