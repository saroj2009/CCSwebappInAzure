using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CCSmvc.Models
{
    public class Empdetails
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DOB { get; set; }

        public string Image { get; set; }

        public string aid { get; set; }

        public string dvid { get; set; }
        public Empdetails()
        {
            Url = "https://sarojwebappstorage.blob.core.windows.net/sarojcontainer/";
        }
        public string Url { get; set; }

    }

    [Table("EmpDetails")]
    public class Empldetails
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Required Name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Required ContactName")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required DOB")]
        public string DOB { get; set; }

        
    }
    public class common
    {
        public string getDOB(string val)
        {
            string strReturnval = "1st Jan";
            string[] str = val.Split('/');
            int date = Convert.ToInt16(str[0]);
            int month = Convert.ToInt16(str[1]);
            switch (date)
            {
                case 1:
                    strReturnval = "1st";
                    break;
                case 2:
                    strReturnval = "2nd";
                    break;
                case 3:
                    strReturnval = "3rd";
                    break;
                case 4:
                    strReturnval = "4th";
                    break;
                case 5:
                    strReturnval = "5th";
                    break;
                case 6:
                    strReturnval = "6th";
                    break;
                case 7:
                    strReturnval = "7th";
                    break;
                case 8:
                    strReturnval = "8th";
                    break;
                case 9:
                    strReturnval = "9th";
                    break;
                case 10:
                    strReturnval = "10th";
                    break;
                case 11:
                    strReturnval = "11th";
                    break;
                case 12:
                    strReturnval = "12th";
                    break;
                case 13:
                    strReturnval = "13th";
                    break;
                case 14:
                    strReturnval = "14th";
                    break;
                case 15:
                    strReturnval = "15th";
                    break;
                case 16:
                    strReturnval = "16th";
                    break;
                case 17:
                    strReturnval = "17th";
                    break;
                case 18:
                    strReturnval = "18th";
                    break;
                case 19:
                    strReturnval = "19th";
                    break;
                case 20:
                    strReturnval = "20th";
                    break;
                case 21:
                    strReturnval = "21st";
                    break;
                case 22:
                    strReturnval = "22nd";
                    break;
                case 23:
                    strReturnval = "23rd";
                    break;
                case 24:
                    strReturnval = "24th";
                    break;
                case 25:
                    strReturnval = "25th";
                    break;
                case 26:
                    strReturnval = "26th";
                    break;
                case 27:
                    strReturnval = "27th";
                    break;
                case 28:
                    strReturnval = "28th";
                    break;
                case 29:
                    strReturnval = "29th";
                    break;
                case 30:
                    strReturnval = "30th";
                    break;
                case 31:
                    strReturnval = "31st";
                    break;

            }
            switch (month)
            {
                case 1:
                    strReturnval = strReturnval +" "+ "January";
                    break;
                case 2:
                    strReturnval = strReturnval + " " + "February";
                    break;
                case 3:
                    strReturnval = strReturnval + " " + "March";
                    break;
                case 4:
                    strReturnval = strReturnval + " " + "April";
                    break;
                case 5:
                    strReturnval = strReturnval + " " + "May";
                    break;
                case 6:
                    strReturnval = strReturnval + " " + "June";
                    break;
                case 7:
                    strReturnval = strReturnval + " " + "July";
                    break;
                case 8:
                    strReturnval = strReturnval + " " + "August";
                    break;
                case 9:
                    strReturnval = strReturnval + " " + "September";
                    break;
                case 10:
                    strReturnval = strReturnval + " " + "October";
                    break;
                case 11:
                    strReturnval = strReturnval + " " + "November";
                    break;
                default:
                    strReturnval = strReturnval + " " + "December";
                    break;
            }
            return strReturnval;
        }
    }
}