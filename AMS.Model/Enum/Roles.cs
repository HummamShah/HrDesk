using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Enums
{
    public static class Roles
    {
        public static string Employee = "Employee";
        public static string Lead_Creation = "Lead_Creation";
        public static string Lead_Sales = "Lead_Sales";
        public static string Pmd_Feasibility = "Pmd_Feasibility";
        public static string PreSale = "PreSale";
        public static string Closer = "Closer";
        public static string Admin = "Admin";
        public static string Accounts = "Accounts"; // add in db live
        public static string Core = "Core"; // add in db live
        public static string SD = "SD"; // add in db live
        public static string SuperAdmin = "SuperAdmin";
        public static string HR = "HR"; //add in db
        //INSERT INTO[Sharptel_Lms_Db].[dbo].[AspNetRoles]
        //(Id, Name)
        //VALUES(8, 'Accounts');
        //        INSERT INTO[Sharptel_Lms_Db].[dbo].[AspNetRoles]
        //        (Id, Name)
        //VALUES(9, 'Core');

        //        INSERT INTO[Sharptel_Lms_Db].[dbo].[AspNetRoles]
        //        (Id, Name)
        //VALUES(10, 'SD');
        
        //        INSERT INTO[Sharptel_Lms_Db].[dbo].[AspNetRoles]
        //        (Id, Name)
        //VALUES(11, 'HR');
    }
}