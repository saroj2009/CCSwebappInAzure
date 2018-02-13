﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCSmvc.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace CCSmvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Empdetails> Empdetails = new List<Empdetails>();
            Empdetails = getdata();
            //Empdetails Obj = new Empdetails();
            //Obj.Id = 1;
            //Obj.Name = "Kayal";
            //Obj.Description = "hggafsghdfs";
            //Obj.DOB = "12/20/2017";
            //Obj.Image = "~/Content/images/teammember.jpg";
            //Obj.aid = "#s1";
            //Obj.dvid ="s1";
            //Empdetails.Add(Obj);

            //Obj = new Empdetails();
            //Obj.Id = 2;
            //Obj.Name = "Kayal2";
            //Obj.Description = "hggafsghdfs22222";
            //Obj.DOB = "12/2/2017";
            //Obj.Image = "~/Content/images/teammember.jpg";
            //Obj.aid = "#s2";
            //Obj.dvid = "s2";
            //Empdetails.Add(Obj);
            return View(Empdetails.ToList());
        }
        public List<Empdetails> getdata()
        {

            List<Empdetails> Empdetails = new List<Empdetails>();
           // var con = "Server=tcp:sarojwebappdb.database.windows.net,1433;Initial Catalog=sarojwebappdb;Persist Security Info=False;User ID=sarojwebappdb;Password=Saroj@12345678;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";// ConfigurationManager.ConnectionStrings["Yourconnection"].ToString();
            var con = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from EmpDetails";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                //oCmd.Parameters.AddWithValue("@Fname", fName);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        common cmn = new common();
                        Empdetails EmpdetailsObj = new Empdetails();
                        EmpdetailsObj.Name = oReader["Name"].ToString();
                        EmpdetailsObj.Description = oReader["Description"].ToString();
                        EmpdetailsObj.DOB = cmn.getDOB(oReader["DOB"].ToString());
                        EmpdetailsObj.aid = "#"+oReader["Name"].ToString();
                        EmpdetailsObj.dvid = oReader["Name"].ToString();
                        if (Convert.ToString(oReader["ImagePath"]) == "")
                            EmpdetailsObj.Image = "NoImage.png";
                        else
                        EmpdetailsObj.Image = oReader["ImagePath"].ToString();
                        Empdetails.Add(EmpdetailsObj);
                    }

                    myConnection.Close();
                }
            }
            return Empdetails;
       
    }
        
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}