using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolApp.Models;

namespace SchoolApp.Controllers;
public class TeacherDataController : Controller
{
    // The database context class which allows us to access our MySQL Database.
    private SchoolDbContext SchoolDb = new SchoolDbContext();

    [HttpPost]
    public IActionResult Add(Teacher teacher)
    {
        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO teachers(employeenumber,hiredate,salary,teacherfname,teacherlname) VALUES(@employeenumber,@hiredate,@salary,@teacherfname,@teacherlname)";
        command.Parameters.AddWithValue("@employeenumber", teacher.EmployeeNumber);
        command.Parameters.AddWithValue("@hiredate", teacher.HireDate);
        command.Parameters.AddWithValue("@salary", teacher.Salary);
        command.Parameters.AddWithValue("@teacherfname", teacher.FirstName);
        command.Parameters.AddWithValue("@teacherlname", teacher.LastName);
        command.ExecuteNonQuery();
        return Ok();
    }

    [HttpPost]
    public IActionResult Update(Teacher teacher)
    {
        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE teachers SET employeenumber=@employeenumber,hiredate=@hiredate,salary=@salary,teacherfname=@teacherfname,teacherlname=@teacherlname WHERE teacherid=@teacherid";
        command.Parameters.AddWithValue("@employeenumber", teacher.EmployeeNumber);
        command.Parameters.AddWithValue("@hiredate", teacher.HireDate);
        command.Parameters.AddWithValue("@salary", teacher.Salary);
        command.Parameters.AddWithValue("@teacherfname", teacher.FirstName);
        command.Parameters.AddWithValue("@teacherlname", teacher.LastName);
        command.Parameters.AddWithValue("@teacherid", teacher.Id);
        command.ExecuteNonQuery();
        return Ok();
    }

    [HttpDelete("{teacherId}")]
    public IActionResult DeleteTeacher(int teacherId)
    {
        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM teachers WHERE teacherid=@teacherid";
        command.Parameters.AddWithValue("@teacherid", teacherId);
        command.ExecuteNonQuery();
        return Ok();
    }


    [HttpGet("{teacherId}")]
    public IActionResult FindTeacher(int teacherId) { 

        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM teachers WHERE teacherid=@teacherid";
        command.Parameters.AddWithValue("@teacherid", teacherId);

         MySqlDataReader reader = command.ExecuteReader();

        Teacher teacher = new Teacher();

        if(reader.Read()){
            teacher.Id = (int)reader["teacherid"];
            teacher.EmployeeNumber = reader["employeenumber"].ToString();
            teacher.HireDate = DateTime.Parse(reader["hiredate"].ToString());
            teacher.FirstName = reader["teacherfname"].ToString();
            teacher.LastName = reader["teacherlname"].ToString();
            teacher.Salary = decimal.Parse(reader["salary"].ToString());
        }

        connection.Close();

        return Ok(teacher);
    }

}