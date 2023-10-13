// See https://aka.ms/new-console-template for more information


        TollCalculator.Models.Vehicle[] vehicleCollection = new TollCalculator.Models.Vehicle[]
           {
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Motorbike(),
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Tractor(),
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Emergency(),
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Motorbike(),
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Tractor(),
                new TollCalculator.Models.Car(),
                new TollCalculator.Models.Emergency()
           };
//Testing long time max 60
        DateTime dtStart = new DateTime(2024, 6, 20, 5, 0, 0, 0, 0);
        DateTime date = dtStart.Date;
        for (int t =0;t<12;t++)
        {

            List<DateTime> dtColl = new List<DateTime>();
            int minuteOffset = 3 + t*2;
            DateTime dtnew = dtStart.AddMinutes(minuteOffset);
    
            while (dtnew.Date.CompareTo(date.Date) == 0)
            {
                dtColl.Add(dtnew);
                dtnew = dtnew.AddMinutes(minuteOffset);
            }
            TollFeeCalculator.TollCalculator  calc = new TollFeeCalculator.TollCalculator();
            
            int total =calc.GetTollFee(vehicleCollection[t], dtColl.ToArray()); //This is a car
            Console.WriteLine("Vehicle of type " + vehicleCollection[t].GetVehicleType() + " has a total amount of "+ total.ToString() + " to pay for date of " +date.Date.ToShortDateString());

        
        }

        //Testing hour limit
        for (int t = 0; t < 12; t++)
        {
            dtStart = new DateTime(2024, 6 , 20, 5+t, 0, 0, 0, 0);
            List<DateTime> dtColl = new List<DateTime>();
            int minuteOffset = 7; //9 first will be in same hour period
            DateTime dtNew = dtStart;
            DateTime dtEnd = dtStart;
            for (int m = 0; m < 16; m++)
            {
                dtColl.Add(dtNew);
                dtEnd = dtNew;
                dtNew = dtNew.AddMinutes(minuteOffset);
            }

            TollFeeCalculator.TollCalculator calc = new TollFeeCalculator.TollCalculator();

            int total = calc.GetTollFee(vehicleCollection[0], dtColl.ToArray());
            Console.WriteLine("Vehicle of type " + vehicleCollection[0].GetVehicleType() + " has a total amount of " + total.ToString());
            Console.WriteLine("to pay for  " + date.Date.ToShortDateString() + " Time period " + dtStart.ToShortTimeString() + "-" + dtNew.ToShortTimeString());
    
        }

    //Testing sequences in a day
    for (int h = 0; h < 7; h++)
    {
        List<DateTime> dtColl = new List<DateTime>();
        for (int t = 0; t < 3; t++)
        {
            dtStart = new DateTime(2024, 6, 20, 6 + h + t*4, 0, 0, 0, 0);
        
            int minuteOffset = 7; //9 first will be in same hour period
            DateTime dtNew = dtStart;
            DateTime dtEnd = dtStart;
            for (int m = 0; m < 8; m++)
            {
                dtColl.Add(dtNew);
                dtEnd = dtNew;
                dtNew = dtNew.AddMinutes(minuteOffset);
            }
            Console.WriteLine("Period  " + date.Date.ToShortDateString() + " Time period " + dtStart.ToShortTimeString() + "-" + dtNew.ToShortTimeString());

        }
        TollFeeCalculator.TollCalculator calc = new TollFeeCalculator.TollCalculator();
        int total = calc.GetTollFee(vehicleCollection[0], dtColl.ToArray());
        Console.WriteLine("Vehicle of type " + vehicleCollection[0].GetVehicleType() + " has a total amount of " + total.ToString());

    }
        //Testing holiday limit
        for (int t = 0; t < 10; t++)
        {
            dtStart = new DateTime(2024, 6, 19+t, 7, 0, 0, 0, 0);
            List<DateTime> dtColl = new List<DateTime>();
            int minuteOffset = 7; //9 first will be in same hour period
            DateTime dtNew = dtStart;
            DateTime dtEnd = dtStart;
            for (int m = 0; m < 16; m++)
            {
                dtColl.Add(dtNew);
                dtEnd = dtNew;
                dtNew = dtNew.AddMinutes(minuteOffset);
            }

            TollFeeCalculator.TollCalculator calc = new TollFeeCalculator.TollCalculator();

            int total = calc.GetTollFee(vehicleCollection[0], dtColl.ToArray());
            Console.WriteLine("Vehicle of type " + vehicleCollection[0].GetVehicleType() + " has a total amount of " + total.ToString());
            Console.WriteLine("to pay for  " + dtStart.Date.ToShortDateString() + " Time period " + dtStart.ToShortTimeString() + "-" + dtNew.ToShortTimeString());

        }

