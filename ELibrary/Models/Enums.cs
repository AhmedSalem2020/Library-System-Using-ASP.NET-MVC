using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
    //public enum Role
    //{
    //    BasicAdmin=0,
    //    Admin=1,
    //    Employee=2 ,
    //    //Memkber=3
    //}
    public enum BookStatus
    {
        isReading=0,
        isborrowking = 1,
    }
    public enum blockStaues
    {
        notBlock=0,
        blockInWeek,
        blockAfterWeek

    }
}