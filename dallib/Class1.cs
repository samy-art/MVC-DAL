using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dallib
{
    public class EmpOrm
    {

        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID{ get; set; }
    }

    public class CDal
    {
        private readonly string cnnstr;
        SqlCommand cmd;
        SqlConnection cnn;

        public CDal()
        {
           cnnstr = @"data source=.\sqlexpress;initial catalog=NJDB;Integrated Security=true" ;
            cnn = new SqlConnection(cnnstr);
            cmd = new SqlCommand();
            cmd.Connection = cnn;
        }   
        
        public IEnumerable<EmpOrm> GetAllEmployees()
        {
            List<EmpOrm> list = new List<EmpOrm>();
            cmd.CommandText = "Select * from Employee";
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new EmpOrm {EmpID = reader.GetInt32(0), EmpName = reader.GetString(1) , DeptID = reader.GetString(2) });
            }
            cnn.Close();
            reader.Close();
            return list;

        }
        public EmpOrm GetByID(int id)
        {
            List<EmpOrm> list = new List<EmpOrm>();
            cmd.CommandText = $"Select * from Employee where EmpID = {id}";
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
          if (reader.Read())
            {
                list.Add(new EmpOrm { EmpID = reader.GetInt32(0), EmpName = reader.GetString(1), DeptID = reader.GetString(2) });
            }
            cnn.Close();
            reader.Close();
            return list.Count==0 ? null : list[0];


        }

        public bool ModifyEmployee(EmpOrm empOrm)
        {
            cmd.CommandText = $"update Employee set EmpName = '{empOrm.EmpName}',DeptID='{empOrm.DeptID}' where EmpID={empOrm.EmpID}";
            cnn.Open();
          int RowsEffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return RowsEffected > 0;

        }
        public bool AddEmployee(EmpOrm empOrm)
        {
            cmd.CommandText = $"insert into Employee values({empOrm.EmpID},'{empOrm.EmpName}','{empOrm.DeptID}')";
            cnn.Open();
            int RowsEffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return RowsEffected > 0;

        }
        public bool DeleteEmployee(int id)
        {
            cmd.CommandText = $"delete from Employee where EmpID = {id}";
            cnn.Open();
            int RowsEffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return RowsEffected > 0;

        }
    }
}
