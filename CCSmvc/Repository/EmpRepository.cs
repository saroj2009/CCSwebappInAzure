using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CCSmvc.Models;
using CCSmvc1.Models;
using System.Data;
using Dapper;

namespace CCSmvc.Repository
{
    public class EmpRepository
    {
        public SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString();
            con = new SqlConnection(constr);
        }
        //To Add Employee details
        public void AddEmployee(EmployeeModel objEmp)
        {
            //Additing the employess
            try
            {
                int id = RandomNumber(1, 500);
                objEmp.aid = Convert.ToString(id);
                objEmp.dvid = Convert.ToString(id);


                connection();
                con.Open();
                con.Execute("AddNew", objEmp, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Generate a random number between two numbers
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        //To view employee details with generic list 
        public List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                connection();
                con.Open();
                IList<EmployeeModel> EmpList = SqlMapper.Query<EmployeeModel>(
                                  con, "GetEmployees").ToList();
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //To Update Employee details
        public void UpdateEmployee(EmployeeModel objUpdate)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateEmpDetails", objUpdate, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //To delete Employee details
        public bool DeleteEmployee(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                connection();
                con.Open();
                con.Execute("DeleteEmpById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                //Log error as per your need 
                throw ex;
            }
        }
        public int GetImageCount(string filename)
        {
            DynamicParameters ObjParm = new DynamicParameters();
            ObjParm.Add("@FileName", filename);
            ObjParm.Add("@ImageCount", dbType: DbType.Int16, direction: ParameterDirection.Output, size: 5215585);
            connection();
            con.Open();
            con.Execute("GetImageCount", ObjParm, commandType: CommandType.StoredProcedure);
            //Getting the out parameter value of stored procedure  
            var Count = ObjParm.Get<Int16>("@ImageCount");
            con.Close();
            return Count;

        }
    }
}